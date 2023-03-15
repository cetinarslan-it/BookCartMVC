using Library.DataAccess;
using Library.DataAccess.Repository.IRepository;
using Library.Models;
using Library.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace Library.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
           
            return View();
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
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(u =>
                    new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }
                ),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(u =>
                    new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }
                )
            };

            //Product product = new Product();
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    });

            //IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    });

            if (id == null || id == 0)
            {
                //create product

                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;

                return View(productVM);
            }
            else
            {
                //update product
            }
            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoothPath = _hostEnvironment.WebRootPath;

                 if(file != null)
                 {
                     string fileName = Guid.NewGuid().ToString();
                     var uploads = Path.Combine(wwwRoothPath, @"images\products");
                     var extension = Path.GetExtension(file.FileName);

                     using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                     {
                        file.CopyTo(fileStreams);
                     }

                     obj.Product.ImageUrl = @"\images\products\" + fileName + extension;

                 }

           
                 _unitOfWork.Product.Add(obj.Product);
                 _unitOfWork.Save();
 
                 TempData["success"] = "New product created succesfully";

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


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
          var productList = _unitOfWork.Product.GetAll();

            return Json(new { data = productList });    
        }

        #endregion

    }

}

