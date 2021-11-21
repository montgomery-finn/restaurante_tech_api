using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CustomerModel : ModelClass
    {
        public Guid ID { get; set; }
        public string CPF { get; set; }

        public CustomerModel(string cpf, Guid? id = null)
        {
            ID = id ?? Guid.NewGuid();
            CPF = cpf;
        }

        public override CustomerEntity ToEntity()
        {
            return new CustomerEntity(CPF, ID);
        }
    }
}
