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
    [Route("products2"), AllowAnonymous]
    public class Products2Controller : Controller
    {
        private readonly ISaveFileFromBase64StringService _saveFileFromBase64StringService;
        private readonly IProductRepository _productRepository;

        public Products2Controller(ISaveFileFromBase64StringService saveFileFromBase64StringService,
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
    }
}
