using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using restaurante_tech_api.DTOs;
using restaurante_tech_api.Services.Interfaces;
using System.Threading.Tasks;

namespace restaurante_tech_api.Controllers
{
    [Route("customers"), AllowAnonymous]
    public class CustomersController : Controller
    {
        private readonly ISaveFileFromBase64StringService _saveFileFromBase64StringService;
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDTO createCustomerDTO)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var customer = new Customer(createCustomerDTO.cpf);

            await _customerRepository.Add(customer);

            return Ok();
        }
    }
}
