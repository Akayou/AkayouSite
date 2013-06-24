using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkayouSite.Areas.PreCadastro.Models
{
    public class AdminIndexViewModel
    {
        public List<AkayouSite.Db.PreCadastro> PreCadastros { get; set; }
    }

    public class AdminEditaViewModel
    {
        public AkayouSite.Db.PreCadastro PreCadastro { get; set; }
    }
}