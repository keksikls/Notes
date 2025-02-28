using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notes.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models.ViewModel
{
    public class NotesProductVM
    {
        public NotesProduct? NotesProduct { get; set; }
        [Required]
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
