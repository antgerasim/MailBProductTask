using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
    public interface IDescriptionValidator
    {
        Task<bool> ValidateAsync(string description);
    }
}
