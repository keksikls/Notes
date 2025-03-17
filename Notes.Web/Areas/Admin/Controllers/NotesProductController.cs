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

            List<NotesProduct> objProductList = _unitOfWork.NotesProduct.GetAll(includeProperties: "Category").ToList();

            _logger.LogInformation("Method Index / Передал NotesProduct в представление ");

            return View(objProductList);

        }
        //get для view delte
        [HttpGet]
        public IActionResult Delete(long? id)
        {
            _logger.LogInformation("Method Delete / Проверка условия");
            if (id == null || id == 0)
            {
                return NotFound();
            }
            _logger.LogInformation("Method Delete / Проверка успешна");

            _logger.LogInformation($"Method Delete / try Get {id} process");
            NotesProduct? productFromDb = _unitOfWork.NotesProduct.Get(u => u.Id == id);
            _logger.LogInformation($"Method Delete / try Get {id} finish");

            _logger.LogInformation("Method Delete / Проверка условия");
            if (productFromDb == null)
            {
                return NotFound();
            }
            _logger.LogInformation("Method Delete / Проверка успешна");

            return View(productFromDb);
        }

        //post метод для delete 
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(long? id)
        {
            _logger.LogInformation($"Method DeletePost / try Get {id} process");
            NotesProduct? obj = _unitOfWork.NotesProduct.Get(u => u.Id == id);
            _logger.LogInformation($"Method DeletePost / try Get {id} finish");

            _logger.LogInformation($"Method DeletePost / проверка условия");
            if (obj == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Method DeletePost / проверка успешна");

            _logger.LogInformation($"Method DeletePost / attempt remove + savechanges {obj} process");
            _unitOfWork.NotesProduct.Remove(obj);
            _unitOfWork.Save();
            _logger.LogInformation($"Method DeletePost / attempt remove + savechanges {obj} finish");
            TempData["success"] = "Product удален";

            return RedirectToAction("Index");
        }
        //get для метода upsert
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
            _logger.LogInformation($"Method Upsert / attempt get productVM finish");

            _logger.LogInformation($"Method Upsert / проверка условия");
            if (id == null || id == 0)
            {
                //create
                _logger.LogInformation($"Method Upsert / return create view");
                return View(productVM);
            }
            else
            {
                _logger.LogInformation($"Method Upsert / try Get {id} process for update view procces");
                //update
                productVM.NotesProduct = _unitOfWork.NotesProduct.Get(u => u.Id == id);
                _logger.LogInformation($"Method Upsert / try Get {id} process for update view finish");
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(NotesProductVM notesProductVM)
        {
            _logger.LogInformation($"Method UpsertPost / проверка условия");
            if (notesProductVM == null)
            {
                return NotFound();
            }
            else if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _logger.LogInformation($"Method UpsertPost / проверка успешна");

            _logger.LogInformation($"Method UpsertPost / проверка условия ModelState.IsValid");
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Method UpsertPost / проверка условия /get id");
                if (notesProductVM.NotesProduct.Id == 0)
                {
                    _logger.LogInformation($"Method UpsertPost / get id,if id == 0 its .Add");
                    _unitOfWork.NotesProduct.Add(notesProductVM.NotesProduct);
                }
                else
                {
                    _logger.LogInformation($"Method UpsertPost / get id,if id != 0 its .Update");
                    _unitOfWork.NotesProduct.Update(notesProductVM.NotesProduct);
                }

                _logger.LogInformation($"Method UpsertPost / SaveChanges process");
                _unitOfWork.Save();
                _logger.LogInformation($"Method UpsertPost / SaveChanges finish");
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
                _logger.LogInformation($"Method UpsertPost / !ModelState.IsValid create new categoryList finish ");
                return View(notesProductVM);
            }
        }
    }
}
