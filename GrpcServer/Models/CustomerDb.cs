using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Configuration;

namespace GrpcServer.Models

{
    public class CustomerDb : DbContext
    {
        private readonly IConfiguration _configuration;

        public CustomerDb(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySQL(_configuration.GetConnectionString("CustomerDb"));
        }

        public DbSet<Customer> Customers { get; set; }


        public DbSet<Sale> Sales { get; set; }

    }
}
