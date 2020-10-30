namespace AndroidLanches.Domain.Entities
{
    public class Mesa
    {
        public int MesaId { get;  set; }
        public int Numero { get;  set; }

        public Mesa(int numero)
        {
            this.Numero = numero;
        }
        public Mesa()
        {

        }
    }
}
