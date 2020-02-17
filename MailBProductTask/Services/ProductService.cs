using MailBProductTask.Helpers;
using MailBProductTask.Models;
using MailBProductTask.ViewModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
    public class ProductService : IProductService
    {
        private readonly MailBOptions _snapshotOptions;
        private readonly INameValidator _nameValidator;
        private readonly IDescriptionValidator _descriptionValidator;

        public ProductService(IOptionsSnapshot<MailBOptions>
            snapshotOptionsAccessor, INameValidator nameValidator,
            IDescriptionValidator descriptionValidator)
        {
            _snapshotOptions = snapshotOptionsAccessor.Value;
            _nameValidator = nameValidator;
            _descriptionValidator = descriptionValidator;
        }

        public async Task<IResponse> CreateProductAsync(ProductRequest productRequest)
        {
            var product = productRequest.MapToEntity();
            string storagePath = GetStoragePath();
            var json = default(string);
            // if file not exist
            try
            {
                json = File.ReadAllText(storagePath);
            }
            catch (Exception)
            {
                File.WriteAllText(storagePath, String.Empty);
            }

            json = File.ReadAllText(storagePath);
            var products = await Task.Run(() => JsonConvert.DeserializeObject<List<Product>>(json));

            var lastProductId = default(long);
            if (products == null)
            {
                products = new List<Product>();
                lastProductId = 1;
                product.Id = lastProductId;

                if (!await _nameValidator.ValidateAsync(product.Name) |
                    !await _descriptionValidator.ValidateAsync(product.Description))
                {
                    return new ResponseBad<string>(400, GetErrorMsg(product));
                }
                products.Add(product);
            }
            else
            {
                lastProductId = products.OrderByDescending(p => p.Id).FirstOrDefault().Id;
                lastProductId++;
                product.Id = lastProductId;

                if (!await _nameValidator.ValidateAsync(product.Name) |
                    !await _descriptionValidator.ValidateAsync(product.Description))
                {
                    return new ResponseBad<string>(400, GetErrorMsg(product));
                }
                products.Add(product);
            }
            File.WriteAllText(storagePath, JsonConvert.SerializeObject(products));
            return new ResponseOk<ProductResponse>(201, product.MapToResponse());
        }

        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            string storagePath = GetStoragePath();

            if (!File.Exists(storagePath))
            {
                return null;
            }

            var json = File.ReadAllText(storagePath);
            var products = await Task.Run(() => JsonConvert.DeserializeObject<List<Product>>(json));
            if (products == null)
            {
                return null;
            }
            return products.SingleOrDefault(p => p.Id == id).MapToResponse();
        }

        public async Task<ProductRequest> ReadRequestBodyStream(Stream requestBody)
        {
            using (var reader = new StreamReader(requestBody, Encoding.UTF8))
            {
                var readResult = await reader.ReadToEndAsync();
                var strArray = readResult.Split("~").Select(str => str.Substring(str.IndexOf("=") + 1)).ToArray();
                return new ProductRequest { Name = strArray[0], Description = strArray[1] };
            }
        }

        private static string GetErrorMsg(Product product)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(product);
            var sb = new StringBuilder();

            if (!Validator.TryValidateObject(product, context, results, true))
            {
                for (int i = 0; i < results.Count; i++)
                {
                    var error = results[i];
                    if (results.Count == 1)
                    {
                        sb.Append($"Поле: {error.MemberNames.FirstOrDefault()} - Ошибка: {error.ErrorMessage}");
                    }
                    else if (results.Count > 1 && i < results.Count - 1)
                    {
                        sb.Append($"Поле: {error.MemberNames.FirstOrDefault()} - Ошибка: {error.ErrorMessage}; ");
                    }
                    else
                    {
                        sb.Append($"Поле: {error.MemberNames.FirstOrDefault()} - Ошибка: {error.ErrorMessage}.");
                    }
                }
            }
            return sb.ToString();
        }

        private string GetStoragePath()
        {
            string fPath = _snapshotOptions.StorageFilePath;
            var fName = _snapshotOptions.StorageFileName;
            CreateDirectory(fPath);
            return Path.Combine(fPath, fName);
        }

        private static void CreateDirectory(string fPath)
        {
            if (!Directory.Exists(fPath))
            {
                Directory.CreateDirectory(fPath);
            }
        }
    }
}