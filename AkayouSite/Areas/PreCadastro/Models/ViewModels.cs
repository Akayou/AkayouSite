using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkayouSite.Areas.PreCadastro.Models
{
    public class AdminIndexViewModel
    {
        public List<AkayouSite.Db.Cadastro> PreCadastros { get; set; }
    }

    public class AdminEditaViewModel
    {
        public AkayouSite.Db.Cadastro PreCadastro { get; set; }
    }

    public class CadastroIndexViewModel
    {
        public string CadastroGuid { get; set; }
        public Db.Cadastro Cadastro { get; set; }
    }
}