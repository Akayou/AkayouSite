using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AkayouSite.Areas.PreCadastro.Controllers
{
    public class UsuarioController : AkayouSite.Controllers.BaseController
    {
        //
        // GET: /PreCadastro/Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SiteAdminsSetup()
        {
            if (!Roles.RoleExists("Sysadm")) Roles.CreateRole("Sysadm");
            if (!Roles.RoleExists("Admin")) Roles.CreateRole("Admin");

            var pedro = Membership.GetUser("PedroSobota");
            if (pedro == null)
            {
                pedro = Membership.CreateUser("PedroSobota", "akayou123*", "pedrosobota@akayou.com.br");
                Roles.AddUserToRole("PedroSobota", "Sysadm");
            }
            var daniel = Membership.GetUser("Daniel");
            if (daniel == null)
            {
                daniel = Membership.CreateUser("Daniel", "akayou123*", "daniel@akayou.com.br");
                Roles.AddUserToRole("Daniel", "Admin");
            }
            return Json(true);
        }
    }
}