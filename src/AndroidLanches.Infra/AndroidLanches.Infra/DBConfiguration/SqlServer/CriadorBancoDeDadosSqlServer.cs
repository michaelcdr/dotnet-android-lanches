using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace AndroidLanches.Infra.DBConfiguration
{
    public class CriadorBancoDeDadosSqlServer: ICriadorBancoDeDados
    {
        private IDbConnection _dbConnection;
        private readonly IDatabaseFactory _databaseFactory;
        public CriadorBancoDeDadosSqlServer(IDatabaseFactory databaseOptions)
        {
            _databaseFactory = databaseOptions;
            _dbConnection = _databaseFactory.GetDbConnection;

            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
        }

        public async Task Criar()
        {
            string sqlCreateTableMesa = @"  IF	NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Mesas]')
	                                            AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
                                            CREATE TABLE [dbo].Mesas ( 
	                                            mesaId INT  NOT NULL IDENTITY PRIMARY KEY, 
	                                            numero INT NOT NULL
                                            );";

            string sqlCreateTableProduto = @"IF	NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Produtos]')
	                                            AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
                                             CREATE TABLE [dbo].Produtos ( 
                                                produtoId  INT NOT NULL IDENTITY PRIMARY KEY,
                                                nome VARCHAR(255) NOT NULL, 
                                                descricao VARCHAR(255) NOT NULL,
                                                foto VARCHAR(255),
                                                preco DECIMAL(10, 2), 
                                                tipo VARCHAR(255), 
                                                serveQuantasPessoas INT,
                                                embalagem VARCHAR(255)
                                             );";

            string sqlCreatePedidos = @"IF	NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Pedidos]')
	                                        AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
                                        CREATE TABLE [dbo].Pedidos ( 
	                                        numero INT NOT NULL IDENTITY PRIMARY KEY , 
	                                        pago INT NOT NULL, 
	                                        gorjeta DECIMAL(10, 2), 
	                                        mesaId INT 
                                        );";

            string sqlCreatePedidosItens = @"IF	NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[PedidosItens]')
	                                            AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
                                             CREATE TABLE [dbo].PedidosItens ( 
	                                            pedidoItemId INT NOT NULL IDENTITY PRIMARY KEY ,
	                                            numero INT NOT NULL, 
	                                            quantidade INT NOT NULL, 
	                                            produtoId INT NOT NULL, 
	                                            FOREIGN KEY (numero) REFERENCES Pedidos(numero), 
	                                            FOREIGN KEY (produtoId) REFERENCES Produtos(produtoid) 
                                            );";

            await _dbConnection.ExecuteAsync(sqlCreateTableMesa + sqlCreateTableProduto + sqlCreatePedidos + sqlCreatePedidosItens);
        }
    }
}
