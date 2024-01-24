using System;
using AutoMapper;
using ECommerce.Api.Domain.Models;
using ECommerce.Common.Events.CartItem;
using ECommerce.Common.Events.ShoppingCart.AddShoppingCart;
using ECommerce.Common.Events.ShoppingCart.GetShoppingCart;
using ECommerce.Common.Models.Queries;
using ECommerce.Common.Models.Queries.ShoppingCart;
using ECommerce.Common.Models.RequestModels;

namespace ECommerce.Api.Application.Mapping;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<User, LoginUserViewModel>()
			.ReverseMap();

		CreateMap<CartItem, AddCartITemModel>()
			.ReverseMap();

		CreateMap<ShoppingCart,GetShoppingCartViewModel>() 
			.ReverseMap();

		CreateMap<AddCartOrder, Order>()
			.ReverseMap();

		CreateMap<AddCartViewModel,Product>() 
			.ReverseMap();

    }
}

