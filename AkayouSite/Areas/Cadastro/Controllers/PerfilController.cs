using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkayouSite.Areas.Cadastro.Controllers
{
    public class PerfilController : AkayouSite.Controllers.BaseController
    {
        [Authorize(Roles="Sysadm,Usuario")]
        public ActionResult Index()
        {
            var cadastro = Db.Cadastro.SingleOrDefault(c => c.MembershipUserName == User.Identity.Name);
            if (cadastro == null) return RedirectToAction("Index", "Home");

            var podeCadastrar = Db.Cadastro.Where(c => c.MembershipUserName != User.Identity.Name &&
                c.Finalizado).OrderBy(c => c.Nome).OrderBy(c => c.Sobrenome).ToList();

            var model = new Models.PerfilIndexViewModel()
            {
                Cadastro = cadastro, 
                PodeCadastrar = podeCadastrar, 
                VolumePernaEsquerda = 100, 
                VolumePernaDireita = 100
            };

            return View(model);
        }

    }
}