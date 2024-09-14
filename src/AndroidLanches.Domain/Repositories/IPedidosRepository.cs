using AndroidLanches.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndroidLanches.Domain.Repositories
{
    public interface IPedidosRepository
    {
        Task<Pedido> Obter(long numeroPedido);
        Task<List<PedidoItem>> ObterItensPedido(long numeroPedido);
        Task<List<Pedido>> ObterTodos();
        Task<List<Pedido>> ObterTodosSemPagamentoEfetuado();
        Task<long> Criar(Pedido pedido);
        Task<long> Criar(int mesaId, int produtoId);
        Task AdicionarItem(long numeroPedido, int produtoId);
        Task IncrementarQuantidadeProduto(int pedidoItemId);
        Task DecrementarQuantidadeProduto(int pedidoItemId);
        Task Pagar(long numeroPedido);
        Task PagarComGorjeta(long numeroPedido);
    }
}
