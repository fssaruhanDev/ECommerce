using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Api.Domain.Models;
using ECommerce.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Persistence.Repostory;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(EntityContext dbContext) : base(dbContext)
    {
    }
}
