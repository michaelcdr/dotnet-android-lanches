using AndroidLanches.Domain.Entities;
using AndroidLanches.Domain.Repositories;
using AndroidLanches.Infra.DBConfiguration;
using AndroidLanches.Infra.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndroidLanches.Domain.Infra
{
    public class Pedidos : BaseRepository, IPedidos
    {
        public Pedidos(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }

        public async Task<List<Pedido>> ObterTodosSemPagamentoEfetuado()
        {
            List<Pedido> pedidos = (await Conexao.QueryAsync<Pedido>(
                   @"SELECT Pedidos.numero, Pedidos.pago, Mesas.mesaId, Mesas.numero as numeroMesa FROM Pedidos 
                INNER JOIN Mesas  ON Pedidos.mesaId = Mesas.mesaId 
                WHERE Pedidos.pago = 0 ORDER BY  Pedidos.numero")).AsList();

            return pedidos;
        }

        public async Task<List<Pedido>> ObterTodos()
        {
            List<Pedido> pedidos = (await Conexao.QueryAsync<Pedido>(
                @"SELECT Pedidos.numero, Pedidos.pago, Mesas.mesaId, Mesas.numero as numeroMesa FROM Pedidos 
                INNER JOIN Mesas  ON Pedidos.mesaId = Mesas.mesaId 
                ORDER BY  Pedidos.numero")).AsList();

            return pedidos;
        }

        public async Task<Pedido> Obter(int numeroPedido)
        {
            Pedido pedido =  await Conexao.QuerySingleOrDefaultAsync<Pedido>(
                @"SELECT Pedidos.numero, Pedidos.pago, Mesas.mesaId, Mesas.numero as numeroMesa FROM Pedidos  
                INNER JOIN Mesas  ON Pedidos.mesaId = Mesas.mesaId
                WHERE Pedidos.numero = @numeroPedido; ", new { numeroPedido }
            );
            if (pedido == null) return pedido;

            List<PedidoItem> itens = await ObterItensPedido(numeroPedido);
            foreach (PedidoItem item in itens)
                pedido.AdicionarItem(item);

            return pedido;
        }

        public async Task<List<PedidoItem>> ObterItensPedido(int numeroPedido)
        {
            dynamic resultados = (await Conexao.QueryAsync(
                @"SELECT pedidositens.PedidoItemId, pedidositens.Numero, pedidositens.Quantidade, 
                         pedidositens.ProdutoId, produtos.Nome, produtos.Foto , produtos.Preco 
                  FROM pedidositens 
                  INNER JOIN produtos ON produtos.produtoId  = pedidositens.produtoId 
                  WHERE pedidositens.numero = @numeroPedido", new { numeroPedido })).AsList();

            var itens = new List<PedidoItem>();
            foreach (var resultado in resultados)
            {
                itens.Add(new PedidoItem(
                    resultado.PedidoItemId,
                    resultado.Numero,
                    resultado.Quantidade,
                    resultado.ProdutoId,
                    resultado.Nome,
                    resultado.Foto,
                    resultado.Preco));
            }
            return itens;
        }

        public async Task<int> Criar(Pedido pedido)
        {
            int numeroPedido = await Conexao.QuerySingleAsync<int>(
                @"INSERT INTO Pedidos(mesaId,Pago) values (@mesaId, 0);
                SELECT CAST(SCOPE_IDENTITY() as int)", new { mesaId = pedido.Mesa.MesaId });

            foreach (PedidoItem item in pedido.Itens)
                await AdicionarItem(numeroPedido, item);
            
            return numeroPedido;
        }

        private async Task AdicionarItem(int numeroPedido, PedidoItem item)
        {
            await Conexao.ExecuteAsync(
                @"INSERT INTO PedidosItens(numero, quantidade, produtoId) values 
                                         (@numero, @quantidade, @produtoId);", new { 
                    numero = numeroPedido, 
                    quantidade =item.Quantidade, 
                    produtoId = item.ProdutoId 
            });
        }

        public async Task AdicionarItem(int numeroPedido, int produtoId)
        {
            await Conexao.ExecuteAsync(
                @"if (select quantidade from PedidosItens where produtoId = @produtoId and numero = @numeroPedido) is null
	                INSERT INTO PedidosItens(numero, quantidade, produtoId) values (@numeroPedido, 1, @produtoId);
                else
	                UPDATE PedidosItens SET Quantidade = Quantidade + 1 where produtoId = @produtoId and numero = @numeroPedido", 
                new { numeroPedido, produtoId }
            );
        }

        public async Task IncrementarQuantidadeProduto(int pedidoItemId)
        {
            await Conexao.ExecuteAsync(
                "UPDATE PedidosItens SET Quantidade = Quantidade + 1 where pedidoItemId = @pedidoItemId;",
                new { pedidoItemId }
            );
        }

        public async Task DecrementarQuantidadeProduto(int pedidoItemId)
        {
            await Conexao.ExecuteAsync(
                @"if (select quantidade from PedidosItens where pedidoItemId = @pedidoItemId) = 1
	                delete from PedidosItens where pedidoItemId = @pedidoItemId;
                else 
	                UPDATE PedidosItens SET Quantidade = Quantidade - 1 where pedidoItemId = @pedidoItemId;
                ", new { pedidoItemId });
        }

        public async Task<int> Criar(int mesaId, int produtoId)
        {
            int numeroPedido = await Conexao.QuerySingleAsync<int>(
                @"INSERT INTO Pedidos(mesaId,Pago) values (@mesaId, 0);
                SELECT CAST(SCOPE_IDENTITY() as int)", new { mesaId = mesaId });

            await AdicionarItem(numeroPedido, new PedidoItem(produtoId,1));

            return numeroPedido;
        }

        public void Dispose()
        {
            Conexao.Close();
            Conexao.Dispose();
            GC.SuppressFinalize(Conexao);
        }
    }
}