using System.Web.Mvc;

namespace AkayouSite.Areas.PreCadastro
{
    public class PreCadastroAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PreCadastro";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "PreCadastro_default",
                "PreCad/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
