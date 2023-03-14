using Library.DataAccess;
using Library.DataAccess.Repository.IRepository;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Library.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> CoverTypeList = _unitOfWork.Product.GetAll();
            return View(CoverTypeList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();

                TempData["success"] = "A new cover type added succesfully";

                return RedirectToAction("Index");
            }

            return View(obj);

        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Product product = new();
            if (id == null || id == 0)
            {
                //create product

                return View(product);
            }
            else
            {
                //update product
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();

                TempData["success"] = "Cover type updated succesfully";

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
            var product = _unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
          
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);

            if (obj == null)
            {
                return NotFound();

            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();

            TempData["success"] = "Cover type deleted succesfully";

            return RedirectToAction("Index");
        }
    }
}

