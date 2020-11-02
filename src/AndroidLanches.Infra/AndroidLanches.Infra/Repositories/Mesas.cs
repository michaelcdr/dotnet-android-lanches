using AndroidLanches.API.Domain.Repositories;
using AndroidLanches.Domain.Entities;
using AndroidLanches.Infra.DBConfiguration;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AndroidLanches.Infra.Repositories
{
    public class Mesas : IMesas
    {
        private IDbConnection _dbConnection;
        private readonly IDatabaseFactory _databaseFactory;
        public Mesas() { }

        public Mesas(IDatabaseFactory databaseOptions)
        {
            _databaseFactory = databaseOptions;
            _dbConnection = _databaseFactory.GetDbConnection;

            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
        }

        private IDbConnection ObterConexao()
        {
            if (_databaseFactory.GetDbConnection.State == ConnectionState.Closed)
                _databaseFactory.GetDbConnection.Open();
            _dbConnection = _databaseFactory.GetDbConnection;
            return _dbConnection;
        }

        public async Task<List<Mesa>> ObterDesocupadas()
        {
            var mesas = (await ObterConexao().QueryAsync<Mesa>("SELECT * FROM Mesas WHERE  MesaId NOT IN (SELECT MesaId From Pedidos WHERE PAGO = 0 GROUP BY MesaId) ORDER BY Numero")).ToList();
            return mesas;
        }

        public async Task Adicionar(Mesa mesa)
        {
            await _dbConnection.ExecuteAsync("INSERT into Mesas(numero) values (@Numero)", new { mesa.Numero });            
        }

        public async Task<bool> TemAoMenosUma()
        {
            int quantidade = await ObterConexao().QuerySingleAsync<int>("select count(mesaid) from mesas");
            return quantidade > 1;
        }

        public async Task<bool> Existe(int numero)
        {
            bool existe = await ObterConexao().QuerySingleAsync<bool>("select CAST(COUNT(*) AS BIT) FROM Mesas where numero = @numero;", new { numero });
            return existe;
        }
    }
}
