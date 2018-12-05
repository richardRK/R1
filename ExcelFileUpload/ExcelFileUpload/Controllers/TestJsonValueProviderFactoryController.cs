using ExcelFileUpload.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExcelFileUpload.Controllers
{
    public class TestJsonValueProviderFactoryController : Controller
    {
        
        [HttpGet]
        public ActionResult Index()
        {
            return View(new TestJsonDotNetValueProviderFactoryModel().LoadTestData());
        }

        [HttpPost]
        public ActionResult Index(TestJsonDotNetValueProviderFactoryModel viewModel)
        {
            return Json(viewModel);
        }
	}
}