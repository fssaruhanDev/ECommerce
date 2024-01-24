
using ECommerce.Common.Models.Queries.ShoppingCart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common.Models.RequestModels.ShoppingCart;

public class AddCartCommand : IRequest<AddCartViewModel>
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
