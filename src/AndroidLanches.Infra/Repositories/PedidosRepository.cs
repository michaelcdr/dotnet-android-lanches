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
    public class PedidosRepository : BaseRepository, IPedidosRepository
    {
        public PedidosRepository(IDatabaseFactory databaseFactory) : base(databaseFactory) {}

        public async Task<List<Pedido>> ObterTodosSemPagamentoEfetuado()
        {
            List<Pedido> pedidos = (await Conexao.QueryAsync<Pedido>(
                @"SELECT Pedidos.pedidoId, 
                         Pedidos.numero, 
                         Pedidos.pago, 
                         Mesas.mesaId, 
                         Mesas.numero as numeroMesa 
                FROM Pedidos 
                INNER JOIN Mesas  ON Pedidos.mesaId = Mesas.mesaId 
                WHERE Pedidos.pago = 0 ORDER BY  Pedidos.numero")).AsList();

            foreach (var pedido in pedidos)
            {
                List<PedidoItem> itens = await ObterItensPedido(pedido.Numero);
                foreach (var item in itens)
                    pedido.AdicionarItem(item);
            }
            return pedidos;
        }

        public async Task<List<Pedido>> ObterTodos()
        {
            List<Pedido> pedidos = (await Conexao.QueryAsync<Pedido>(
                @"SELECT Pedidos.pedidoId,
                         Pedidos.numero, 
                         Pedidos.pago, 
                         Mesas.mesaId, 
                         Mesas.numero as numeroMesa 
                FROM Pedidos 
                INNER JOIN Mesas  ON Pedidos.mesaId = Mesas.mesaId 
                ORDER BY  Pedidos.numero")).AsList();

            foreach (var pedido in pedidos)
            {
                List<PedidoItem> itens = await ObterItensPedido(pedido.Numero);
                foreach (var item in itens)
                    pedido.AdicionarItem(item);
            }
            return pedidos;
        }

        public async Task<Pedido> Obter(long numeroPedido)
        {
            Pedido pedido =  await Conexao.QuerySingleOrDefaultAsync<Pedido>(
                @"SELECT Pedidos.pedidoId, Pedidos.numero, Pedidos.pago, Mesas.mesaId, 
                         Mesas.numero as numeroMesa FROM Pedidos  
                INNER JOIN Mesas  ON Pedidos.mesaId = Mesas.mesaId
                WHERE Pedidos.numero = @numeroPedido; ", new { numeroPedido }
            );
            if (pedido == null) return pedido;

            List<PedidoItem> itens = await ObterItensPedido(numeroPedido);
            foreach (PedidoItem item in itens)
                pedido.AdicionarItem(item);

            return pedido;
        }

        public async Task<List<PedidoItem>> ObterItensPedido(long numeroPedido)
        {
            dynamic resultados = (await Conexao.QueryAsync(
                @"SELECT pedidositens.PedidoItemId, pedidositens.PedidoId, pedidositens.Quantidade, 
                         pedidositens.ProdutoId, produtos.Nome, produtos.Foto , produtos.Preco 
                  FROM pedidositens 
                  INNER JOIN produtos ON produtos.produtoId  = pedidositens.produtoId 
                  INNER JOIN pedidos on pedidositens.pedidoId =  pedidos.pedidoId  
                  WHERE pedidos.numero = @numeroPedido", new { numeroPedido })).AsList();

            var itens = new List<PedidoItem>();
            foreach (var resultado in resultados)
            {
                itens.Add(new PedidoItem(
                    resultado.PedidoItemId,
                    resultado.PedidoId,
                    resultado.Quantidade,
                    resultado.ProdutoId,
                    resultado.Nome,
                    resultado.Foto,
                    resultado.Preco));
            }
            return itens;
        }

        private long GerarNumeroPedido()
            => long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));

        public async Task<long> Criar(Pedido pedido)
        {
            long numeroPedido = GerarNumeroPedido();
            int pedidoId = await Conexao.QuerySingleAsync<int>(
                @"INSERT INTO Pedidos(mesaId,Pago,Numero) values (@mesaId, 0,@numero);
                SELECT CAST(SCOPE_IDENTITY() as int)", new { 
                    mesaId = pedido.Mesa.MesaId,
                    numero = numeroPedido
                });

            foreach (PedidoItem item in pedido.Itens)
                await AdicionarItem(pedidoId, item);
            
            return numeroPedido;
        }

        private async Task AdicionarItem(int pedidoId, PedidoItem item)
        {
            await Conexao.ExecuteAsync(
                @"INSERT INTO PedidosItens(pedidoId, quantidade, produtoId) values 
                                         (@pedidoId, @quantidade, @produtoId);", new {
                    pedidoId = pedidoId, 
                    quantidade =item.Quantidade, 
                    produtoId = item.ProdutoId 
            });
        }

        public async Task AdicionarItem(long numeroPedido, int produtoId)
        {
            int pedidoId = await Conexao.QuerySingleAsync<int>(
                "select pedidoId from pedidos where numero = @numeroPedido", new { numeroPedido });
            
            await Conexao.ExecuteAsync(
                @"if (select quantidade from PedidosItens where produtoId = @produtoId and pedidoId = @pedidoId) is null
	                INSERT INTO PedidosItens(pedidoId, quantidade, produtoId) values (@pedidoId, 1, @produtoId);
                else
	                UPDATE PedidosItens SET Quantidade = Quantidade + 1 where produtoId = @produtoId and pedidoId = @pedidoId", 
                new { pedidoId, produtoId }
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

        public async Task<long> Criar(int mesaId, int produtoId)
        {
            long numeroPedido = GerarNumeroPedido();
            int pedidoId = await Conexao.QuerySingleAsync<int>(
                @"INSERT INTO Pedidos(mesaId,Pago,numero) values (@mesaId, 0, @numeroPedido);
                SELECT CAST(SCOPE_IDENTITY() as int)", new { mesaId, numeroPedido });

            await AdicionarItem(pedidoId, new PedidoItem(produtoId,1));

            return numeroPedido;
        }

        public void Dispose()
        {
            Conexao.Close();
            Conexao.Dispose();
            GC.SuppressFinalize(Conexao);
        }

        public async Task Pagar(long numeroPedido)
        {
            await Conexao.ExecuteAsync("update pedidos set  pago = 1 where numero = @numeroPedido", new { numeroPedido });
        }

        public async Task PagarComGorjeta(long numeroPedido)
        {
            await Conexao.ExecuteAsync("update pedidos set gorjeta = 1 , pago = 1 where numero = @numeroPedido", new { numeroPedido });
        }
    }
}