using MailBProductTask.Helpers.Tweetbook.Contracts.V1;
using MailBProductTask.Models;
using MailBProductTask.Services;
using MailBProductTask.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MailBProductTask.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUriService _uriService;

        public ProductController(IProductService productService, IUriService uriService)
        {
            _productService = productService;
            _uriService = uriService;
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Product.Get)] //api/v1//product/{Id}        
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            try
            {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound("Продукт не найден");
            }
            var response = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description
            };

            return Ok(new ResponseOk<ProductResponse>(200, response));
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return BadRequest(new ResponseBad(400, message));
            }
        }

        //RAW Data Content-Type: text/product;charset=UTF-8
        [HttpPost(ApiRoutes.Product.Create)]
        public async Task<IActionResult> Post()
        {
            var product = await _productService.ReadRequestBodyStream(Request.Body);

            try
            {
                if (string.IsNullOrEmpty(product.Name))
                {
                    var message = "Не указано название продукта";
                    return Ok(new ResponseBad(400, message));
                }

                var response = await _productService.CreateProductAsync(product);

                return Ok(new ResponseOk<ProductResponse>(201, response));
            }

            catch (Exception ex)
            {
                var message = ex.Message;
                return BadRequest(new ResponseBad(400, message));
            }
        }
    }
}