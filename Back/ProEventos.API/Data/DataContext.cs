using Microsoft.EntityFrameworkCore;
using ProEventos.API.Models;

namespace ProEventos.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

         //   modelBuilder.Entity<Customer>().HasData(getCustomerSeed());
        //    modelBuilder.Entity<Product>().HasData(getProductSeed());
        }

        public DbSet<Evento> Eventos { get; set; }
    }
}
