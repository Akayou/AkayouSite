using System.Data.Linq;

namespace AkayouSite.Db
{
    partial class Cadastro
    {
        public Cadastro CadastradoPorReference
        {
            get { return this._Cadastro3.Entity; }
        }

        public Cadastro SuperiorReference
        {
            get { return this._Cadastro1.Entity; }
        }
    }
}
