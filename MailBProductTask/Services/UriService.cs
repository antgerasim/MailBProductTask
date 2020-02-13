using MailBProductTask.Helpers.Tweetbook.Contracts.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetProductUri(string Id)
        {
            return new Uri(_baseUri + ApiRoutes.Product.Get.Replace("{Id}", Id));
        }
    }
}
