using ECommerce.Api.Application.Features.Queries.Product.GetProduct;
using ECommerce.Api.Application.Features.Queries.Product.GetProducts;
using ECommerce.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.WebAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator madiator;

        public ProductController(IMediator mediator)
        {
            this.madiator = mediator;
        }


        [HttpGet]
        [Route("getproducts")]
        public async Task<IActionResult> GetProducts([FromQuery]GetProductsQuery getProductsQuery)
        {
            var res = await madiator.Send(getProductsQuery);
            return Ok(res);
        }

        [HttpGet]
        [Route("detail")]
        public async Task<IActionResult> GetProduct(Guid productID)
        {
            var res = await madiator.Send(new GetProductQuery(productID));
            return Ok(res);
        }
    }
}
