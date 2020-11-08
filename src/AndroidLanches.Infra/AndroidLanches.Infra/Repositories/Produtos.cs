using AndroidLanches.Domain.Entities;
using AndroidLanches.Domain.Repositories;
using AndroidLanches.Infra.DBConfiguration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AndroidLanches.Infra.Repositories
{
    public class Produtos : BaseRepository, IProdutos,IDisposable
    {

        public Produtos(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
        public async Task AdicionarBebida(Bebida produto)
        {
            await Conexao.ExecuteAsync(
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
            await Conexao.ExecuteAsync(
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

        public async Task<List<Bebida>> ObterBebidas()
        {
            return (await Conexao.QueryAsync<Bebida>(
                "SELECT produtoId, nome, descricao, preco, foto, embalagem, tipo " +
                "FROM Produtos WHERE tipo = 'bebida' ORDER BY nome"
            )).ToList();
        }

        public async Task<List<Prato>> ObterPratos()
        {
            return (await Conexao.QueryAsync<Prato>(
                "SELECT produtoId, nome, descricao, preco, foto, serveQuantasPessoas, tipo " +
                "FROM produtos WHERE tipo = 'prato' ORDER BY nome"
            )).ToList();
        }
        public void Dispose()
        {
            Conexao.Close();
            GC.SuppressFinalize(Conexao);
        }
    }
}
