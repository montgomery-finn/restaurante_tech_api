using System.ComponentModel.DataAnnotations;

namespace restaurante_tech_api.DTOs
{
    public class CreateUserDTO
    {
        [Required, MinLength(3)]
        public string name { get; set; }

        [Required, EmailAddress]
        public string email { get; set; }

        [Required, MinLength(6)]
        public string password { get; set; }
    }
}
