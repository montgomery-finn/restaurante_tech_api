using System;

namespace Domain.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EncodedPassword { get; set; }

        public User() { }

        public User(string name, string email, string encodedPassword)
        {
            ID = Guid.NewGuid();
            Name = name;
            Email = email;
            EncodedPassword = encodedPassword;
        }

    }
}
