namespace AndroidLanches.Domain.Entities
{
    public class PedidoItemComProduto
    {
        public int PedidoItemId { get; private set; }
        public int Numero { get; private set; }
        public int Quantidade { get; private set; }
        public string Nome { get; private set; }
        public string Foto { get; private set; }
        public decimal Preco { get; private set; }
        public int ProdutoId { get; private set; }

        public PedidoItemComProduto(
            int pedidoItemId, int numero, int quantidade, int produtoId,
            string nome, string foto, decimal preco)
        {
            this.PedidoItemId = pedidoItemId;
            this.Numero = numero;
            this.Quantidade = quantidade;
            this.ProdutoId = produtoId;
            this.Nome = nome;
            this.Foto = foto;
            this.Preco = preco;
        }

    }
}

