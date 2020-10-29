using System;

namespace AndroidLanches.API.Domain
{
    public class Produto
    {
        public int ProdutoId { get; protected set; }
        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }
        public double Preco { get; protected set; }
        public string Foto { get; protected set; }
        public string Tipo { get; protected set; }

        public Produto() { }

        public Produto(string nome, String descricao, double preco)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Preco = preco;
        }
    }
}
