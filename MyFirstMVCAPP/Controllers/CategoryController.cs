using Microsoft.AspNetCore.Mvc;
using MyFirstMVCAPP.Models;
using MyFirstMVCAPP.Repository.Base;

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
    }
}
