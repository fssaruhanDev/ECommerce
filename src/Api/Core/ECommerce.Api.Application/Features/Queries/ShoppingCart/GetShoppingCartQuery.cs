using ECommerce.Common.Events.ShoppingCart.GetShoppingCart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Features.Queries.ShoppingCart;

public class GetShoppingCartQuery : IRequest<GetShoppingCartViewModel>
{
    public Guid UserId { get; set; }

    public GetShoppingCartQuery(Guid userId)
    {
        UserId = userId;
    }
}
