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
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError","The display order cannot have the same value with the name!");
            }
         
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
           
        }
      
    }
}
