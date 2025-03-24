using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notes.Data.UnitOfWorks.IUnitOfWorks;
using Notes.Models.Entity;
using Notes.Models.RoleIdConst;
using Notes.Models.ViewModel;
using Serilog;

namespace Notes.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ConstRole.Admin)]
    public class NotesProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<NotesProductController> _logger;

        public NotesProductController(IUnitOfWork unitOfWork, ILogger<NotesProductController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
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
            NotesProduct? productFromDb = _unitOfWork.NotesProduct.Get(u => u.Id == id);

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
            NotesProduct? obj = _unitOfWork.NotesProduct.Get(u => u.Id == id);

            if (obj == null || id == 0)
            {
                return NotFound();
            }

            _unitOfWork.NotesProduct.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product удален";

            return RedirectToAction("Index");
        }
        //get для метода upsert
        [HttpGet]
        public IActionResult Upsert(long? id)
        {
            _logger.LogInformation($"Method Upsert / attempt get productVM procces");
            NotesProductVM productVM = new()
            {

                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
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
                productVM.NotesProduct = _unitOfWork.NotesProduct.Get(u => u.Id == id);
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
            else if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (notesProductVM.NotesProduct.Id == 0)
                {
                    _unitOfWork.NotesProduct.Add(notesProductVM.NotesProduct);
                }
                else
                {
                    _unitOfWork.NotesProduct.Update(notesProductVM.NotesProduct);
                }

                _unitOfWork.Save();
                TempData["success"] = "NotesProduct создан";

                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogInformation($"Method UpsertPost / !ModelState.IsValid create new categoryList proccess ");
                notesProductVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(notesProductVM);
            }
        }
    }
}
