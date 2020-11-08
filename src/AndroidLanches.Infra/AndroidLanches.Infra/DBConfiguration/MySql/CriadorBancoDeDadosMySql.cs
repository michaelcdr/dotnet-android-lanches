using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace AndroidLanches.Infra.DBConfiguration
{

    public class CriadorBancoDeDadosMySql : ICriadorBancoDeDados
    {
        private IDbConnection _dbConnection;
        private readonly IDatabaseFactory _databaseFactory;
        public CriadorBancoDeDadosMySql(IDatabaseFactory databaseOptions)
        {
            _databaseFactory = databaseOptions;
            _dbConnection = _databaseFactory.GetDbConnection;

            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
        }

        public async Task Criar()
        {
            string sqlCreateTableMesa = "CREATE TABLE IF NOT EXISTS  Mesas (mesaId INT(11) NOT NULL AUTO_INCREMENT PRIMARY KEY , numero INT(11) NOT NULL); ";

            string sqlCreateTableProduto = @"CREATE TABLE IF NOT EXISTS  Produtos (
                                                produtoid  INT(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
                                                nome VARCHAR(255) NOT NULL, 
                                                descricao VARCHAR(255) NOT NULL,
                                                foto VARCHAR(255),
                                                preco DECIMAL(10, 2), 
                                                tipo VARCHAR(255), 
                                                serveQuantasPessoas INT(11),
                                                embalagem VARCHAR(255)
                                            );";

            string sqlCreatePedidos = @"CREATE TABLE IF NOT EXISTS  Pedidos (
                numero INT(11) NOT NULL AUTO_INCREMENT PRIMARY KEY , 
                pago BIT NOT NULL, 
                gorjeta DECIMAL(10, 2), 
                mesaId INT(11) 
            );";

            string sqlCreatePedidosItens = @"CREATE TABLE IF NOT EXISTS  PedidosItens (
                pedidoItemId INT(11) NOT NULL AUTO_INCREMENT PRIMARY KEY ,
                numero INT(11) NOT NULL, 
                quantidade INT(11) NOT NULL, 
                produtoId INT(11) NOT NULL, 
                FOREIGN KEY (numero) REFERENCES Pedidos(numero), 
                FOREIGN KEY (produtoId) REFERENCES Produtos(produtoid)
            );";

            await _dbConnection.ExecuteAsync(sqlCreateTableMesa + sqlCreateTableProduto + sqlCreatePedidos + sqlCreatePedidosItens);
        }
    }
}
