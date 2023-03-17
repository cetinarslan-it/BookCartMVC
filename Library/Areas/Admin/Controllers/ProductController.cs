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
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
            }
         
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

                    //to delete an existing image file before replacing it

                    if(obj.Product.ImageUrl != null) 
                    { 
                        var oldImagePath = Path.Combine(wwwRoothPath, obj.Product.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath)) 
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                     using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                     {
                        file.CopyTo(fileStreams);
                     }

                     obj.Product.ImageUrl = @"\images\products\" + fileName + extension;

                 }

                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);    
                }

                 _unitOfWork.Save();
 
                 TempData["success"] = "New product created succesfully";

                 return RedirectToAction("Index");
            }

            return View(obj);

        }
   
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
          var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");

            return Json(new { data = productList });    
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);

            if (obj == null)
            {
                return Json(new {success = false, message = "A error happened while deleting!.."});

            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();


            return Json(new { success = true, message = "Product deleted succesfully!..." });
        }

        #endregion

    }

}

