using Microsoft.EntityFrameworkCore;
using Notes.Data.AppDbContext;
using Notes.Data.Repository.IRepository;
using Notes.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public void AddCategory(Category obj)
        {
            _context.Categories.Add(obj);
            if (obj != null)
            {
                _context.SaveChanges();
            }
        }

        public void DeleteCategory(Category obj)
        {
            _context.Categories.Remove(obj);
            if (obj != null)
            {
                _context.SaveChanges();
            }
        }

        public Category? EditCategory(long? id)
        {
            var category = Get(u=>u.Id == id);
            return category;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public void UpdateCategry(Category obj)
        {
            _context.Categories.Update(obj);
            if (obj != null)
            {
                _context.SaveChanges();
            }
        }

        void ICategoryRepository.Update(Category obj)
        {
            throw new NotImplementedException();
        }
    }
}
