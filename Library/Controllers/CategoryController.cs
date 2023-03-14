using Library.DataAccess;
using Library.DataAccess.Repository.IRepository;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Library.Controllers
{
    public class CategoryController :Controller
    {
        private readonly ICategoryRepository _db;
        public CategoryController (ICategoryRepository db)
        {
            _db = db;
        }
       public IActionResult Index()
        {
            IEnumerable<Category> CategoryList = _db.GetAll();
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
                _db.Add(obj);
                _db.Save();

                TempData["success"] = "A new category added succesfully";

                return RedirectToAction("Index");
            }

            return View(obj);
           
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _db.GetFirstOrDefault(c => c.Id == id);
            //var categorySingle = _db.Categories.SingleOrDefault(c => c.Id == id);
            //var categoryFind = _db.Categories.Find(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The display order cannot have the same value with the name!");
            }

            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.Save();

                TempData["success"] = "Category updated succesfully";

                return RedirectToAction("Index");
            }

            return View(obj);

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
             var category = _db.GetFirstOrDefault(c => c.Id == id);
            //var categorySingle = _db.Categories.SingleOrDefault(c => c.Id == id);
            //var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.GetFirstOrDefault(c => c.Id == id);

            if (obj == null)
            {
                return NotFound();
               
            }
         
            _db.Remove(obj);
            _db.Save();

            TempData["success"] = "Category deleted succesfully";

            return RedirectToAction("Index");
        }
    }
}

