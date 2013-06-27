using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkayouSite.Areas.PreCadastro.Models
{
    public class AdminIndexViewModel
    {
        public List<AkayouSite.Db.Cadastro> Cadastros { get; set; }
    }

    public class AdminEditaViewModel
    {
        public AkayouSite.Db.Cadastro Cadastro { get; set; }
    }

    public class HomeIndexViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class CadastroIndexViewModel
    {
        public string CadastroGuid { get; set; }
        public long? Upline { get; set; }
        public bool Iniciando { get; set; }
        public Db.Cadastro Cadastro { get; set; }
    }

    public class PerfilIndexViewModel
    {
        public Db.Cadastro Cadastro { get; set; }
        public IEnumerable<Db.Cadastro> PodeCadastrar { get; set; }
        public int VolumePernaEsquerda { get; set; }
        public int VolumePernaDireita { get; set; }
    }
}