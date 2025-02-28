using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models.Entity
{
    public class NotesProduct : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; }

        public long CategoryId { get; set; } // ключ для столбца для связывания в Product
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }//явно связываем 

    }
}
