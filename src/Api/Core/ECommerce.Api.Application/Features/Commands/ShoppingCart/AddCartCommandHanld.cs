using AutoMapper;
using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Api.Domain.Models;
using ECommerce.Common.Events.CartItem;
using ECommerce.Common.Models.RequestModels.ShoppingCart;
using ECommerce.Infrastructure.Persistence.Exeptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Features.Commands.ShoppingCart;

public class AddCartCommandHanld : IRequestHandler<AddCartCommand, Guid>
{
    private readonly IShoppingCartRepository shoppingCartRepository;
    private readonly ICartItemRepository cartItemRepository;
    private readonly IMapper mapper;

    public AddCartCommandHanld( IShoppingCartRepository shoppingCartRepository, ICartItemRepository cartItemRepository,IMapper mapper)
    {
        this.shoppingCartRepository = shoppingCartRepository;
        this.cartItemRepository = cartItemRepository;
        this.mapper = mapper;
    }

    public async Task<Guid> Handle(AddCartCommand request, CancellationToken cancellationToken)
    {

        var shoppingCart = await shoppingCartRepository.FindWithIncludesShoppingCart(request.UserId);



        if (shoppingCart  is null)
            throw new DatabaseValidationException("Shopping Cart was null");


        var existCartItem = shoppingCart.CartItems.FirstOrDefault(i=>i.ProductID == request.ProductId);

        if (existCartItem is not null)
        {
            AddCartITemModel model = new AddCartITemModel()
            {
                ProductId = request.ProductId,
                Quantity = existCartItem.Quantity +  request.Quantity,
                OrderID = existCartItem.Order.ID,
                ShoppingCartId = shoppingCart.ID
            };

            mapper.Map(model,existCartItem);

            var cartitem = await cartItemRepository.UpdateAsync(existCartItem);
        }
        else
        {
            AddCartITemModel model = new AddCartITemModel()
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                OrderID = Guid.Parse("386e8dd8-da6a-4225-bc8f-2a48d9ded8da"),
                ShoppingCartId = shoppingCart.ID
            };

            var cartItem = mapper.Map<CartItem>(model);

            var cartitem = await cartItemRepository.AddAsync(cartItem);
        }

        var name = shoppingCart.User.FirstName;


        return shoppingCart.UserID;
    }
}
