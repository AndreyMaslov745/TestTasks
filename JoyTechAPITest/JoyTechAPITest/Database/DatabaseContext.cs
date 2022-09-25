using JoyTechAPITest.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace JoyTechAPITest.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductInOrder> InOrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
