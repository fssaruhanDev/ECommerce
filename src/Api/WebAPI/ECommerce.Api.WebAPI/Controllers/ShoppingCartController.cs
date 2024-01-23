using ECommerce.Api.Application.Features.Queries.ShoppingCart;
using ECommerce.Api.Domain.Models;
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
        public async Task<IActionResult> PostAddCart([FromBody]AddCartCommand addCartCommand)
        {
            var res = await madiator.Send(addCartCommand);
            return Ok(res);
        }


        [HttpGet]
        [Route("getsoppingcart")]
        public async Task<IActionResult> GetProducts(Guid userId)
        {
            var res = await madiator.Send(new GetShoppingCartQuery(userId));
            return Ok(res);
        }


        [HttpGet]
        [Route("buy")]
        public async Task<IActionResult> GetBut(Guid shoppingCartID)
        {
            var res = await madiator.Send(new GetBuyShoppingCartCommand(shoppingCartID));
            return Ok(res);
        }

    }
}
