using AndroidLanches.Domain.Entities;
using AndroidLanches.Domain.Filtros;
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

        public async Task<List<Bebida>> ObterBebidas(FiltrosBebida filtros)
        {
            string sql = "SELECT produtoId, nome, descricao, preco, foto, embalagem, tipo " +
                "FROM Produtos WHERE tipo = 'bebida' ";

            var values = new DynamicParameters();
            if (!string.IsNullOrEmpty(filtros.Descricao))
            {
                sql += " and descricao like @Descricao ";
                values.AddDynamicParams(new { Descricao = "%" + filtros.Descricao + "%" });
            }

            if (!string.IsNullOrEmpty(filtros.Nome))
            {
                sql += " and Nome like @Nome ";
                values.AddDynamicParams(new { Nome = "%" + filtros.Nome + "%" });
            }

            if (!string.IsNullOrEmpty(filtros.Embalagem))
            {
                sql += " and Embalagem like @Embalagem ";
                values.AddDynamicParams(new { Embalagem = "%" + filtros.Embalagem + "%" });
            }

            sql += " ORDER BY nome";
            return (await Conexao.QueryAsync<Bebida>(sql, values)).ToList();
        }

        public async Task<List<Prato>> ObterPratos(FiltrosPrato filtros)
        {
            string sql = "SELECT produtoId, nome, descricao, preco, foto, serveQuantasPessoas, tipo " +
                "FROM produtos WHERE tipo = 'prato' ";

            var values = new DynamicParameters();

            if (!string.IsNullOrEmpty(filtros.Descricao))
            {
                sql += " and descricao like @Descricao ";
                values.AddDynamicParams(new { Descricao = "%" + filtros.Descricao + "%" });
            }

            if (!string.IsNullOrEmpty(filtros.Nome))
            {
                sql += " and Nome like @Nome ";
                values.AddDynamicParams(new { Nome = "%" + filtros.Nome + "%" });
            }

            sql += " ORDER BY nome ";

            return (await Conexao.QueryAsync<Prato>(sql, values)).ToList();

        }
        public void Dispose()
        {
            Conexao.Close();
            GC.SuppressFinalize(Conexao);
        }
    }
}
