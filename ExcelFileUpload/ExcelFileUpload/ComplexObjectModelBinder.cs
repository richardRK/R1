using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ExcelFileUpload
{
    public class ComplexObjectModelBinder : System.Web.Mvc.IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            var contentType = controllerContext.HttpContext.Request.ContentType;
            if (!contentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
                return (null);

            string jsonString;

            using (var stream = controllerContext.HttpContext.Request.InputStream)
            {
                stream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(stream))
                    jsonString = reader.ReadToEnd();
            }

            if (string.IsNullOrEmpty(jsonString)) return (null);

            DynamicComplexObject ExtraData = new DynamicComplexObject();

            ExtraData.Details = new JavaScriptSerializer().Deserialize<dynamic>(jsonString);

            return (ExtraData);
        }
    }
}