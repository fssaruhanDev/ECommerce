using AutoMapper;
using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Common.Models.RequestModels.ShoppingCart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Features.Commands.ShoppingCart;

public class GetBuyShoppingCartCommandHandle : IRequestHandler<GetBuyShoppingCartCommand, Guid>
{

    private readonly IShoppingCartRepository shoppingCartRepository;
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public GetBuyShoppingCartCommandHandle(IShoppingCartRepository shoppingCartRepository, IOrderRepository orderRepository, IMapper mapper)
    {
        this.shoppingCartRepository = shoppingCartRepository;
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    public async Task<Guid> Handle(GetBuyShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var shoppingCart = await shoppingCartRepository.GetByIdAsync(request.shoppingCartId);
        /// Satın alma kontrolleri yapılıp satın alma işleminin yapıldığından emin olduktan sonra.
        shoppingCart.IsActive = false;
  
        await shoppingCartRepository.UpdateAsync(shoppingCart);

        return shoppingCart.ID;
    }
}
