﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AkayouSite.Areas.Cadastro.Controllers
{
    public class CadastroController : AkayouSite.Controllers.BaseController
    {
        public ActionResult Login() {
            return View();
        }

        public ActionResult Index(long? upline)
        {
            string cadastroGuid;
            Db.Cadastro cadastro;
            bool iniciando;
            if (Request.Cookies.AllKeys.Contains("CadastroGuid"))
            {
                cadastroGuid = Request.Cookies["CadastroGuid"].Value;
                cadastro = Db.Cadastro.SingleOrDefault(c => c.CadastroGuid == cadastroGuid);
                iniciando = false;
            }
            else
            {
                cadastroGuid = Guid.NewGuid().ToString();
                Response.Cookies.Add(new HttpCookie("CadastroGuid", cadastroGuid));
                cadastro = null;
                iniciando = true;
            }

            var model = new Models.CadastroIndexViewModel()
            {
                CadastroGuid = cadastroGuid, 
                Upline = upline, 
                Iniciando = iniciando, 
                Cadastro = cadastro
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult Persiste(bool finalizar, string cadastroGuid, long? upline, string nome, string sobrenome, 
            string cpf, string rg, string pisPasep, DateTime? dataNascimento, string cep, string endereco, string bairro, 
            string complemento, string cidade, string telCelular, string telResidencial, string telComercial, string email)
        {
            if (string.IsNullOrWhiteSpace(cadastroGuid)) cadastroGuid = null;
            if (string.IsNullOrWhiteSpace(nome)) nome = null;
            if (string.IsNullOrWhiteSpace(sobrenome)) sobrenome = null;
            if (string.IsNullOrWhiteSpace(cpf)) cpf = null;
            if (string.IsNullOrWhiteSpace(rg)) rg = null;
            if (string.IsNullOrWhiteSpace(pisPasep)) pisPasep = null;
            if (string.IsNullOrWhiteSpace(cep)) cep = null;
            if (string.IsNullOrWhiteSpace(endereco)) endereco = null;
            if (string.IsNullOrWhiteSpace(bairro)) bairro = null;
            if (string.IsNullOrWhiteSpace(complemento)) complemento = null;
            if (string.IsNullOrWhiteSpace(cidade)) cidade = null;
            if (string.IsNullOrWhiteSpace(telCelular)) telCelular = null;
            if (string.IsNullOrWhiteSpace(telResidencial)) telResidencial = null;
            if (string.IsNullOrWhiteSpace(telComercial)) telComercial = null;
            if (string.IsNullOrWhiteSpace(email)) email = null;

            if (finalizar)
            {
                if (cadastroGuid == null || nome == null || sobrenome == null || cpf == null || rg == null ||
                    pisPasep == null || !dataNascimento.HasValue || cep == null || endereco == null || bairro == null ||
                    complemento == null || cidade == null || telCelular == null || telResidencial == null ||
                    telComercial == null || email == null)
                    return Json(new { ok = false, resultado = Resultado.INFORMACAO_INSUFICIENTE.ToString() });
            }

            long? patrocinadorId = null;
            if (User.Identity.IsAuthenticated)
                patrocinadorId = Db.Cadastro.Single(c => c.MembershipUserName == User.Identity.Name).Id;

            string id;
            string senha = "123456";

            bool criado = false;
            var cadastro = Db.Cadastro.SingleOrDefault(c => c.CadastroGuid == cadastroGuid);
            if (cadastro == null)
            {
                criado = true;
                cadastro = new Db.Cadastro();
                cadastro.Finalizado = finalizar;
                cadastro.CadastroGuid = cadastroGuid;
                cadastro.DataCadastro = DateTime.Now;
                cadastro.Patrocinador = patrocinadorId;

                if (!finalizar)
                {
                    cadastro.Upline = upline;
                }
                else
                {
                    cadastro.Upline = EncontraUpline();
                }

                cadastro.Nome = nome;
                cadastro.Sobrenome = sobrenome;
                cadastro.Cpf = cpf;
                cadastro.Rg = rg;
                cadastro.PisPasep = pisPasep;
                cadastro.DataNascimento = dataNascimento;
                cadastro.Cep = cep;
                cadastro.Endereco = endereco;
                cadastro.Bairro = bairro;
                cadastro.Complemento = complemento;
                cadastro.Cidade = cidade;
                cadastro.TelCelular = telCelular;
                cadastro.TelResidencial = telResidencial;
                cadastro.TelComercial = telComercial;
                cadastro.Email = email;

                Db.Cadastro.InsertOnSubmit(cadastro);
                Db.SubmitChanges();

                id = cadastro.Id.ToString().PadLeft(7);

                cadastro.MembershipUserName = id;
                Db.SubmitChanges();

                Membership.CreateUser(id, senha, email);
            }
            else
            {
                // Só atualiza informações pessoais
                cadastro.DataCadastro = DateTime.Now;
                cadastro.Patrocinador = patrocinadorId;
                cadastro.Nome = nome;
                cadastro.Sobrenome = sobrenome;
                cadastro.Cpf = cpf;
                cadastro.Rg = rg;
                cadastro.PisPasep = pisPasep;
                cadastro.DataNascimento = dataNascimento;
                cadastro.Cep = cep;
                cadastro.Endereco = endereco;
                cadastro.Bairro = bairro;
                cadastro.Complemento = complemento;
                cadastro.Cidade = cidade;
                cadastro.TelCelular = telCelular;
                cadastro.TelResidencial = telResidencial;
                cadastro.TelComercial = telComercial;
                cadastro.Email = email;

                cadastro.MembershipUserName = cadastro.Id.ToString().PadLeft(7);

                // E upline, se informado
                if (!finalizar)
                {
                    cadastro.Upline = upline;
                }
                else
                {
                    cadastro.Upline = EncontraUpline();
                }

                Db.SubmitChanges();

                id = cadastro.Id.ToString().PadLeft(7);
            }

            return Json(new { ok = true, criado = criado, login = cadastro.Id.ToString().PadLeft(7), senha = senha });
        }

        long EncontraUpline()
        {
            return Db.Cadastro.First().Id;
        }
    }
}
