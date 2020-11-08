using AndroidLanches.Infra.DBConfiguration;
using System;
using System.Data;

namespace AndroidLanches.Infra.Repositories
{
    public abstract class BaseRepository 
    {
        protected IDbConnection _dbConnection;
        protected readonly IDatabaseFactory _databaseFactory;
       
        protected BaseRepository(IDatabaseFactory databaseOptions)
        {
            _databaseFactory = databaseOptions;
            _dbConnection = _databaseFactory.GetDbConnection;
        }

        private IDbConnection ObterConexao()
        {
            //if (_databaseFactory.GetDbConnection.State == ConnectionState.Closed)
            //    _databaseFactory.GetDbConnection.Open();
            _dbConnection = _databaseFactory.GetDbConnection;
            return _dbConnection;
        }

        public IDbConnection Conexao
        {
            get
            {
                return ObterConexao();
            }
        }

       
    }
}
