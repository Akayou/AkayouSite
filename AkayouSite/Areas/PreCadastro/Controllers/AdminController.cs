using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkayouSite.Areas.PreCadastro.Controllers
{
    public class AdminController : AkayouSite.Controllers.BaseController
    {
        [Authorize(Roles="Sysadm,Admin")]
        public ActionResult Index(int page = 1)
        {
            var cads = Db.Cadastro.Skip((page - 1) * 50).Take(50).OrderByDescending(pc => pc.DataCadastro).ToList();

            var model = new Models.AdminIndexViewModel()
            {
                Cadastros = cads
            };

            return View(model);
        }

        [Authorize(Roles = "Sysadm,Admin")]
        public ActionResult Edita(int? pc)
        {
            AkayouSite.Db.Cadastro cad = null;
            if (pc.HasValue)
            {
                cad = Db.Cadastro.SingleOrDefault(_pc => _pc.Id == pc.Value);
                if (cad == null) return RedirectToAction("Index");
            }

            var model = new Models.AdminEditaViewModel()
            {
                Cadastro = cad
            };

            return View(model);
        }

        [Authorize(Roles = "Sysadm,Admin")]
        [HttpPost]
        public JsonResult Salva(int? id, string login, string nome, string cpf, int? cadastradoPor, int? superior)
        {
            return Json(new { ok = true });
        }
    }
}
