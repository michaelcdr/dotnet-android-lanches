namespace AndroidLanches.Domain.Entities
{
    public class Prato : Produto
    {
        public int ServeQuantasPessoas { get; private set; }

        protected Prato() { }
        
        public Prato(string nome, string descricao, double preco, int serveQuantasPessoas, string foto)
        {
            this.Tipo = "prato";
            this.Nome = nome;
            this.Descricao = descricao;
            this.Preco = preco;
            this.ServeQuantasPessoas = serveQuantasPessoas;
            this.Foto = foto;
        }
    }
}
