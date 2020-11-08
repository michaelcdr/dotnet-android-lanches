using AndroidLanches.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndroidLanches.Domain.Repositories
{
    public interface IPedidos
    {
        Task<Pedido> Obter(int numeroPedido);
        Task<List<PedidoItem>> ObterItensPedido(int numeroPedido);
        Task<List<Pedido>> ObterTodos();
        Task<List<Pedido>> ObterTodosSemPagamentoEfetuado();
        Task<int> Criar(Pedido pedido);
        Task DeletarItem(int numeroPedido, int idProduto);
        Task IncrementarQuantidadeProduto(int pedidoItemId);
        Task DecrementarQuantidadeProduto(int pedidoItemId);
    }
}
