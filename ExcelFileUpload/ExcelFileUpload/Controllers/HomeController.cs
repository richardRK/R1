using ExcelFileUpload.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ExcelFileUpload.Controllers
{
    public class HomeController : Controller
    {


       

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (ExcelUploadEntities db = new ExcelUploadEntities())
            {
                List<StudentsList> studentList = db.StudentsLists.ToList<StudentsList>();
                return Json(new { data = studentList }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ImportData()
        {
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> ImportData(JsonDynamicWrapper json)
        {
            dynamic data = json.objects; // Get the actual data out of the object


            List<StudentsList> modelList = new List<StudentsList>();
            if (data != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                modelList = (List<StudentsList>)serializer.Deserialize(data, typeof(List<StudentsList>));
            }



            bool status = false;
            if (ModelState.IsValid)
            {
                using (ExcelUploadEntities db = new ExcelUploadEntities())
                {
                    foreach (var i in modelList)
                    {
                        db.StudentsLists.Add(i);
                    }
                    await db.SaveChangesAsync();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };

        }


    }
}