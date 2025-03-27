using Microsoft.AspNetCore.Mvc;
using Notes.Data.ApiRepository.IApiRepository;
using Notes.Data.AppDbContext;
using Notes.Models.Entity;
using Notes.Models.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.ApiRepository
{
    public class CategoryApiRepository : BaseApiRepository<Category>, ICategoryApiRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryApiRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void AddCategory([FromBody] CategoryDTO categoryDTO)
        {
            Category model = new()
            {
                //todo замапить очень дохуя писать потому что 
                Name = categoryDTO.Name,
                DisplayOrder = categoryDTO.DisplayOrder,
                Id = categoryDTO.Id
            };
            _context.Categories.Add(model);
            if (model != null)
            {
                _context.SaveChanges();
            }
        }

        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
        }

        public IEnumerable<CategoryDTO> GetCategory()
        {
            return _context.Categories.ToList();
        }

        public void SearchForId(long? id)
        {
            _context.Categories.FirstOrDefault(u=>u.Id == id);
        }

        public void UpdateCategory([FromBody] CategoryDTO categoryDTO)
        {
            Category model = new()
            {
                //todo замапить очень дохуя писать потому что 
                Name = categoryDTO.Name,
                DisplayOrder = categoryDTO.DisplayOrder,
                Id = categoryDTO.Id
            };

            _context.Categories.Update(model);
            if (model != null)
            {
                _context.SaveChanges();
            }
        }
    }
}
