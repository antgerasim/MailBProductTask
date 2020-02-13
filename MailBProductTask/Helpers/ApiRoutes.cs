using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Helpers
{
    namespace Tweetbook.Contracts.V1
    {
        public static class ApiRoutes
        {
            public const string Root = "api";

            public const string Version = "v1";

            public const string Base = Root + "/" + Version;

            public static class Product
            {   
                public const string Get = Base + "/product/{Id}"; //Get
                public const string Create = Base + "/products"; //Post
            }
        }
    }
}
