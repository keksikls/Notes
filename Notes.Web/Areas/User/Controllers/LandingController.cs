using Microsoft.AspNetCore.Mvc;
using Notes.Data.UnitOfWorks.IUnitOfWorks;
using Notes.Models.Entity;

namespace Notes.Web.Areas.User.Controllers
{
    [Area("User")]
    public class LandingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LandingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<NotesProduct> products = _unitOfWork.NotesProduct.GetAll().ToList();
            return View(products);
        }
    }
}
