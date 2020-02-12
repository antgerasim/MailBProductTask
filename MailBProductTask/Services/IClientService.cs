using MailBProductTask.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
    public interface IClientService
    {
        Task<bool> Authenticate(long id);
    }
}
