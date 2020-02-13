using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
    public interface IUriService
    {
        Uri GetProductUri(string postId);
    }
}
