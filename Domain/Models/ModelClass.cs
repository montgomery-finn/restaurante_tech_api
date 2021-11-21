using Domain.Entities;
using System;

namespace Domain.Models
{
    public abstract class ModelClass
    {
        public abstract EntityClass ToEntity();
    }
}
