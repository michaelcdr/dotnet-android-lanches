using AndroidLanches.API.Domain.Repositories;
using AndroidLanches.Domain.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace AndroidLanches.Infra.Repositories
{
    public class Mesas : IMesas
    {
        private readonly IDbConnection _dbConnection;
        public Mesas()
        {

        }
        public Mesas(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Mesa> ObterDesocupadas()
        {
            return new List<Mesa>();
        }

        public async Task Adicionar(Mesa mesa)
        {
            await _dbConnection.ExecuteAsync("INSERT into Mesas(numero) values (@Numero)", new { Numero = mesa.Numero });            
        }
    }
}
