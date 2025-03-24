using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Data.AppDbContext;
using Notes.Data.UnitOfWorks;
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
        private readonly ApplicationDbContext _context;
        public NotesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<NotesProductDTO>> GetNotes() 
        {
            var notes = _context.NotesProducts?.ToList();

            if (notes == null)
            {
                return NotFound();
            }

            return Ok(notes);
        }

        [HttpGet("{id:long}", Name = "GetById")]
        public ActionResult<NotesProductDTO> GetById(long id) 
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var notes = _context.NotesProducts?.FirstOrDefault(u=>u.Id == id);

            if (notes == null)
            {
                return NotFound();
            }

            return Ok(notes);
        }
    }
}
