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

namespace ECommerce.Api.Application.Features.Queries.Product.GetProduct;

public class GetProductQueryHandle : IRequestHandler<GetProductQuery, GetProductViewModel>
{

    private readonly IProductRepository productRepository;

    public GetProductQueryHandle( IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }
    public async Task<GetProductViewModel> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var query = productRepository.AsQueryable();

        var list = query.Where(x => x.ID == request.ProductId).Select(i => new GetProductViewModel()
        {
            ID = i.ID,
            Barcode = i.Barcode,
            Company = i.Company,
            Description = i.Description,
            Name = i.Name,
            Picture = i.Picture,
            Price = i.Price,
            Size = i.Size,
            Stock = i.Stock
        });
        return await list.FirstOrDefaultAsync();
    }
}
