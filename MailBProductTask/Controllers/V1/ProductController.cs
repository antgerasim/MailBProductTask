using MailBProductTask.Helpers;
using MailBProductTask.Helpers.Tweetbook.Contracts.V1;
using MailBProductTask.Models;
using MailBProductTask.Services;
using MailBProductTask.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
                var productResp = await _productService.GetProductByIdAsync(id);

                if (productResp == null)
                {
                    return NotFound("Продукт не найден");
                }

                return Ok(new ResponseOk<ProductResponse>(200, productResp));
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
            var product = await _productService.ReadRequestBodyStream(Request.Body);

            try
            {
                var response = await _productService.CreateProductAsync(product);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return BadRequest(new ResponseBad<string>(400, message));
            }
        }
    }
}