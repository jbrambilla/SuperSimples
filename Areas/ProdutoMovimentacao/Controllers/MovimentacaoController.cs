using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSuperSimplesMVC.Areas.ProdutoMovimentacao.Controllers
{
    public class MovimentacaoController : Controller
    {
        ProjetoSuperSimples.Modulos.ProdutoMovimentacao.Controller.MovimentacaoController library = new ProjetoSuperSimples.Modulos.ProdutoMovimentacao.Controller.MovimentacaoController();
        // GET: ProdutoMovimentacao/Movimentacao
        public ActionResult Index(string fkProduto)
        {
            ProjetoSuperSimples.Modulos.ProdutoMovimentacao.Model.Entidades.Produto produto = library.carregarProduto(fkProduto);

            ViewBag.Produto = produto;

            return View();
        }

        [HttpPost]
        public ActionResult InsertAction(ProjetoSuperSimples.Modulos.ProdutoMovimentacao.Model.Entidades.Movimentacao movimentacao)
        {
            if (ModelState.IsValid && library.salvarMovimentacao(movimentacao))
            {
                TempData["Mensagem"] = "Movimentacao do produto de Id " + movimentacao.fkProduto + " cadastrada com sucesso!";
                TempData["Acao"] = "Index";
                TempData["Controller"] = "Relatorio";
                TempData["Produto"] = movimentacao.fkProduto;
                return RedirectToAction("Index", "Produto");
            }
            return View(movimentacao);
        }
    }
}