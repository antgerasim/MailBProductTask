using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailBProductTask.Helpers.Tweetbook.Contracts.V1;
using MailBProductTask.Models;
using MailBProductTask.Services;
using MailBProductTask.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MailBProductTask.Controllers.V1
{
    // [Route("api/v1/[controller]")]
    //[Route("api/v1/[controller]")]
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

        // GET: api/Product/5
        //[HttpGet("{id}", Name = "Get")]
        [AllowAnonymous]
        [HttpGet(ApiRoutes.Product.Get)] //api/v1//product/{Id}
        [ProducesResponseType(typeof(Product), 200)]//201?
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            var response = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description
            };

            return Ok(new Response<ProductResponse>(200, response));
        }

        // POST: api/Product
        //[HttpPost]
        [HttpPost(ApiRoutes.Product.Create)] //api/v1/products
        //[ProducesResponseType(typeof(Product), 200)]//201?
        [Consumes("text/product")]//https://stackoverflow.com/questions/51158971/restrict-accepted-media-types-in-asp-net-core-controller-action
        public async Task<IActionResult> Post(CreateProductRequest postRequest)
        {
            //var newProductId = 123;
            var product = new Product
            {
                // Id = newProductId,
                Name = postRequest.Name,
                Description = postRequest.Description
            };

            try
            {
                if (string.IsNullOrEmpty(product.Name))
                {
                    var message = "Не указано название продукта";
                    return Ok(new ResponseNoName(400, message));
                }

                var response = await _productService.CreateProductAsync(product);

                // var locationUri = _uriService.GetProductUri(product.Id.ToString());//static hlper method ?

                return Ok(new Response<ProductResponse>(201, response));
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return BadRequest(new ResponseNoName(400, message));
            }
            //return Created(locationUri, new Response<ProductResponse>(response));

        }

        //RAW Data Test
        [HttpPost]
        [Route("api/v1/products2")]
        public async Task<string> Post()
        {
            var retVal = await ReadRequestBodyStream();
            return retVal;
        }

        private async Task<string> ReadRequestBodyStream()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
