using ECommerce.Api.Application.Features.Queries.Product.GetProducts;
using ECommerce.Common.Models.RequestModels.ShoppingCart;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.WebAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {

        private readonly IMediator madiator;

        public ShoppingCartController(IMediator mediator)
        {
            this.madiator = mediator;
        }


        [HttpPost]
        [Route("addcartproduct")]
        public async Task<IActionResult> GetProducts([FromBody]AddCartCommand addCartCommand)
        {
            var res = await madiator.Send(addCartCommand);
            return Ok(res);
        }

    }
}
