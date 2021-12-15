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

            var sameCpfCustomer = await _customerRepository.GetByCPF(createCustomerDTO.cpf);

            if(sameCpfCustomer != null)
            {
                return StatusCode(400, "CPF já cadastrado");
            }

            var customer = new Customer(createCustomerDTO.cpf);

            await _customerRepository.Add(customer);

            return Ok();
        }

        [HttpGet, Route("points/{cpf}")]
        public async Task<IActionResult> GetPoints(string cpf)
        {
            var customer = await _customerRepository.GetByCPF(cpf);

            if(customer == null)
            {
                return NotFound("CPF não cadastrado");
            }

            return Ok(customer.Points);
        }
    }
}
