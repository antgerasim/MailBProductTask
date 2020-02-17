using MailBProductTask.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Services
{
    public class DescriptionValidator : IDescriptionValidator
    {
        public async Task<bool> ValidateAsync(string description)
        {
            return await Task.Run(() => IsValid(description));
        }

        private bool IsValid(string description)
        {
            if (string.IsNullOrEmpty(description)  || description.Length > ValidationConsts.Description.StringLength)
            {
                return false;
            }
            return true;
        }
    }
}
