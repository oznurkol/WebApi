using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProje.Entities;
using WebApiProje.Models;

namespace WebApiProje.DataAccess
{
    public interface IProductDal : IEntityRepository<Product>
    {
        List<ProductModel> GetProductsWithDetails();
    }
}
