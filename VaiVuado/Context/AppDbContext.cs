using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VaiVuado.Model;

namespace VaiVuado.Context
{
    public class AppDbContext : DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //person
        public DbSet<Person> Person { get; set; }
        //address
        public DbSet<Address> Address { get; set; }
        //business
        public DbSet<Business> Business { get; set; }
        //client
        public DbSet<Client> Client { get; set; }
        public DbSet<ReturnClientInfo> ReturnClientInfos { get; set; }
        public DbSet<ReturnClientInfoAll> ReturnClientInfoAlls { get; set; }
        //employee
        public DbSet<Employee> Employee { get; set; }
        //product
        public DbSet<Product> Product { get; set; }
        //productStock
        public DbSet<ProductStock> ProductStock { get; set; }
        //productHistory
        public DbSet<ProductHistory> ProductHistory { get; set; }
        //Route
        public DbSet<Route> Route { get; set; }
        //Delivery
        public DbSet<Delivery> Delivery { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ServerConnection"));
        }
    }
}
