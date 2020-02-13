using MailBProductTask.Models;
using MailBProductTask.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            return new ProductResponse { Id = product.Id,  Name = product.Name, Description = product.Description};
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
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var retVal = await Task.Run(() => new Product { Id = id, Name = "StubName", Description = "StubDescription" });
            return retVal;
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

        //public static void SaveToTxt()
        //{
        //    using (TextWriter tw = new StreamWriter(Path))
        //    {
        //        foreach (var item in Data.List)
        //        {
        //            tw.WriteLine(string.Format("Item: {0} - Cost: {1}", item.Name, item.Cost.ToString()));
        //        }
        //    }
        //}
    }
}