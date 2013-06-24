using System.Data.Linq;

namespace AkayouSite.Db
{
    partial class PreCadastro
    {
        public PreCadastro CadastradoPorReference
        {
            get { return this._PreCadastro3.Entity; }
        }

        public PreCadastro SuperiorReference
        {
            get { return this._PreCadastro1.Entity; }
        }
    }
}
