using System;

namespace AndroidLanches.Domain.Entities
{
    public class Produto
    {
        public int ProdutoId { get; protected set; }
        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }
        public double Preco { get; protected set; }
        public string Foto { get; protected set; }
        public string Tipo { get; protected set; }

        protected Produto() { }
    }
}
