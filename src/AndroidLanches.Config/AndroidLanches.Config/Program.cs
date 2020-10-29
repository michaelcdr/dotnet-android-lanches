using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AndroidLanches.Config
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            //criando base de dados
            try
            {
                var criador = new CriadorBancoDeDados(configuration);
                await criador.Criar();

                Console.WriteLine("BANCO DE DADOS CRIADO COM SUCESSO");
            }
            catch (Exception ex)
            {
                Console.WriteLine("BANCO DE DADOS não foi criado");
                Console.WriteLine("erro: " + ex.Message);
            }
        }
    }
}
