using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Library.Controllers
{
    public class CategoryController :Controller
    {
        private readonly LibraryDBContext _db;
        public CategoryController (LibraryDBContext db)
        {
            _db = db;
        }
       public IActionResult Index()
        {
            IEnumerable<Category> CategoryList = _db.Categories;
            return View(CategoryList);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
