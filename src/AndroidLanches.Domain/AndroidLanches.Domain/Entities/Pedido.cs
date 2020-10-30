using System.Collections.Generic;

namespace AndroidLanches.Domain.Entities
{
    public class Pedido
    {
        public int Numero { get; private set; }
        public bool Pago { get; private set; }
        public Mesa Mesa { get; private set; }
        public double Gorjeta { get; private set; }
        public List<PedidoItem> Itens { get; private set; }

        public Pedido(int numero, bool pago, Mesa mesa)
        {
            this.Numero = numero;
            this.Pago = pago;
            this.Mesa = mesa;
        }
    }
}
