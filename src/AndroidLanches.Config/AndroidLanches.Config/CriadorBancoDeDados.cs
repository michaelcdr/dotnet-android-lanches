using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AndroidLanches.Config
{
    public class CriadorBancoDeDados
    {
        private readonly IConfiguration _configuration;
        private const string USUARIO_DB = "androidlanches";
        private const string PWD_DB = "Teste@123";
        private const string SCHEMA_DB = "android-lanches";

        public CriadorBancoDeDados(IConfiguration configuration)
        {
            _configuration = configuration;
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
                                                  pago INT(11), 
                                                  gorjeta DECIMAL(10, 2), 
                                                  mesaId INT(11) 
                                            );";

            string sqlCreatePedidosItens = "CREATE TABLE IF NOT EXISTS  PedidosItens (" +
                     " pedidoItemId INT(11) NOT NULL AUTO_INCREMENT PRIMARY KEY ," +
                     " numero INT(11) NOT NULL, " +
                     " quantidade INT(11) NOT NULL, " +
                     " produtoId INT(11) NOT NULL, " +
                    "FOREIGN KEY (quantidade) REFERENCES Pedidos(numero), " +
                    "FOREIGN KEY (produtoId) REFERENCES Produtos(produtoid)" +
                    ");";


            using (var conn = new MySqlConnection($"Server=localhost;User Id={USUARIO_DB};Password={PWD_DB};Database={SCHEMA_DB};"))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    await conn.OpenAsync();

                using (var comando = new MySqlCommand())
                {
                    comando.Connection = conn;
                    comando.CommandText = sqlCreateTableMesa + sqlCreateTableProduto + sqlCreatePedidos + sqlCreatePedidosItens;
                    await comando.ExecuteNonQueryAsync();
                }
            }
            
        }
    }
}
