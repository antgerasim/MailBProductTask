using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace MailBProductTask.ViewModels
{
    
    public class ResponseBad<T> : IResponse 
    {
        public ResponseBad(int statusCode, T message) 
        {
            StatusCode = statusCode;
            Message = message;
        }       
        public T Message { get; set; }
        public int StatusCode { get; set; }
    }
}
