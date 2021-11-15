using System.ComponentModel.DataAnnotations;

namespace restaurante_tech_api.DTOs
{
    public class AuthorizeDTO
    {
        [Required, EmailAddress]
        public string email { get; set; }

        public string password { get; set; }
    }
}
