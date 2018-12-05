using System.Dynamic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using ExcelFileUpload;

namespace System.Web.Mvc
{
    public sealed class JsonDotNetValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {

            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");

            if (!controllerContext.HttpContext.Request.ContentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
                return null;

            var reader = new StreamReader(controllerContext.HttpContext.Request.InputStream);
            var bodyText = reader.ReadToEnd();

            //dynamic objects = JsonConvert.DeserializeObject<ExpandoObject>(bodyText, new ExpandoObjectConverter());

            //foreach (var obj in objects)
            //{
            //vProvider = String.IsNullOrEmpty(bodyText) ? null : new DictionaryValueProvider<object>(objects, CultureInfo.CurrentCulture);
            //}

            //JsonDynamicWrapper jd = new JsonDynamicWrapper();
            //jd.objects = JsonConvert.DeserializeObject<ExpandoObject>(bodyText, new ExpandoObjectConverter());
            return String.IsNullOrEmpty(bodyText) ? null : new DictionaryValueProvider<object>(JsonConvert.DeserializeObject<ExpandoObject>(bodyText, new ExpandoObjectConverter()), CultureInfo.CurrentCulture); ;

        }
    }
}