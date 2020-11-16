using System.Collections.Generic;

namespace AndroidLanches.Domain.Entities
{
    public class PedidoComItens
    {
        public long Numero { get; private set; }
        public bool Pago { get; private set; }
        public Mesa Mesa { get; private set; }
        public bool Gorjeta { get; private set; }
        public List<PedidoItemComProduto> Itens { get; private set; }
        public PedidoComItens(Pedido pedido, List<PedidoItemComProduto> itens)
        {
            this.Numero = pedido.Numero;
            this.Pago = pedido.Pago;
            this.Mesa = pedido.Mesa;
            this.Gorjeta = pedido.Gorjeta;
            this.Itens = itens;
        }
    }
}

