using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Data.UnitOfWorks.IUnitOfWorks;
using Notes.Models.Entity.Dto;
using Notes.Models.RoleIdConst;

namespace Notes.Web.Areas.Admin.ApiControllers
{
    [Route("api/CategoryApi")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = ConstRole.Admin)]
    public class CategoryApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetAllCategories() 
        {
            return Ok();
        }
    }
}
