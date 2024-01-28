using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common.Models.RequestModels.ShoppingCart.CartItemDelete;

public class CartItemDeleteCommand : IRequest<Guid>
{
    public Guid cartItemId { get; set; }

    public CartItemDeleteCommand(Guid cartItemId)
    {
        this.cartItemId = cartItemId;
    }
}
