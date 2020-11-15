using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProje.Entities;

namespace WebApiProje.DataAccess
{
    public class NorthwindContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=OZNUR-PC\SQLEXPRESS02;Database=Northwind;Integrated Security=false");
            optionsBuilder.UseSqlServer("Server=OZNUR-PC\\SQLEXPRESS02; Database=Northwind;Integrated Security=SSPI", option => {
                option.EnableRetryOnFailure();
            });
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
