using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class TechContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=127.0.0.1,1433; Database=RestauranteTech; User Id=SA; Password=Jko3va-D9821jhsvGD;");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
