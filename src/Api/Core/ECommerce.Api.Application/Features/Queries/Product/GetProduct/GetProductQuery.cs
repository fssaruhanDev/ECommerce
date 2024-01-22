using ECommerce.Common.Events.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Features.Queries.Product.GetProduct;

public class GetProductQuery : IRequest<GetProductViewModel>
{

    public Guid ProductId { get; set; }
    public GetProductQuery(Guid productId)
    {
        ProductId = productId;
    }
}
