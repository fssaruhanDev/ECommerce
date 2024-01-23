
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common.Models.RequestModels.ShoppingCart;

public class GetBuyShoppingCartCommand : IRequest<Guid>
{
    public GetBuyShoppingCartCommand(Guid shoppingCartId)
    {
        this.shoppingCartId = shoppingCartId;
    }

    public Guid shoppingCartId { get; set; }
}
