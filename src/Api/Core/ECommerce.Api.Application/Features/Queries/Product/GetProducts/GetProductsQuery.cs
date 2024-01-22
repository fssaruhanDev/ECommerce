using ECommerce.Common.Events.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Features.Queries.Product.GetProducts;

public class GetProductsQuery : IRequest<List<GetProductsViewModel>>
{
}
