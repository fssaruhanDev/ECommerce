using ECommerce.Api.Application.Features.Queries.Order.GetOrderQuery;
using ECommerce.Api.Application.Features.Queries.Product.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.WebAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {

        private readonly IMediator madiator;

        public OrderController(IMediator mediator)
        {
            this.madiator = mediator;
        }


        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetProducts(Guid userId)
        {
            var res = await madiator.Send(new GetOrderQuery(userId));
            return Ok(res);
        }
    }
}
