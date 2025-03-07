using Microsoft.AspNetCore.Mvc;
using Notes.Data.UnitOfWorks.IUnitOfWorks;
using Notes.Models.Entity;

namespace Notes.Web.Controllers
{
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
            List<Category> ObjCategories = _unitOfWork.Category.GetAll().ToList();
            _logger.LogInformation($"Method Index / getall finish ");

            return View(ObjCategories);
        }
        //get create
        public IActionResult Create()
        {
            _logger.LogInformation($"Method CreateGet / return view ");
            return View();
        }

        //пост метод create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            _logger.LogInformation($"Method CreatePost / check {obj} ");
            if (obj == null) 
            {
                return NotFound();
            }
            _logger.LogInformation($"Method CreatePost / check {obj} finish ");

            _logger.LogInformation($"Method CreatePost / Проверка на валидность модели ");

            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Method CreatePost / add {obj} + savechanges {obj} ");
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                _logger.LogInformation($"Method CreatePost / save {obj} finish ");
                TempData["success"] = "Категория создана";
                return RedirectToAction("Index");
            }
            _logger.LogInformation($"Method CreatePost / Проверка на валидность модели finish ");

            return View(obj);
        }
        //для view edit по id get
        public IActionResult Edit(long? id)
        {
            _logger.LogInformation($"Method CreatePost / проверка условия ");
            if (id == null || id == 0)
            {
                return NotFound();
            }
            _logger.LogInformation($"Method CreatePost / Проверка успешна ");

            _logger.LogInformation($"Method CreatePost / get{id} for category proccess ");
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            _logger.LogInformation($"Method CreatePost / get{id} for category finish ");

            _logger.LogInformation($"Method CreatePost / проверка условия ");
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Method CreatePost / проверка успешна ");

            return View(categoryFromDb);
        }

        //пост метод для редактирования 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            _logger.LogInformation($"Method EditGet / проверка условия ");
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Method CreatePost / update {obj} + save {obj} ");
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                _logger.LogInformation($"Method CreatePost / update {obj} + save {obj} finish");
                TempData["success"] = "Категория обновлена";

                return RedirectToAction("Index");
            }
            _logger.LogInformation($"Method EditGet / проверка успешна ");

            return View(obj);
        }

        public IActionResult Delete(long? id)
        {
            _logger.LogInformation($"Method DeleteGet / проверка условия ");
            if (id == null || id == 0)
            {
                return NotFound();
            }
            _logger.LogInformation($"Method DeleteGet / проверка успешна ");

            _logger.LogInformation($"Method DeleteGet / get{id} for category proccess ");
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            _logger.LogInformation($"Method DeleteGet / get{id} for category finish ");

            _logger.LogInformation($"Method DeleteGet / проверка условия ");
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Method DeleteGet / проверка успешна ");

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(long? id)
        {
            _logger.LogInformation($"Method DeletePost / get {id} for obj process");
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            _logger.LogInformation($"Method DeletePost / get {id} for obj Finish");

            _logger.LogInformation($"Method DeletePost / проверка условия");
            if (obj == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Method DeletePost / проверка успешна");


            _logger.LogInformation($"Method DeletePost / remove {obj} + savechanges");
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            _logger.LogInformation($"Method DeletePost / remove {obj} + savechanges finish");
            TempData["success"] = "Категория удалена";

            return RedirectToAction("Index");
        }
    }
}
