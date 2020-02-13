using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.ViewModels
{
    public class Response<T> where T : ProductResponse
    {

        public Response(int statusCode, T response)
        {
            StatusCode = statusCode;
            Data = response;
        }
        public T Data { get; set; }
        public int StatusCode { get; set; }
    }

    public class ResponseNoName
    {
        public ResponseNoName(int statusCode, string message) 
        {
            StatusCode = statusCode;
            Message = message;
        }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }


}
