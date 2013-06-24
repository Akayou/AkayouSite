using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AkayouSite.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return ReturnUrl != null ? Redirect(ReturnUrl) as ActionResult : RedirectToAction("Index", "Home");

            var model = new AkayouSite.Models.LoginViewModel()
            {
                ReturnUrl = ReturnUrl
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult Try(string l1, string l2)
        {
            var membershipUser = Membership.GetUser(l1);

            // Não cadastrado
            if (membershipUser == null) return Json(new { ok = false, resultado = Resultado.USUARIO_INEXISTENTE.ToString() });

            FormsAuthentication.SetAuthCookie(membershipUser.UserName, true);

            return Json(new { ok = true });

            /*// Já acessou
            if (membershipUser.IsApproved) return Json(new { ok = true, resultado = Resultado.OK.ToString() });

            // Primeiro acesso
            membershipUser.IsApproved = true;
            Membership.UpdateUser(membershipUser);
            return Json(new { ok = true, resultado = Resultado.PRIMEIRO_ACESSO.ToString() });*/
        }
    }
}
