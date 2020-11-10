using AndroidLanches.API.Domain.Repositories;
using AndroidLanches.Domain.Entities;
using AndroidLanches.Infra.DBConfiguration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndroidLanches.Infra.Repositories
{
    public class Mesas: BaseRepository , IMesas
    {
        public Mesas(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
        public async Task<List<Mesa>> ObterDesocupadas()
        {
            var mesas = (await Conexao.QueryAsync<Mesa>
                ("SELECT * FROM Mesas WHERE  MesaId NOT IN (SELECT MesaId From Pedidos WHERE PAGO = 0 GROUP BY MesaId) ORDER BY Numero")).AsList();
            return mesas;
        }

        public async Task Adicionar(Mesa mesa)
        {
            await Conexao.ExecuteAsync("INSERT into Mesas(numero) values (@Numero)", new { mesa.Numero });            
        }

        public async Task<bool> TemAoMenosUma()
        {
            int quantidade = await Conexao.QuerySingleAsync<int>("select count(mesaid) from mesas");
            return quantidade > 1;
        }

        public async Task<bool> Existe(int numero)
        {
            bool existe = await Conexao.QuerySingleAsync<bool>("select CAST(COUNT(*) AS BIT) FROM Mesas where numero = @numero;", new { numero });
            return existe;
        }

        public async Task<Mesa> ObterDesocupadaPorNumero(int numeroMesa)
        {
            return await Conexao.QuerySingleOrDefaultAsync<Mesa>
                (@"SELECT * FROM Mesas WHERE  MesaId NOT IN (
                        SELECT MesaId From Pedidos WHERE PAGO = 0 ) and numero = @numeroMesa",new { numeroMesa });
        }

        public void Dispose()
        {
            Conexao.Close();
            Conexao.Dispose();
            GC.SuppressFinalize(Conexao);
        }

        public async Task<Mesa> ObterDesocupadaPorId(int mesaId)
        {
            return await Conexao.QuerySingleOrDefaultAsync<Mesa>
              (@"SELECT * FROM Mesas WHERE  MesaId NOT IN (
                        SELECT MesaId From Pedidos WHERE PAGO = 0 ) and MesaId = @mesaId", new { mesaId });
        }
    }
}
