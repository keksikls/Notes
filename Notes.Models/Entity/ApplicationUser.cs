using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models.Entity
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? MidleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        public string? Phone { get; set; }
        public DateTime? BirthDate { get; set; }
 

    }
}
