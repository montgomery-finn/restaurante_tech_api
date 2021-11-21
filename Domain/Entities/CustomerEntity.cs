using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomerEntity : EntityClass
    {
        public Guid ID { get; set; }
        public string CPF { get; set; }

        public CustomerEntity() { }

        public CustomerEntity(string cpf, Guid id)
        {
            ID = id;
            CPF = cpf;
        }

        public override CustomerModel ToModel()
        {
            return new CustomerModel(CPF, ID);
        }
    }
}
