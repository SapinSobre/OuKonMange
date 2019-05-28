using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OuKonMange3.Filters
{
    public class AjaxFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Headers != null && filterContext.HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                filterContext.Result = new HttpNotFoundResult();
            base.OnActionExecuting(filterContext);
        }
    }
}