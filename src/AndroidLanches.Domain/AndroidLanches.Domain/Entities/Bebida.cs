namespace AndroidLanches.Domain.Entities
{
    public class Bebida:Produto
    {
        public string Embalagem { get; private set; }

        public Bebida() 
        {
        }

        public Bebida(string nome, string descricao, double preco, string embalagem, string foto)
        {
            this.Tipo = "bebida";
            this.Nome = nome;
            this.Descricao = descricao;
            this.Preco = preco;
            this.Embalagem = embalagem;
            this.Foto = foto;
        }

        public Bebida(int produtoId, string nome, string descricao, double preco, string embalagem)
        {
            this.ProdutoId = produtoId;
            this.Tipo = "bebida";
            this.Nome = nome;
            this.Descricao = descricao;
            this.Preco = preco;
            this.Embalagem = embalagem;
        }
    }
}
