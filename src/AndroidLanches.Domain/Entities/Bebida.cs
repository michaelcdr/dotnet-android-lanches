namespace AndroidLanches.Domain.Entities
{
    public class Bebida : Produto
    {
        public string Embalagem { get; private set; }

        protected Bebida() { }

        public Bebida(string nome, string descricao, decimal preco, string embalagem, string foto)
        {
            this.Tipo = "bebida";
            this.Nome = nome;
            this.Descricao = descricao;
            this.Preco = preco;
            this.Embalagem = embalagem;
            this.Foto = foto;
        }
    }
}
