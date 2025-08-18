using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstMVCAPP.Data;
using MyFirstMVCAPP.Models;
using MyFirstMVCAPP.Repository.Base;
using System.Collections;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace MyFirstMVCAPP.Controllers
{
    public class ItemsController : Controller
    {
        public ItemsController(IUnitOfWork unitOfWork, IHostingEnvironment host)
        {
            _unitOfWork = unitOfWork;
            _host = host;
        }
        private readonly IHostingEnvironment _host;
        private readonly IUnitOfWork _unitOfWork;
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
            List<Category> categories = _unitOfWork.Categories.FindAll().ToList();
            ViewBag.ListItems = categories;
            SelectList listItems = new SelectList(categories, "Id", "Name", selectId);
            ViewBag.CategoryList = listItems;
        }
        public IActionResult Index()
        {
            // Using the UnitOfWork to get items
            IEnumerable<Item> items = _unitOfWork.Items.FindAll("Category");
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
                string fileName = string.Empty;
                if (item.clientFile != null)
                {
                    string myUpload = Path.Combine(_host.WebRootPath, "images");
                    fileName = item.clientFile.FileName;
                    string fullPath = Path.Combine(myUpload, fileName);
                    item.clientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                    item.imagePath = fileName;
                }
                _unitOfWork.Items.Add(item);
                _unitOfWork.ComitChanges();
                TempData["successData"] = "Item has been added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }
        // Get View for Edit Item
        [Authorize(Roles = clsRoles.roleAdmin)]
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = _unitOfWork.Items.FindByID((int)Id);
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
        [Authorize(Roles = clsRoles.roleAdmin)]
        public IActionResult Edit(Item item)
        {
            int result;
            if (int.TryParse(item.Name, out result))
            {
                ModelState.AddModelError("Name", "Name cannot be a number");
            }

            if (ModelState.IsValid)
            {
                // Get existing item from DB to preserve old image if no new one is uploaded
                var existingItem = _unitOfWork.Items.FindByID(item.Id);
                if (existingItem == null)
                {
                    return NotFound();
                }

                string fileName = existingItem.imagePath; // keep old image by default

                if (item.clientFile != null)
                {
                    string myUpload = Path.Combine(_host.WebRootPath, "images");
                    fileName = item.clientFile.FileName;
                    string fullPath = Path.Combine(myUpload, fileName);

                    // Save new file
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        item.clientFile.CopyTo(stream);
                    }
                }

                // Update item values
                existingItem.Name = item.Name;
                existingItem.Price = item.Price;
                existingItem.CategoryId = item.CategoryId;
                existingItem.imagePath = fileName;

                _unitOfWork.Items.Update(existingItem);
                _unitOfWork.ComitChanges();

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
        [Authorize(Roles = clsRoles.roleAdmin)]
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = _unitOfWork.Items.FindByID((int)Id);
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
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = _unitOfWork.Items.FindByID((int)Id);
            if (item == null)
            {
                return NotFound();
            }
            _unitOfWork.Items.Delete(item);
            _unitOfWork.ComitChanges();
            TempData["successData"] = "Item has been deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
