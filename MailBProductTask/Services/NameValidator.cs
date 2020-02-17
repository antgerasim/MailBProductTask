using MailBProductTask.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
    public class NameValidator : INameValidator
    {
        public async Task<bool> ValidateAsync(string name)
        {
            return await Task.Run(() => IsValid(name));
        }

        private bool IsValid(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length > ValidationConsts.Name.StringLength)
            {
                return false;
            }
            return true;
        }
    }
}
