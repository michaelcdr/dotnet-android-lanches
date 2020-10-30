namespace AndroidLanches.Domain.Entities
{
    public class PedidoItem
    {
        public int PedidoItemId { get; private set; }
        public int NumeroPedido { get; private set; }
        public int Quantidade { get; private set; }
        public Produto Produto { get; private set; }

        public PedidoItem(int numeroPedido, int quantidade)
        {
            this.NumeroPedido = numeroPedido;
            this.Quantidade = quantidade;
        }

    }
}
