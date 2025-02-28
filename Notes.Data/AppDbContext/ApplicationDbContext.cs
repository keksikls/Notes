using Microsoft.EntityFrameworkCore;
using Notes.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
        {

        }

        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData
                (
                    new Category { Id = 1, Name = "LowPrior", DisplayOrder = 1 },
                    new Category { Id = 2, Name = "MedPrior", DisplayOrder = 2 },
                    new Category { Id = 3, Name = "HighPrior", DisplayOrder = 3 }
                );

        }
    }
}
