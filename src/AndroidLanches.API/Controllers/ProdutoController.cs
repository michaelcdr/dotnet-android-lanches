using AndroidLanches.Domain.Entities;
using AndroidLanches.Domain.Filtros;
using AndroidLanches.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndroidLanches.API.Controllers
{
    [ApiController]
    [Route("v1/Produtos")]
    public class ProdutoController : BaseControllerApi<ProdutoController>, IDisposable
    {
        private readonly IProdutosRepository _produtos;

        public ProdutoController(ILogger<ProdutoController> logger, IProdutosRepository produtos):base(logger)
        {
            _produtos = produtos;
        }

        /// <summary>
        /// Retorna uma lista de bebidas disponiveis
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Bebidas")]
        public async Task<IEnumerable<Bebida>> ObterBebidas([FromQuery] FiltrosBebida parameters)
        {
            return await _produtos.ObterBebidas(parameters);
        }

        /// <summary>
        /// Retorna uma lista de pratos disponiveis
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Pratos")]
        public async Task<IEnumerable<Prato>> ObterPratos([FromQuery] FiltrosPrato filtros)
        {
            return await _produtos.ObterPratos(filtros);
        }

        public void Dispose()
        {
            _produtos.Dispose();
        }
    }
}
