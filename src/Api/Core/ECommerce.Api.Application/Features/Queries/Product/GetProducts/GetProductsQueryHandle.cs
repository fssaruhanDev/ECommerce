using AutoMapper;
using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Common.Events.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Features.Queries.Product.GetProducts;

public class GetProductsQueryHandle : IRequestHandler<GetProductsQuery, List<GetProductsViewModel>>
{


    private readonly IMapper mapper;
    private readonly IProductRepository productRepository;

    public GetProductsQueryHandle(IMapper mapper, IProductRepository productRepository)
    {
        this.mapper = mapper;
        this.productRepository = productRepository;
    }

    public async Task<List<GetProductsViewModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = productRepository.AsQueryable();

        var list = query.Where(x => x.IsActive == true).Select(i => new GetProductsViewModel()
        {
            ID = i.ID,
            Name = i.Name,
            Picture = i.Picture,
            Description = i.Description,
            Price= i.Price,
        }).ToList();
        return list;
    }
}
