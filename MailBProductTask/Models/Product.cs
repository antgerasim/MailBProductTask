using MailBProductTask.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Models
{
    //[Serializable]
    public class Product
    {
        [Range(1, long.MaxValue, ErrorMessage = "Только положительные числа, не превышающие 9223372036854775807 допустимы.")]       
        public long Id { get; set; } 
        [StringLength(ValidationConsts.Name.StringLength, ErrorMessage = "Количество символов в строке превышает 200")]
        [Required(ErrorMessage = "Не указано название продукта")]        
        public string Name { get; set; }
        [StringLength(ValidationConsts.Description.StringLength, ErrorMessage = "Количество символов в строке превышает 500")]       
        public string Description { get; set; }
    }
}
