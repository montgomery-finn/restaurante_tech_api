using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace restaurante_tech_api.DTOs
{
    public class EditProductDTO
    {
        [Required]
        public string id { get; set; }

        [Required, MinLength(3)]
        public string name { get; set; }

        [Required, Range(0, double.MaxValue)]
        public double price { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int priceInPoints { get; set; }

        public string base64Image { get; set; }
    }
}
