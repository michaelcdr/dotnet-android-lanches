using AndroidLanches.API.Domain.Repositories;
using AndroidLanches.Domain.Entities;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndroidLanches.Infra.Repositories
{
    public class Mesas: BaseRepository , IMesas
    {
        public async Task<List<Mesa>> ObterDesocupadas()
        {
            var mesas = (await Conexao.QueryAsync<Mesa>
                ("SELECT * FROM Mesas WHERE  MesaId NOT IN (SELECT MesaId From Pedidos WHERE PAGO = 0 GROUP BY MesaId) ORDER BY Numero")).ToList();
            return mesas;
        }

        public async Task Adicionar(Mesa mesa)
        {
            await _dbConnection.ExecuteAsync("INSERT into Mesas(numero) values (@Numero)", new { mesa.Numero });            
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
    }
}
