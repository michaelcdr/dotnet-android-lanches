using System.Collections.Generic;

namespace AndroidLanches.API.Controllers
{
    public class PedidoInputModel
    {
        public int NumeroMesa { get; set; }
        public List<PedidoItemInputModel> Item { get; set; }
    }
}
