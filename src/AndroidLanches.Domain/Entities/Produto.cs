namespace AndroidLanches.Domain.Entities
{
    public class Produto
    {
        public int ProdutoId { get; protected set; }
        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }
        public decimal Preco { get; protected set; }
        public string Foto { get; protected set; }
        public string Tipo { get; protected set; }

        protected Produto() { }

        public Produto(int produtoId, string nome, string foto, decimal preco)
        {
            ProdutoId = produtoId;
            Nome = nome;
            Foto = foto;
            Preco = preco;
        }
    }
}
