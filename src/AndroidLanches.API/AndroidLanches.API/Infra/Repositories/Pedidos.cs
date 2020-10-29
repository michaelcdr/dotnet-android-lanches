using AndroidLanches.API.Domain;
using AndroidLanches.API.Domain.Repositories;
using System.Collections.Generic;

namespace AndroidLanches.API.Infra.Repositories
{
    public class Pedidos:IPedidos
    {
        public Pedidos()
        {

        }

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
