﻿using FreeBilling.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreeBilling.Web.Data
{
   public class BillingContext: DbContext
    {
        private readonly IConfiguration _config;

        public BillingContext(IConfiguration config)
        {
            _config = config;
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<TimeBill> TimeBills => Set<TimeBill>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connectionString = _config["Connectionstrings:BillingDb"];

            optionsBuilder.UseSqlServer(connectionString);
            

        }

    }
}
