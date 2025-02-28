using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notes.Data.UnitOfWorks.IUnitOfWorks;
using Notes.Models.Entity;
using Notes.Models.ViewModel;

namespace Notes.Web.Controllers
{
    public class NotesProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotesProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //includeProperties: "Category"
            List<NotesProduct> objProductList = _unitOfWork.NotesProduct.GetAll(includeProperties: "Category").ToList();

            return View(objProductList);
 
        }
        //get для view delte
        public IActionResult Delete(long? id) 
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

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
            NotesProduct? obj = _unitOfWork.NotesProduct.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.NotesProduct.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product удален";
            return RedirectToAction("Index");
        }
        //get для метода upsert
        public IActionResult Upsert(long? id) 
        {
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
