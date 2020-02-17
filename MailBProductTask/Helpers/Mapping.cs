using MailBProductTask.Models;
using MailBProductTask.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Helpers
{
    public static class Mapping
    {
        public static ProductResponse MapToResponse(this Product product)
        {
            return product == null ? null : new ProductResponse { Id = product.Id, Name = product.Name, Description = product.Description };
        }

        public static Product MapToEntity(this ProductRequest productRequest)
        {
            return productRequest == null ? null : new Product { Name = productRequest.Name, Description = productRequest.Description };
        }
    }
}
