
using MailBProductTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
    public class ClientService : IClientService
    {
        private List<Client> _clients = new List<Client>
        {
            new Client { Id = 1, },
            new Client { Id = 2, },
            new Client { Id = 3, },
            new Client { Id = 4, },
            new Client { Id = 5, },
            new Client { Id = 6, },
            new Client { Id = 7, },
            new Client { Id = 8, },
            new Client { Id = 9, },
            new Client { Id = 10, },
        };

        public async Task<bool> Authenticate(long clientId)
        {
            //return await Task.Run(() => GetClient(clientId));

            return await Task.Run(() => IsClientIdOdd(clientId));

        }

        private Client GetClient(long clientId)
        {
            return _clients.SingleOrDefault(client => client.Id == clientId);
                
        }

        private static bool IsClientIdEven(long clientId)
        {
            if (clientId % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
