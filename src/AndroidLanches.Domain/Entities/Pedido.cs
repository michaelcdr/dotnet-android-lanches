using System.Collections.Generic;
using System.Linq;

namespace AndroidLanches.Domain.Entities
{
    public class Pedido
    {
        public int PedidoId { get; private set; }
        public long Numero { get; private set; }
        public bool Pago { get; private set; }
        public Mesa Mesa { get; private set; }
        public bool Gorjeta { get; private set; }
        public List<PedidoItem> Itens { get; private set; }

        protected Pedido()
        {

        } 

        public Pedido(Mesa mesa, List<PedidoItem> itens)
        {
            this.Pago = false;
            this.Mesa = mesa;
            this.Itens = new List<PedidoItem>();

            if (itens != null && itens.Count> 0)
                foreach (PedidoItem item in itens) 
                    AdicionarItem(item);
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            if (this.Itens.Any(item => item.ProdutoId == pedidoItem.ProdutoId))
            {
                this.Itens.Single(item => item.ProdutoId == pedidoItem.ProdutoId)
                          .Incrementar(pedidoItem.Quantidade);
            }
            else
                this.Itens.Add(pedidoItem); 
        }
    }
}