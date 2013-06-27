using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkayouSite.Areas.PreCadastro.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /PreCadastro/Home/

        public ActionResult Index(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return ReturnUrl != null ? Redirect(ReturnUrl ?? "/PreCad/Perfil") as ActionResult : RedirectToAction("Index", "Perfil");

            var model = new Models.HomeIndexViewModel()
            {
                ReturnUrl = ReturnUrl
            };

            return View(model);
        }

    }
}
