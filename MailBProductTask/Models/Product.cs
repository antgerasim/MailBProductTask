using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Models
{
    [Serializable]
    public class Product
    {
        [Key]
        public long Id { get; set; } // от 1 до long.MaxValue, автоинкремент, генерируется при добавлении нового продукта
        [MaxLength(200)]
        [Required]
        public string Name { get; set; } //не пустая строка, не более 200 символов
        [MaxLength(500)]
        public string Description { get; set; } 
    }
}
