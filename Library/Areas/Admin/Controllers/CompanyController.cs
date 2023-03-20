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
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {      
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Company company = new();


            if (id == null || id == 0)
            {
              
                return View(company);
            }
            else
            {
                company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);
            }        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {

                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Company created succesfully";
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    TempData["success"] = "Company updated succesfully";
                }

                 _unitOfWork.Save();

                 return RedirectToAction("Index");
            }

            return View(obj);

        }
   
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
          var companyList = _unitOfWork.Company.GetAll();

            return Json(new { data = companyList });    
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);

            if (obj == null)
            {
                return Json(new {success = false, message = "A error happened while deleting!.."});

            }

            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();


            return Json(new { success = true, message = "Company deleted succesfully!..." });
        }

        #endregion

    }

}

