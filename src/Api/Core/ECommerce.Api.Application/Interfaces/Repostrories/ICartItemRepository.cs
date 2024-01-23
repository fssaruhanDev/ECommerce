using ECommerce.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Interfaces.Repostrories;

public interface ICartItemRepository : IGenericRepository<CartItem>
{
}
