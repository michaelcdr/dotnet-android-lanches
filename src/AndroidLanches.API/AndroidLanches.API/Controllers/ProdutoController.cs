using AndroidLanches.Domain.Entities;
using AndroidLanches.Domain.Filtros;
using AndroidLanches.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndroidLanches.API.Controllers
{
    [ApiController]
    [Route("v1/Produtos")]
    public class ProdutoController : Controller
    {
        private readonly IProdutos _produtos;

        public ProdutoController(IProdutos produtos)
        {
            _produtos = produtos;
        }

        /// <summary>
        /// Retorna uma lista de bebidas disponiveis
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Bebidas")]
        public async Task<IEnumerable<Bebida>> ObterBebidas([FromQuery] FiltrosBebida filtros)
        {
            return await _produtos.ObterBebidas(filtros);
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
    }
}
