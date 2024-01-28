using AutoMapper;
using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Api.Domain.Models;
using ECommerce.Common.Events.CartItem;
using ECommerce.Common.Events.ShoppingCart.AddShoppingCart;
using ECommerce.Common.Models.Queries.ShoppingCart;
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

public class AddCartCommandHandle : IRequestHandler<AddCartCommand, AddCartViewModel>
{
    private readonly IShoppingCartRepository shoppingCartRepository;
    private readonly ICartItemRepository cartItemRepository;
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public AddCartCommandHandle(IShoppingCartRepository shoppingCartRepository, ICartItemRepository cartItemRepository, IMapper mapper, IOrderRepository orderRepository)
    {
        this.shoppingCartRepository = shoppingCartRepository;
        this.cartItemRepository = cartItemRepository;
        this.mapper = mapper;
        this.orderRepository = orderRepository;
    }

    public async Task<AddCartViewModel> Handle(AddCartCommand request, CancellationToken cancellationToken)
    {
        var dbShoppingCart = await shoppingCartRepository.FindWithIncludesShoppingCart(request.UserId);

        

        if (dbShoppingCart is null)
        {

            dbShoppingCart = new Domain.Models.ShoppingCart
            {
                UserID = request.UserId,

            };
            await shoppingCartRepository.AddAsync(dbShoppingCart);
            dbShoppingCart = await shoppingCartRepository.FindWithIncludesShoppingCart(request.UserId);
        }

        var existingCartItem = dbShoppingCart.CartItems.FirstOrDefault(i => i.ProductID == request.ProductId);

        if (existingCartItem is not null)
        {
            existingCartItem.Quantity += request.Quantity;
            await cartItemRepository.UpdateAsync(existingCartItem);
        }
        else
        {
            var addCartOrder = new AddCartOrder { UserID = request.UserId };
            var orderItem = mapper.Map<Order>(addCartOrder);
            await orderRepository.AddAsync(orderItem);

            var order = orderRepository.AsQueryable();

            var dbOrder = await order.Where(x => x.UserID == request.UserId)
                         .OrderByDescending(x => x.CreateDate)
                         .FirstOrDefaultAsync();

            var newCartItemModel = new AddCartITemModel
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                OrderID = dbOrder.ID,
                ShoppingCartId = dbShoppingCart.ID
            };

            var newCartItem = mapper.Map<CartItem>(newCartItemModel);
            await cartItemRepository.AddAsync(newCartItem);
        }

        var product = cartItemRepository.AsQueryable();


        var returnModel = existingCartItem != null
            ? mapper.Map<AddCartViewModel>(existingCartItem.Product)
            : mapper.Map<AddCartViewModel>(product.Include(i=>i.Product).FirstOrDefaultAsync(x => x.ProductID == request.ProductId).GetAwaiter().GetResult().Product);

        return returnModel;
    

    }
}
