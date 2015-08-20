using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using PagedList;
using Rotativa;
using Rotativa.Options;


namespace WebSuperSimplesMVC.Areas.ProdutoMovimentacao.Controllers
{
    public class RelatorioController : Controller
    {
        ProjetoSuperSimples.Modulos.ProdutoMovimentacao.Controller.RelatorioMovimentacaoController library = new ProjetoSuperSimples.Modulos.ProdutoMovimentacao.Controller.RelatorioMovimentacaoController();
        // GET: ProdutoMovimentacao/Relatorio
        public ActionResult Index(string fkProduto, int? page, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.Produto = library.carregarProduto(fkProduto);
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            List<DataRow> movimentacoes = library.carregarMovimentacao(fkProduto).AsEnumerable().ToList();

            movimentacoes = (fromDate != null && toDate != null) ? filterDate(movimentacoes, fromDate.Value, toDate.Value) : movimentacoes;

            TempData["mov"] = movimentacoes;

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(movimentacoes.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult PdfAction(string id, DateTime? fromDate, DateTime? toDate)
        {
            var produto = library.carregarProduto(id);
            ViewBag.Produto = produto;
            ViewBag.InfoFilter = (fromDate != null) ? "filtrado de " + fromDate.Value.ToString() + " até " + toDate.Value.ToString() : "Todas as movimentações";

            var pdf = new ViewAsPdf
            {
                ViewName = "PdfAction",
                Model = TempData["mov"],
                FileName =  "Relatorio_" + produto.id +"_" + produto.nome + ".pdf",
                PageSize = Size.A4,
                IsGrayScale = true,
                PageMargins = new Margins { Bottom = 5, Left = 5, Right = 5, Top = 5 }
            };

            return pdf;
        }

        private List<DataRow> filterDate(List<DataRow> movimentacoes, DateTime t1, DateTime t2)
        {
            return movimentacoes.Where(
                    m =>
                    Convert.ToDateTime(m[4]).ToUniversalTime() >= t1.ToUniversalTime() &&
                    Convert.ToDateTime(m[4]).ToUniversalTime() <= t2.ToUniversalTime()
            ).ToList();
        }
    }
}