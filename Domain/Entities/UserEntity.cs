using Domain.Models;
using System;

namespace Domain.Entities
{
    public class UserEntity : EntityClass
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EncodedPassword { get; set; }

        public UserEntity() { }

        public UserEntity(string name, string email, string encodedPassword, Guid id)
        {
            ID = id;
            Name = name;
            Email = email;
            EncodedPassword = encodedPassword;
        }

        public override UserModel ToModel()
        {
            return new UserModel(Name, Email, EncodedPassword, ID);
        }
    }
}
