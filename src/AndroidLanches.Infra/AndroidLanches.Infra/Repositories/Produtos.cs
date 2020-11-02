using AndroidLanches.API.Domain.Repositories;
using AndroidLanches.Domain.Entities;
using AndroidLanches.Domain.Repositories;
using AndroidLanches.Infra.DBConfiguration;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AndroidLanches.Infra.Repositories
{
    public class Produtos : IProdutos
    {
        private IDbConnection _dbConnection;
        private readonly IDatabaseFactory _databaseFactory;
        public Produtos() { }

        public Produtos(IDatabaseFactory databaseOptions)
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

        public async Task AdicionarBebida(Bebida produto)
        {
            await _dbConnection.ExecuteAsync(
                "INSERT into Produtos(nome,descricao,preco,foto,tipo,embalagem) values " +
                "                   (@Nome,@Descricao,@Preco,@Foto,@Tipo,@Embalagem)", new
                {
                    produto.Nome,
                    produto.Descricao,
                    produto.Preco,
                    produto.Foto,
                    produto.Tipo,
                    produto.Embalagem
                });
        }

        public async Task AdicionarPrato(Prato produto)
        {
            await _dbConnection.ExecuteAsync(
                "INSERT into Produtos(nome,descricao,preco,foto,tipo,serveQuantasPessoas) values " +
                "                   (@Nome,@Descricao,@Preco,@Foto,@Tipo,@serveQuantasPessoas)", new
                {
                    produto.Nome,
                    produto.Descricao,
                    produto.Preco,
                    produto.Foto,
                    produto.Tipo,
                    produto.ServeQuantasPessoas
                });
        }
    }
}
