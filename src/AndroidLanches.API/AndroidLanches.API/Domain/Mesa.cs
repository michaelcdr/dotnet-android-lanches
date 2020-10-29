namespace AndroidLanches.API.Domain
{
    public class Mesa
    {
        public int MesaId { get; private set; }
        public int Numero { get; private set; }

        public Mesa(int numero)
        {
            this.Numero = numero;
        }
    }
}
