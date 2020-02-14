using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.ViewModels
{
    public class ResponseOk<T> where T : ProductResponse
    {
        public ResponseOk(int statusCode, T response)
        {
            StatusCode = statusCode;
            Data = response;
        }
        public T Data { get; set; }
        public int StatusCode { get; set; }
    }
}
