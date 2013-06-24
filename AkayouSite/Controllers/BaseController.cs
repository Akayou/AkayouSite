using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkayouSite.Controllers
{
    public class BaseController : Controller
    {
        protected AkayouSiteDataContext Db { get; set; }

        public BaseController()
        {
            Db = new AkayouSiteDataContext();
        }
    }
}
