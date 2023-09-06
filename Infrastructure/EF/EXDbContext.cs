using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF
{
    public class EXDbContext: DbContext
    {
        public EXDbContext(DbContextOptions<EXDbContext> options) : base(options)
        {
        }

        public DbSet<Accounts> accounts { get; set; }
        public DbSet<Category> categories { get; set; }

        public DbSet<Product> products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>()
                .HasKey(a => a.AccountID);
        }
        public DbSet<Employee> employees { get; set; }

        public DbSet<Promotion> promotions { get; set; }
        public DbSet<Order> orders { get; set; }

        public DbSet<OrderDetail> orderDetails { get; set; }

        public DbSet<Customer> customers { get; set; }
        public DbSet<CheckOut> checkOuts { get; set; }
    }
}
