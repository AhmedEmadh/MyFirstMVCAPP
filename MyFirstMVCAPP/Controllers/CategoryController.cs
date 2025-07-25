using Microsoft.AspNetCore.Mvc;
using MyFirstMVCAPP.Models;
using MyFirstMVCAPP.Repository.Base;
using System.Threading.Tasks;

namespace MyFirstMVCAPP.Controllers
{
    public class CategoryController : Controller
    {
        public CategoryController(IRepository<Category> repository)
        {
            _repository = repository;
        }

        private IRepository<Category> _repository;
        public IActionResult Index()
        {

            return View(_repository.FindAll());
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
                _repository.Add(category);
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
            Category category = _repository.FindByID(id);
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
                _repository.Update(category);
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
            Category category = _repository.FindByID(id.Value);
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
            var category = _repository.FindByID((int)id);
            if (category == null)
            {
                return NotFound();
            }

            _repository.Delete(category);
            TempData["successData"] = "Category has been deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
