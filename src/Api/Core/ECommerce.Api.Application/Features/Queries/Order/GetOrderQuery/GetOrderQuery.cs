using ECommerce.Common.Events.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Features.Queries.Order.GetOrderQuery;

public class GetOrderQuery : IRequest<IQueryable<GetOrdersViewModel>>
{
    public Guid UserID { get; set; }

    public GetOrderQuery(Guid userID)
    {
        UserID = userID;
    }
}
