using AndroidLanches.Domain.Entities;
using AndroidLanches.Domain.Repositories;
using AndroidLanches.Infra.Repositories;
using System.Collections.Generic;

namespace AndroidLanches.Domain.Infra
{
    public class Pedidos : BaseRepository, IPedidos
    {
        public List<Pedido> ObterTodosPedidosSemPagamentoEfetuado()
        {
            var pedidos = new List<Pedido>();
            return pedidos;
        }

        public List<Pedido> ObterTodos()
        {
            var pedidos = new List<Pedido>();
            pedidos.Add(new Pedido(1, false, new Mesa(10)));
            pedidos.Add(new Pedido(2, false, new Mesa(11)));
            pedidos.Add(new Pedido(3, false, new Mesa(12)));
            return pedidos;
        }

        public Pedido Obter(int numeroPedido)
        {
            return new Pedido(0,false,new Mesa(10));
        }
    }
}
