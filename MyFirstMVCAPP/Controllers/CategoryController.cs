using Microsoft.AspNetCore.Mvc;
using MyFirstMVCAPP.Models;
using MyFirstMVCAPP.Repository.Base;
using System.Threading.Tasks;

namespace MyFirstMVCAPP.Controllers
{
    public class CategoryController : Controller
    {
        public CategoryController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        private IUnitOfWork _UnitOfWork;
        public IActionResult Index()
        {

            return View(_UnitOfWork.Categories.FindAll());
        }
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Category category)
        {
            int result;
            if (int.TryParse(category.Name, out result))
            {
                ModelState.AddModelError("Name", "Category Name cannot be a number");
            }
            if (ModelState.IsValid)
            {
                _UnitOfWork.Categories.Add(category);
                TempData["successData"] = "Category has been added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        public IActionResult Edit(int id)
        {
            Category category = _UnitOfWork.Categories.FindByID(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            int result;
            if (int.TryParse(category.Name, out result))
            {
                ModelState.AddModelError("Name", "Category Name cannot be a number");
            }
            if (ModelState.IsValid)
            {
                _UnitOfWork.Categories.Update(category);
                TempData["successData"] = "Category has been updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category category = _UnitOfWork.Categories.FindByID(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _UnitOfWork.Categories.FindByID((int)id);
            if (category == null)
            {
                return NotFound();
            }

            _UnitOfWork.Categories.Delete(category);
            TempData["successData"] = "Category has been deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
