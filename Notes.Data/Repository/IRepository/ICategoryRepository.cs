using Notes.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Repository.IRepository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        void Update(Category obj);
        List<Category> GetAllCategories();
        void UpdateCategry(Category obj);
        void DeleteCategory(Category obj);
        void AddCategory(Category obj);
        Category? EditCategory(long? id);
        Category? Get(Expression<Func<Category, bool>> filter, string? includeProperties = null);
    }
}
