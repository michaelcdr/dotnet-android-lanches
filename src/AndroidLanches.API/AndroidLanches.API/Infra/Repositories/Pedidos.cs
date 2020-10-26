using AndroidLanches.API.Domain;
using AndroidLanches.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public Pedido Obter(int numeroPedido)
        {
            return new Pedido(0,false,new Mesa(10));
        }
    }
}
