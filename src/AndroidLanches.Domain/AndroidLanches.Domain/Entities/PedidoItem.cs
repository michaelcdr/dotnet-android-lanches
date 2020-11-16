using System;

namespace AndroidLanches.Domain.Entities
{
    public class PedidoItem
    {
        public int PedidoItemId { get; private set; }
        public int PedidoId { get; private set; }
        public int Quantidade { get; private set; }
        public Produto Produto { get; private set; }
        public int ProdutoId { get; set; }

        public PedidoItem(int pedidoItemId, int pedidoId, int quantidade, int produtoId, string nome, string foto, decimal preco)
        {
            this.PedidoItemId = pedidoItemId;
            this.PedidoId = pedidoId;
            this.ProdutoId = produtoId;
            this.Quantidade = quantidade;
            this.Produto = new Produto(produtoId, nome, foto, preco);
        }
        public PedidoItem( int produtoId, int quantidade)
        {
            this.ProdutoId = produtoId;
            this.Quantidade = quantidade;
        }

        public void Incrementar(int quantidade)
        {
            this.Quantidade += quantidade;
        }
    }


}

