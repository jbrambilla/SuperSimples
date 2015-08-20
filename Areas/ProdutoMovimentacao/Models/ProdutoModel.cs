using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetoSuperSimples.Modulos.ProdutoMovimentacao.Controller; 

namespace WebSuperSimplesMVC.Areas.ProdutoMovimentacao.Models
{
    public class ProdutoModel
    {
        private ProdutoController library;

        public ProdutoModel()
        {
            library = new ProdutoController();
        }


    }
}