using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notes.Data.UnitOfWorks.IUnitOfWorks;
using Notes.Models.Entity;

namespace Notes.Web.Areas.User.Controllers
{
    [Area("User")]
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
            List<NotesProduct> notesProduct = _unitOfWork.NotesProduct
                .GetAll(includeProperties: "Category")
                .ToList();

            foreach (var item in notesProduct)
            {
                Console.WriteLine($"Product: {item.Title}, Category: {item.Category?.Name}");
            }
            _logger.LogInformation("Method Index / getall finish");

            return View(notesProduct);
        }
        [HttpGet]
        public IActionResult Details([FromQuery(Name = "productId")] long id)
        {
            _logger.LogInformation("Method DeteilsGet / getForId start");
            NotesProduct notesProduct = _unitOfWork.NotesProduct.Get(u => u.Id == id, includeProperties: "Category");
            _logger.LogInformation("Method DeteilsGet / getForId finish");

            return View(notesProduct);
        }
    }
}
