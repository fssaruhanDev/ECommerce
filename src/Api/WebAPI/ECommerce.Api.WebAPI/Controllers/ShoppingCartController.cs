using ECommerce.Api.Application.Features.Queries.ShoppingCart;
using ECommerce.Api.Domain.Models;
using ECommerce.Common.Models.RequestModels.ShoppingCart;
using ECommerce.Common.Models.RequestModels.ShoppingCart.CartItemDelete;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.WebAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> GetBuy(Guid shoppingCartId)
        {
            var res = await madiator.Send(new GetBuyShoppingCartCommand(shoppingCartId));
            return Ok(res);
        }

        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> GetDelete(Guid cartItemId)
        {
            var res = await madiator.Send(new CartItemDeleteCommand(cartItemId));
            return Ok(res);
        }

    }
}
