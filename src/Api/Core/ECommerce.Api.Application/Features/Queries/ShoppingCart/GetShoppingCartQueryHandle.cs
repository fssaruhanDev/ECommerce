using AutoMapper;
using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Api.Domain.Models;
using ECommerce.Common.Events.Product;
using ECommerce.Common.Events.ShoppingCart.GetShoppingCart;
using ECommerce.Common.Models.Queries;
using ECommerce.Infrastructure.Persistence.Exeptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Features.Queries.ShoppingCart;

public class GetShoppingCartQueryHandle : IRequestHandler<GetShoppingCartQuery, GetShoppingCartViewModel>
{

    private readonly IMapper mapper;
    private readonly IShoppingCartRepository shoppingCartRepository;

    public GetShoppingCartQueryHandle(IMapper mapper, IShoppingCartRepository shoppingCartRepository)
    {
        this.mapper = mapper;
        this.shoppingCartRepository = shoppingCartRepository;
    }

    public async Task<GetShoppingCartViewModel> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken)
    {

        var query = shoppingCartRepository.AsQueryable();

        query = query.Include(i => i.CartItems).ThenInclude(i => i.Product);

        var shoppingCart = await query.FirstOrDefaultAsync(x => x.UserID == request.UserId);

        if (shoppingCart is null)
            throw new DatabaseValidationException("Shopping Card Was Not Found");


        var itemList = shoppingCart.CartItems.Select(x => new CartItems()
        {
            Id = x.ID,
            Name = x.Product.Name,
            Price = x.Product.Price,
            Quantity = x.Quantity,
            Picture = x.Product.Picture,
            Size = x.Product.Size

        }).ToList();

        var result = mapper.Map<GetShoppingCartViewModel>(shoppingCart);
        result.Items = itemList;

        var totalPrice = 0.0;

        //Total Price her defasında hesaplanıyor çünkü eğer ürünün fiyatı değişirse ve sepette eski fiyatta kalırsa
        //karışıklık yaşanabilir.
        for (int i = 0; i < itemList.Count; i++)
        {
            totalPrice += itemList[i].Price * itemList[i].Quantity;
        }

        result.TotalPrice = totalPrice;
        result.Id = shoppingCart.ID;


        return result;
    }
}
