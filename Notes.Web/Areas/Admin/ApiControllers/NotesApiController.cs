using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Data.AppDbContext;
using Notes.Data.UnitOfWorks;
using Notes.Data.UnitOfWorks.IUnitOfWorks;
using Notes.Models.Entity;
using Notes.Models.Entity.Dto;
using Notes.Models.RoleIdConst;

namespace Notes.Web.Areas.Admin.ApiControllers
{
    [Route("api/NotesApi")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = ConstRole.Admin)]
    public class NotesApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotesApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<NotesProductDTO>> GetNotes() 
        //{
        //    var notes = _unitOfWork.NotesProduct.

        //    if (notes == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(notes);
        //}

        //[HttpGet("{id:long}", Name = "GetById")]
        //public ActionResult<NotesProductDTO> GetById(long id) 
        //{
        //    if (id == 0)
        //    {
        //        return BadRequest();
        //    }

        //    var notes = _context.NotesProducts?.FirstOrDefault(u=>u.Id == id);

        //    if (notes == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(notes);
        //}
    }
}
