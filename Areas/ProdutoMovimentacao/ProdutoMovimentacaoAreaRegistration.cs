using System.Web.Mvc;

namespace WebSuperSimplesMVC.Areas.ProdutoMovimentacao
{
    public class ProdutoMovimentacaoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ProdutoMovimentacao";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ProdutoMovimentacao_default",
                "ProdutoMovimentacao/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}