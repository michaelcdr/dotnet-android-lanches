using AndroidLanches.Domain.Entities;
using System.Collections.Generic;

namespace AndroidLanches.Domain.Extensions
{
    public static class PedidoExtensions
    {
        public static PedidoComItens ToPedidoComItens(this Pedido pedido, List<PedidoItemComProduto> pedidoItems)
        {
            return new PedidoComItens(pedido, pedidoItems);
        }
    }
}
