namespace MailBProductTask.ViewModels
{
    public class ResponseBad
    {
        public ResponseBad(int statusCode, string message) 
        {
            StatusCode = statusCode;
            Message = message;
        }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
