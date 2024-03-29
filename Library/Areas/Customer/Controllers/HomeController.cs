﻿using Library.DataAccess.Repository.IRepository;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Library.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()          
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            ShoppingCartVM shoppingCartObj = new ShoppingCartVM()
            {
                Count = 1,
                Product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id, includeProperties: "Category,CoverType")
            };
           
            return View(shoppingCartObj);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current.Id ?? HttpContext.TraceIdentifier });
        }
    }
}