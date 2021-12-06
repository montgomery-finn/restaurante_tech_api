using Domain.Models;
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

        public OrdersController(
            ICustomerRepository customerRepository, 
            IProductRepository productRepository, 
            IOrderRepository orderRepository
            )
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<dynamic> Create([FromBody] CreateOrderDTO dto)
        {
            var customer = await GetCustomer(dto);

            var order = new Order(customer);

            order.OrderProducts = new List<OrderProduct>();

            foreach(var productDTO in dto.products)
            {
                order.OrderProducts.Add(new OrderProduct(Guid.Parse(productDTO.productId), productDTO.quantity, order.ID));
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

            order.Finish();

            await _orderRepository.Update(order);

            return Ok();
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
