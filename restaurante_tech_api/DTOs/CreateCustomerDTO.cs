using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace restaurante_tech_api.DTOs
{
    public class CreateCustomerDTO
    {
        [Required, MinLength(11), MaxLength(11)]
        public string cpf { get; set; }

    }
}
