using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Notes.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }


        [DisplayName("Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Updated Date")]
        public DateTime? UpdatedDate { get; set; }

        public long CategoryId { get; set; } // ключ для столбца для связывания в Product
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }//явно связываем 

    }
}
