using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurante_tech_api.DTOs
{
    public class CreateOrderDTO
    {
        public string cpf { get; set; }
        public List<ProductDTO> products { get; set; }
    }

    public class ProductDTO
    {
        public string productId { get; set; }
        public int quantity { get; set; }
    }
}
