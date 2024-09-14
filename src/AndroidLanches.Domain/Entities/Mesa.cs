namespace AndroidLanches.Domain.Entities
{
    public class Mesa
    {
        public int MesaId { get; private set; }
        public int Numero { get; private set; }

        public Mesa(int mesaId, int numero)
        {
            this.MesaId = mesaId;
            this.Numero = numero;
        }
        public Mesa(int numero)
        {
            this.Numero = numero;
        }
    }
}
