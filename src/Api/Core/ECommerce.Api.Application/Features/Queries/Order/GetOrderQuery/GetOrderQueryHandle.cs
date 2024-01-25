using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Common.Events.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Features.Queries.Order.GetOrderQuery;

public class GetOrderQueryHandle : IRequestHandler<GetOrderQuery, List<GetOrdersViewModel>>
{

    private readonly IOrderRepository orderRepository;
    private readonly IShoppingCartRepository shoppingCartRepository;

    public GetOrderQueryHandle(IOrderRepository orderRepository, IShoppingCartRepository shoppingCartRepository)
    {
        this.orderRepository = orderRepository;
        this.shoppingCartRepository = shoppingCartRepository;
    }

    public IShoppingCartRepository ShoppingCartRepository => shoppingCartRepository;

    public async Task<List<GetOrdersViewModel>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {

        var order = orderRepository.AsQueryable();
        var shoppingCart = shoppingCartRepository.AsQueryable();

        order = order
            .Include(i => i.User)
            .Include(i => i.CartItems)
            .ThenInclude(i => i.Product)
            .Include(i => i.CartItems)
            .ThenInclude(i => i.ShoppingCart);

        var shoppingCartItem = await shoppingCart.FirstOrDefaultAsync(x => x.UserID == request.UserID);

        var orders = order.Where(x => x.UserID == request.UserID);

        if (shoppingCartItem != null)
        {
            orders = orders.Where(x => x.CartItems.All(ci => ci.ShoppingCartID != shoppingCartItem.ID));
        }

        var groupedOrders = orders
            .GroupBy(i => i.CartItems.FirstOrDefault().ShoppingCartID)
            .Select(group => new GetOrdersViewModel
            {
                ShoppingCartID = group.Key,
                Items = group.SelectMany(i => i.CartItems)
                             .Select(x => new OrderCartItems
                             {
                                 Id = x.ID,
                                 Name = x.Product.Name,
                                 Picture = x.Product.Picture,
                                 Price = x.Product.Price,
                                 Quantity = x.Quantity,
                                 OrderID = x.Order.ID,
                                 Size = x.Product.Size,
                                 TotalPrice = x.Quantity * x.Product.Price
                             })
                             .ToList()
            })
            .ToList();

        var totalPrice = 0.00;
        foreach (var group in groupedOrders)
        {
            foreach (var item in group.Items)
            {
                totalPrice += item.TotalPrice;
            }

            group.TotalPrice = totalPrice;

        }
        return groupedOrders;

      
    }
}
