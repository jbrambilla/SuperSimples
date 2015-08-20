using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetoSuperSimples.Modulos.ProdutoMovimentacao.Model.Entidades;
using PagedList;

namespace WebSuperSimplesMVC.Areas.ProdutoMovimentacao.Controllers
{
    public class ProdutoController : Controller
    {
        private ProjetoSuperSimples.Modulos.ProdutoMovimentacao.Controller.ProdutoController library = new ProjetoSuperSimples.Modulos.ProdutoMovimentacao.Controller.ProdutoController();

        private ProjetoSuperSimples.Modulos.ProdutoMovimentacao.Controller.LocalizarProdutoController localizar = new ProjetoSuperSimples.Modulos.ProdutoMovimentacao.Controller.LocalizarProdutoController();

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(string search, string linq, string sortOrder, string currentFilter, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.EstoqueSortParam = sortOrder == "estoque" ? "estoque_desc" : "estoque";

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;

            List<Produto> produtos = localizar.produtos;

            if (!String.IsNullOrEmpty(search))
            {
                produtos = localizar.buscarProduto(search, (linq == "1"));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    produtos = produtos.OrderByDescending(p => p.nome).ToList();
                    break;
                case "estoque":
                    produtos = produtos.OrderBy(p => p.estoque).ToList();
                    break;
                case "estoque_desc":
                    produtos = produtos.OrderByDescending(p => p.estoque).ToList();
                    break;
                default:
                    produtos = produtos.OrderBy(p => p.nome).ToList();
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);


            return View(produtos.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult InsertAction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertAction(ProjetoSuperSimples.Modulos.ProdutoMovimentacao.Model.Entidades.Produto produto)
        {

            if (library.salvarProduto(produto))
            {
                TempData["Mensagem"] = "Produto " + produto.nome + " salvo com sucesso!";
                TempData["Controller"] = "Movimentacao";
                TempData["Acao"] = "Index";
                TempData["Produto"] = (produto.id == 0) ? library.getLastProductId() : produto.id;
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        public ActionResult EditAction(string id)
        {
            return View(library.carregarProduto(id));
        }

        public ActionResult DeleteAction(string id)
        {
            var produto = library.carregarProduto(id);
            if (library.excluirProduto(id))
            {
                TempData["ExclusaoS"] = "Produto " + produto.nome + " excluido com sucesso!";
            } else
            {
                TempData["ExclusaoN"] = "Falha ao excluir o produto " + produto.nome;
            }
            return RedirectToAction("Index");
        }
    }
}