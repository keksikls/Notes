using Notes.Data.AppDbContext;
using Notes.Data.Repository.IRepository;
using Notes.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        void ICategoryRepository.Update(Category obj)
        {
            _context.Categories.Update(obj);
        }

    }
}
