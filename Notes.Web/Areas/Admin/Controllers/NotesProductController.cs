using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notes.Data.UnitOfWorks.IUnitOfWorks;
using Notes.Models.Entity;
using Notes.Models.RoleIdConst;
using Notes.Models.ViewModel;
using Serilog;
using Service.CategoryServices.ICategoryServices;

namespace Notes.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ConstRole.Admin)]
    public class NotesProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<NotesProductController> _logger;
        private readonly ICategoryService _categoryService;

        public NotesProductController(IUnitOfWork unitOfWork, ILogger<NotesProductController> logger, ICategoryService categoryService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("Method Index /  Передача NotesProduct в представление ");

            //todo как блядь инклудить эту поебень в репозиторий
            List<NotesProduct> objProductList = _unitOfWork.NotesProduct.GetAll(includeProperties: "Category").ToList();

            return View(objProductList);

        }
        //get для view delte
        [HttpGet]
        public IActionResult Delete(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            _logger.LogInformation($"Method Delete / try Get {id} process");
            NotesProduct? productFromDb = _unitOfWork.NotesProduct.GetProduct(id);

            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }

        //post метод для delete 
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(long? id)
        {
            _logger.LogInformation($"Method DeletePost / try Get {id} process");
            NotesProduct? obj = _unitOfWork.NotesProduct.GetProduct(id);

            if (obj == null || id == 0)
            {
                return NotFound();
            }

            _unitOfWork.NotesProduct.DeleteNotes(obj);

            return RedirectToAction("Index");
        }
        //get для метода upsert
        [HttpGet]
        public IActionResult Upsert(long? id)
        {
            _logger.LogInformation($"Method Upsert / attempt get productVM procces");
            NotesProductVM productVM = new()
            {
                CategoryList = _categoryService.GetCategorySelectList(),
                NotesProduct = new NotesProduct()
            };

            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.NotesProduct = _unitOfWork.NotesProduct.GetProduct(id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(NotesProductVM notesProductVM)
        {
            if (notesProductVM == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                notesProductVM.CategoryList = _categoryService.GetCategorySelectList();
                return View(notesProductVM);
            }

            _logger.LogInformation("Method Upsert / start");

            if (notesProductVM.NotesProduct.Id == 0)
            {
                _unitOfWork.NotesProduct.AddProduct(notesProductVM); 
            }
            else
            {
                _unitOfWork.NotesProduct.UpdateProduct(notesProductVM);
            }

            return RedirectToAction("Index");
        }
    }
}
