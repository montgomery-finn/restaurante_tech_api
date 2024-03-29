﻿using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using restaurante_tech_api.DTOs;
using restaurante_tech_api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurante_tech_api.Controllers
{
    [Route("Orders")]
    public class OrdersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public OrdersController(
            ICustomerRepository customerRepository, 
            IProductRepository productRepository, 
            IOrderRepository orderRepository,
            IUserRepository userRepository
            )
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepository.GetFinished();
            
            foreach(var order in orders)
            {
                await _orderRepository.LoadCustomer(order);
                await _orderRepository.LoadProducts(order);
                await _orderRepository.LoadUser(order);
                
                if(order.User != null)
                    order.User.EncodedPassword = "";
            }

            return Ok(orders.OrderByDescending(o => o.CreatedAt).ToList());
        }

        [HttpPost]
        public async Task<dynamic> Create([FromBody] CreateOrderDTO dto)
        {
            Customer customer = null;

            if (!String.IsNullOrEmpty(dto.cpf))
            {
                customer = await GetCustomer(dto);

                if(customer == null)
                {
                    return NotFound("Usuário não encontrado");
                }
            }

            var order = new Order(customer?.ID);

            order.OrderProducts = new List<OrderProduct>();

            foreach(var productDTO in dto.products)
            {
                for(int i = 0; i < productDTO.quantity; i++)
                {
                    order.OrderProducts.Add(new OrderProduct(Guid.Parse(productDTO.productId), order.ID));
                }
            }

            await _orderRepository.Add(order);

            return StatusCode(200, order);
        }

        private async Task<Customer> GetCustomer(CreateOrderDTO dto)
        {
            Customer customer = null;

            if (!String.IsNullOrEmpty(dto.cpf))
            {
                customer = await _customerRepository.GetByCPF(dto.cpf);
            }

            return customer;
        }

        [HttpPost, Route("Finish")]
        public async Task<IActionResult> Finish([FromBody] FinishOrderDTO dto)
        {
            if (String.IsNullOrEmpty(dto.orderId))
                return StatusCode(400);

            var orderIdGuid = Guid.Parse(dto.orderId);

            var order = await _orderRepository.GetById(orderIdGuid);

            if (order == null)
                return NotFound();

            var userIdGuid = Guid.Parse(dto.userId);

            var user = await _userRepository.GetByID(userIdGuid);

            if (user == null)
                return StatusCode(400, "Usuário não encontrado");

            await _orderRepository.LoadCustomer(order);

            if (order.Customer != null)
            {
                if(dto.usedPoints > 0)
                {
                    if(order.Customer.Points < dto.usedPoints)
                    {
                        return StatusCode(400, "O cliente não possui pontos suficientes para esta compra");
                    }

                    DecreaseCustomerPoints(order.Customer, dto.usedPoints);
                }
            }

            order.Finish(user.ID);

            await _orderRepository.Update(order);

            if(order.Customer != null)
            {
                var orderWillGeneratePoint = true;

                var pricePaidInPoints = dto.usedPoints;

                if(pricePaidInPoints > 0)
                {
                    await _orderRepository.LoadProducts(order);
                    var maxOrderPoints = order.OrderProducts.Select(o => o.Product.PriceInPoints).Sum();
                    var customerPaidSomethinWithMoney = maxOrderPoints - pricePaidInPoints > 0;

                    orderWillGeneratePoint = customerPaidSomethinWithMoney;
                }

                if (orderWillGeneratePoint)
                {
                    IncreaseCustomerPoints(order.Customer);
                }

                await _customerRepository.Update(order.Customer);
            }

            return Ok();
        }

        private void IncreaseCustomerPoints(Customer customer)
        {
            customer.Points++;
        }

        private void DecreaseCustomerPoints(Customer customer, int usedPoints)
        {
            customer.Points -= usedPoints;
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var order = await _orderRepository.GetById(Guid.Parse(id));
            await _orderRepository.LoadCustomer(order);
            await _orderRepository.LoadProducts(order);

            return Ok(order);
        }
    }
}
