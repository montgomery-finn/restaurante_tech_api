using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurante_tech_api.DTOs;
using restaurante_tech_api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurante_tech_api.Controllers
{
    [Route("products"), Authorize]
    public class ProductsController : Controller
    {
        private readonly ISaveFileFromBase64StringService _saveFileFromBase64StringService;
        private readonly IProductRepository _productRepository;

        public ProductsController(ISaveFileFromBase64StringService saveFileFromBase64StringService,
                                  IProductRepository productRepository)
        {
            _saveFileFromBase64StringService = saveFileFromBase64StringService;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<List<ProductModel>> Index()
        {
            return await _productRepository.GetAll();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateProductDTO createProductDTO)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var fileName = await _saveFileFromBase64StringService.Execute(createProductDTO.base64Image);

            var product = new ProductModel(createProductDTO.name, createProductDTO.price, createProductDTO.priceInPoints, fileName);

            await _productRepository.Add(product);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productRepository.GetByID(Guid.Parse(id));

            if (product == null)
                return NotFound();

            await _productRepository.Delete(product);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]EditProductDTO editProductDTO)
        {
            if(!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var product = await _productRepository.GetByID(Guid.Parse(editProductDTO.id));

            if (product == null)
                return NotFound();

            string fileName = null;

            if (!String.IsNullOrEmpty(editProductDTO.base64Image))
            {
                fileName = await _saveFileFromBase64StringService.Execute(editProductDTO.base64Image);
            }

            product.Update(editProductDTO.name, editProductDTO.price, editProductDTO.priceInPoints, fileName);

            await _productRepository.Update(product);

            return Ok();
        }
    }
}
