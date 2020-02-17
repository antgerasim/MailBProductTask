using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
   public interface INameValidator
    {
        Task<bool> ValidateAsync(string name);
    }
}
