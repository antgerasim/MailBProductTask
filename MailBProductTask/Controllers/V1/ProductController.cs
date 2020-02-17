using MailBProductTask.Helpers.Tweetbook.Contracts.V1;
using MailBProductTask.Services;
using MailBProductTask.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MailBProductTask.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Product.Get)]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            try
            {
                var productResponseVm = await _productService.GetProductByIdAsync(id);

                if (productResponseVm == null)
                {
                    return NotFound("Продукт не найден");
                }

                return Ok(new ResponseOk<ProductResponseVm>(200, productResponseVm));
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return BadRequest(new ResponseBad<string>(400, message));
            }
        }

        //RAW Data Content-Type: text/product;charset=UTF-8
        [HttpPost(ApiRoutes.Product.Create)]
        public async Task<IActionResult> Post()
        {
            var productRequestVm = await _productService.ReadRequestBodyStream(Request);
            try
            {
                var productResponseVm = await _productService.CreateProductAsync(productRequestVm);
                return Ok(productResponseVm);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return BadRequest(new ResponseBad<string>(400, message));
            }
        }
    }
}