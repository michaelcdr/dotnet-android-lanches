using AndroidLanches.Domain.Entities;
using System.Collections.Generic;

namespace AndroidLanches.Domain.Repositories
{
    public interface IPedidos
    {
        Pedido Obter(int numeroPedido);
        List<Pedido> ObterTodos();
        List<Pedido> ObterTodosPedidosSemPagamentoEfetuado();
    }
}
