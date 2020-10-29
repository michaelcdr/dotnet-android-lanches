using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndroidLanches.API.Domain.Repositories
{
    public interface IPedidos
    {
        Pedido Obter(int numeroPedido);
        List<Pedido> ObterTodos();
        List<Pedido> ObterTodosPedidosSemPagamentoEfetuado();
    }
}
