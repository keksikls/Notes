using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CategoryServices.ICategoryServices
{
    public interface ICategoryService
    {
        IEnumerable<SelectListItem> GetCategorySelectList();
    }
}
