using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AkayouSite.Controllers
{
    public class LoginController : BaseController
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
            if (User.Identity.IsAuthenticated) return Json(new { ok = true, resultado = Resultado.USUARIO_JA_AUTENTICADO.ToString() });

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

        [HttpPost]
        public JsonResult EstaLogado()
        {
            return Json(User.Identity.IsAuthenticated);
        }

        [HttpPost]
        public JsonResult InitialUsersSetup() {
            if (!Roles.RoleExists("Sysadm")) Roles.CreateRole("Sysadm");
            if (!Roles.RoleExists("Admin")) Roles.CreateRole("Admin");
            if (!Roles.RoleExists("Usuario")) Roles.CreateRole("Usuario");

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
                Roles.AddUserToRole("Daniel", "Sysadm");
            }

            long? ficticio1Id = null;
            for (int i = 1; i < 10; i++)
            {
                var ficticio = Membership.GetUser("Ficticio" + i.ToString());
                if (ficticio == null)
                {
                    ficticio = Membership.CreateUser("Ficticio" + i.ToString(), "akayou123*", "ficticio1@gmail.com");
                    Roles.AddUserToRole("Ficticio" + i.ToString(), "Usuario");
                }
                if (!Db.Cadastro.Any(c => c.MembershipUserName == "Ficticio" + i.ToString()))
                {
                    var ficticioCadastro = new Db.Cadastro()
                    {
                        CadastroGuid = Guid.NewGuid().ToString(),
                        Finalizado = i > 1,
                        DataCadastro = DateTime.Now,
                        Patrocinador = i > 1 ? ficticio1Id.Value : null as long?,
                        Upline = i > 1 ? ficticio1Id.Value : null as long?,
                        MembershipUserName = "Ficticio" + i.ToString(),
                        Nome = "Ficticio",
                        Sobrenome = i.ToString(),
                        Cpf = "111.111.111-11",
                        Rg = "11.111.111-11",
                        PisPasep = "111.11111.11-1",
                        DataNascimento = new DateTime(1980, 1, 1),
                        Cep = "11111-11",
                        Endereco = "Rua Eng. Caetano Álvares, 4100",
                        Bairro = "Casa Verde",
                        Complemento = "andar 1",
                        Cidade = "São Paulo",
                        TelCelular = "(11) 11111-1111",
                        TelResidencial = "(11) 1111-1111",
                        TelComercial = "(11) 1111-1111",
                        Email = "ficticio" + i.ToString() + "@gmail.com"
                    };
                    Db.Cadastro.InsertOnSubmit(ficticioCadastro);
                    Db.SubmitChanges();
                    if (i == 1) ficticio1Id = ficticioCadastro.Id;
                }
            }

            return Json(new { ok = true });
        }
    }
}
