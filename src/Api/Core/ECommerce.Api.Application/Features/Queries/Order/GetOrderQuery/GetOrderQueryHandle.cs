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

public class GetOrderQueryHandle : IRequestHandler<GetOrderQuery, IQueryable< GetOrdersViewModel>>
{

    private readonly IOrderRepository orderRepository;
    private readonly IShoppingCartRepository shoppingCartRepository;

    public GetOrderQueryHandle(IOrderRepository orderRepository, IShoppingCartRepository shoppingCartRepository)
    {
        this.orderRepository = orderRepository;
        this.shoppingCartRepository = shoppingCartRepository;
    }

    public IShoppingCartRepository ShoppingCartRepository => shoppingCartRepository;

    public async Task<IQueryable< GetOrdersViewModel>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = orderRepository.AsQueryable();
        var shoppingCart = shoppingCartRepository.AsQueryable();


        order = order.Include(i => i.User)
                     .Include(i=>i.CartItems)
                     .Include(i => i.CartItems)
                        .ThenInclude(i => i.Product)
                     .Include(i => i.CartItems)
                        .ThenInclude(i => i.ShoppingCart);

        var orders = order.Where(x => x.UserID == request.UserID);

        var dbOrder = orders.Select(i=> new GetOrdersViewModel()
        {
            OrderId = i.ID,
            Items = i.CartItems.Select(x => new OrderCartItems()
                                        {
                                            Id = x.ID,
                                            Name = x.Product.Name,
                                            Picture = x.Product.Picture,
                                            Price = x.Product.Price,
                                            Quantity = x.Quantity,
                                            Size = x.Product.Size,
                                            TotalPrice = x.Quantity * x.Product.Price
                                        }).ToList(),
        });


        return  dbOrder;
    }
}
