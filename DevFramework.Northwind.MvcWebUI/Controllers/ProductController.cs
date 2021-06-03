using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevFramework.Core.Aspects.Postsharp.LogAspects;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using DevFramework.Northwind.MvcWebUI.Models;


namespace DevFramework.Northwind.MvcWebUI.Controllers
{

    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //[LogAspect(typeof(DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers.DatabaseLogger))]
        public ActionResult Index()
        {
            var model = new ProductListViewModel
            {
                Products = _productService.GetAll()
            };
            return View(model);
        }
        public string Add()
        {
            _productService.Add(new Product
            {
                CategoryId = 1,
                ProductName = "Gsm",
                QuantityPerUnit = "1",
                UnitPrice = 21
            });
            return "Added";
        }

        /*
        [LogAspect(typeof(DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers.DatabaseLogger))]
        public ActionResult List()
        {
            var Model = new ProductListViewModel
            {
                Products = _productService.GetAll()
            };
            return View(Model);
        }
        */
    }
}