namespace MailBProductTask.Models
{
    public class MailBOptions
    {
        public MailBOptions()
        {
            StorageFilePath = "c:\\temp";
            StorageFileName = "MailBProductTask.txt";
        }

        public string StorageFilePath { get; set; }
        public string StorageFileName { get; set; }
    }
}