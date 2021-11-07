
using Domain.Entities;
using System;

namespace Domain.Models
{
    public class UserModel : ModelClass
    {
        public Guid ID { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string EncodedPassword { get; private set; }

        public UserModel(string name, string email, string encodedPassword, Guid? id = null)
        {
            ID = id ?? Guid.NewGuid();
            Name = name;
            Email = email;
            EncodedPassword = encodedPassword;
        }

        public override UserEntity ToEntity()
        {
            return new UserEntity(Name, Email, EncodedPassword, ID);
        }
    }
}
