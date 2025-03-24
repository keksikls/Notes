using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Data.UnitOfWorks.IUnitOfWorks;
using Notes.Models.Entity;
using Notes.Models.RoleIdConst;

namespace Notes.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ConstRole.Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IUnitOfWork unitOfWork, ILogger<CategoryController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation($"Method Index / getall start ");
            List<Category> ObjCategories = _unitOfWork.Category.GetAllCategories();

            return View(ObjCategories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            _logger.LogInformation($"Method CreateGet / return view ");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            _logger.LogInformation($"Method CreatePost / add {obj} + savechanges {obj} ");

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.AddCategory(obj);
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        [HttpGet]
        public IActionResult Edit(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            _logger.LogInformation($"Method CreatePost / get{id} for category proccess ");
            Category? categoryFromDb = _unitOfWork.Category.EditCategory(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            _logger.LogInformation($"Method CreatePost / update {obj} + save {obj} ");
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.UpdateCategry(obj);
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        [HttpGet]
        public IActionResult Delete(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            _logger.LogInformation($"Method DeleteGet / get{id} for category proccess ");
            Category? categoryFromDb = _unitOfWork.Category.EditCategory(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(long? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            _logger.LogInformation($"Method DeletePost / get {id} for obj process");
            Category? obj = _unitOfWork.Category.EditCategory(id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.DeleteCategory(obj);

            return RedirectToAction("Index");
        }
    }
}
