using MailBProductTask.Models;
using MailBProductTask.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
    public class ProductService : IProductService
    {
        public async Task<ProductResponse> CreateProductAsync(Product product)
        {
            string pathToTheFile = @"c:\temp";
            var fName = "MailBProductTask.txt";
            CreateDirectory(pathToTheFile, fName);
            var cPath = Path.Combine(pathToTheFile, fName);
            var json = File.ReadAllText(cPath);
            var products = JsonConvert.DeserializeObject<List<Product>>(json);

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

            File.WriteAllText(cPath, JsonConvert.SerializeObject(products));
            return new ProductResponse { Id = product.Id, Name = product.Name, Description = product.Description };
            #region old



            /*
            string path = @"c:\temp\MyTest.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Hello");
                    sw.WriteLine("And");
                    sw.WriteLine("Welcome");
                }
            }

            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
            */

            /*
            var logPath = Path.GetTempFileName();
            using (var writer = File.CreateText(logPath))
            {
                writer.WriteLine("log message"); //or .Write(), if you wish
            }
            */
            //return await Task.Run(() => true);
            #endregion
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            string pathToTheFile = @"c:\temp";//Hot swap candidates
            var fName = "MailBProductTask.txt";
            CreateDirectory(pathToTheFile, fName);
            var cPath = Path.Combine(pathToTheFile, fName);
            var json = File.ReadAllText(cPath);
            var products = await Task.Run(() => JsonConvert.DeserializeObject<List<Product>>(json));
            if (products == null)
            {
                return new Product();
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

        private static void CreateDirectory(string fPath, string fName)
        {
            var targetDirectoryExists = Directory.Exists(fPath);

            if (!targetDirectoryExists)
            {
                Directory.CreateDirectory(fPath);
            }
            var combined = Path.Combine(fPath, fName);
            if (!File.Exists(combined))
            {
                File.CreateText(combined);
            }
        }


    }
}