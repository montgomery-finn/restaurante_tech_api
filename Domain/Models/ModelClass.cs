using Domain.Entities;

namespace Domain.Models
{
    public abstract class ModelClass
    {
        public abstract EntityClass ToEntity();
    }
}
