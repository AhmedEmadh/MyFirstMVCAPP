using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstMVCAPP.Data;
using MyFirstMVCAPP.Models;
using System.Collections;

namespace MyFirstMVCAPP.Controllers
{
    public class ItemsController : Controller
    {
        public ItemsController(AppDbContext db)
        {
            _db = db;
        }
        private readonly AppDbContext _db;
        public void createSelectList(int selectId = 0)
        {
            ViewBag.ListItemsSelectedID = selectId;
            //List<Category> categories = new List<Category> {
            //  new Category() {Id = 0, Name = "Select Category"},
            //  new Category() {Id = 1, Name = "Computers"},
            //  new Category() {Id = 2, Name = "Mobiles"},
            //  new Category() {Id = 3, Name = "Electric machines"},
            //  new Category() {Id = 3, Name = "Animals"},
            //};
            List<Category> categories = _db.Categories.ToList();
            ViewBag.ListItems = categories;
            SelectList listItems = new SelectList(categories, "Id", "Name", selectId);
            ViewBag.CategoryList = listItems;
        }
        public IActionResult Index()
        {
            IEnumerable<Item> items = _db.Items.Include(item => item.Category).ToList();
            return View(items);
        }
        public IActionResult New()
        {
            createSelectList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Item item)
        {
            int result;
            if (int.TryParse(item.Name,out result))
            {
                ModelState.AddModelError("Name", "Name cannot be a number");
            }
            if (ModelState.IsValid)
            {
                _db.Items.Add(item);
                _db.SaveChanges();
                TempData["successData"] = "Item has been added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }
        // Get View for Edit Item
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = _db.Items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }
            createSelectList(item.CategoryId);
            return View(item);
        }
        // Post Edit Item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
            int result;
            if (int.TryParse(item.Name, out result))
            {
                ModelState.AddModelError("Name", "Name cannot be a number");
            }
            if (ModelState.IsValid)
            {
                _db.Items.Update(item);
                _db.SaveChanges();
                TempData["successData"] = "Item has been updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                createSelectList(item.CategoryId);
                return View(item);
            }
        }
        // Get
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = _db.Items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }
            createSelectList(item.CategoryId);
            return View(item);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(int? Id)
        {
            var item = _db.Items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }
            _db.Remove(item);
            _db.SaveChanges();
            TempData["successData"] = "Item has been deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
