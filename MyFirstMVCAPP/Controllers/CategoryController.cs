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
        public async Task<IActionResult> Index()
        {
            return View(await _repository.FindAllAsync());
        }
    }
}
