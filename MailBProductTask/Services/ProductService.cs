using MailBProductTask.Models;
using MailBProductTask.ViewModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
    public class ProductService : IProductService
    {
        private readonly MailBOptions _snapshotOptions;

        public ProductService(IOptionsSnapshot<MailBOptions>
            snapshotOptionsAccessor)
        {
            _snapshotOptions = snapshotOptionsAccessor.Value;
        }

        public async Task<ProductResponse> CreateProductAsync(Product product)
        {
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
                products.Add(product);
            }
            else
            {
                lastProductId = products.OrderByDescending(p => p.Id).FirstOrDefault().Id;
                lastProductId++;
                product.Id = lastProductId;
                products.Add(product);
            }

            File.WriteAllText(storagePath, JsonConvert.SerializeObject(products));
            return new ProductResponse { Id = product.Id, Name = product.Name, Description = product.Description };
        }

        public async Task<Product> GetProductByIdAsync(int id)
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

            return products.SingleOrDefault(p => p.Id == id);
        }

        public async Task<Product> ReadRequestBodyStream(Stream requestBody)
        {
            using (var reader = new StreamReader(requestBody, Encoding.UTF8))
            {
                return JsonConvert.DeserializeObject<Product>(await reader.ReadToEndAsync());
            }
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