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
        private readonly INewOrderNotificationService _newOrderNotificationService;

        public OrdersController(
            ICustomerRepository customerRepository, 
            IProductRepository productRepository, 
            IOrderRepository orderRepository,
            INewOrderNotificationService newOrderNotificationService
            )
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _newOrderNotificationService = newOrderNotificationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDTO dto)
        {
            var customer = await GetCustomer(dto);

            var orderModel = new Order(customer);

            orderModel.OrderProducts = new List<OrderProduct>();

            foreach(var productDTO in dto.products)
            {
                var product = await _productRepository.GetByID(Guid.Parse(productDTO.productId));
                orderModel.OrderProducts.Add(new OrderProduct(Guid.Parse(productDTO.productId), productDTO.quantity, orderModel.ID));
            }

            await _orderRepository.Add(orderModel);

            var notification = new NewOrderNotification(orderModel.ID, null);
            await _newOrderNotificationService.AddNotification(notification);

            return Ok();
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

            await _newOrderNotificationService.RemoveNotificationFromOrder(orderIdGuid);

            return Ok();
        }
    }
}
