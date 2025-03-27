using Microsoft.AspNetCore.Mvc;
using Notes.Models.Entity;
using Notes.Models.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.ApiRepository.IApiRepository
{
    public interface ICategoryApiRepository : IBaseApiRepository<Category>
    {
        void SearchForId(long? id);
        IEnumerable<CategoryDTO> GetCategory();
        void AddCategory([FromBody] CategoryDTO categoryDTO);
        void DeleteCategory(Category category);
        void UpdateCategory([FromBody] CategoryDTO categoryDTO);

    }
}
