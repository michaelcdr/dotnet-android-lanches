using System.Collections.Generic;

namespace AndroidLanches.API.Controllers
{
    public class PedidoInputModel
    {
        public int NumeroMesa { get; set; }
        public List<PedidoItemModel> Item { get; set; }
    }
}
