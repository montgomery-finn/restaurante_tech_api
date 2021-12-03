using System;

namespace Domain.Models
{
    public class Customer
    {
        public Guid ID { get; set; }
        public string CPF { get; set; }

        public Customer() { }

        public Customer(string cpf)
        {
            ID = Guid.NewGuid();
            CPF = cpf;
        }
    }
}
