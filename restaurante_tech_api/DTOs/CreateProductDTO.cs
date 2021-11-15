using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace restaurante_tech_api.DTOs
{
    public class CreateProductDTO
    {
        [Required, MinLength(3)]
        public string name { get; set; }

        [Required, Range(0, double.MaxValue)]
        public double price { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int priceInPoints { get; set; }

        [Required]
        public string base64Image { get; set; }
    }
}
