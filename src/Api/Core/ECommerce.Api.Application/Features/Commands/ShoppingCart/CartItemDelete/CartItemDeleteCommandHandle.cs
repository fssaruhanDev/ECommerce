using AutoMapper;
using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Common.Models.RequestModels.ShoppingCart.CartItemDelete;
using ECommerce.Infrastructure.Persistence.Exeptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Features.Commands.ShoppingCart.CartItemDelete;

public class CartItemDeleteCommandHandle : IRequestHandler<CartItemDeleteCommand, Guid>
{
    private readonly ICartItemRepository cartItemRepository;

    public CartItemDeleteCommandHandle(IShoppingCartRepository shoppingCartRepository, ICartItemRepository cartItemRepository, IMapper mapper, IOrderRepository orderRepository)
    {
        this.cartItemRepository = cartItemRepository;
    }
    public async Task<Guid> Handle(CartItemDeleteCommand request, CancellationToken cancellationToken)
    {

        var query = await cartItemRepository.DeleteAsync(request.cartItemId);

        if (query > 0)
            throw new DatabaseValidationException("An occurrent error");

        return request.cartItemId;
    }
}
