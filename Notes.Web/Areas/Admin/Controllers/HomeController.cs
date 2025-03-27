using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notes.Data.UnitOfWorks.IUnitOfWorks;
using Notes.Models.Entity;
using Notes.Models.RoleIdConst;

namespace Notes.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ConstRole.Admin)]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("Method Index / getall start");
            //todo как блядь инклудить эту поебень в репозиторий

            List<NotesProduct> notesProduct = _unitOfWork.NotesProduct
                .GetAll(includeProperties: "Category")
                .ToList();

            return View(notesProduct);
        }

        [HttpGet]
        public IActionResult Details([FromQuery(Name = "productId")] long id)
        {
            _logger.LogInformation("Method DeteilsGet / getForId start");
            NotesProduct notesProduct = _unitOfWork.homePage.GetForDetails(id);

            return View(notesProduct);
        }
    }
}
