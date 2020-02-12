using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Models
{
    public class Client
    {
        [Key]        
        public long Id { get; set; } //от 1 до long.MaxValue
    }
}
