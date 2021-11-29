using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using restaurante_tech_api.DTOs;
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

        public OrdersController(ICustomerRepository customerRepository, IProductRepository productRepository, IOrderRepository orderRepository)
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
        public async Task<IActionResult> Create([FromBody] CreateOrderDTO dto)
        {
            var customer = await GetCustomer(dto);

            var orderModel = new OrderModel(customer, null);

            foreach(var productDTO in dto.products)
            {
                var product = await _productRepository.GetByID(Guid.Parse(productDTO.productId));
                orderModel.AddProduct(product, productDTO.quantity);
            }

            await _orderRepository.Add(orderModel);

            return Ok();
        }

        private async Task<CustomerModel> GetCustomer(CreateOrderDTO dto)
        {
            CustomerModel customer = null;

            if (!String.IsNullOrEmpty(dto.cpf))
            {
                customer = await _customerRepository.GetByCPF(dto.cpf);
            }

            return customer;
        }

        
    }
}
