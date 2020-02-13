
using MailBProductTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
    public class ClientService : IClientService
    {
        public async Task<bool> Authenticate(long clientId)
        {
            return await Task.Run(() => IsClientIdOdd(clientId));
        }

        private static bool IsClientIdOdd(long clientId)
        {
            if (clientId % 2 != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
