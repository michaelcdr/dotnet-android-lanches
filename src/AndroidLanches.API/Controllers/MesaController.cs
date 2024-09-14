using AndroidLanches.API.Domain.Repositories;
using AndroidLanches.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndroidLanches.API.Controllers
{
    [ApiController]
    [Route("v1/Mesas")]
    public class MesaController : BaseControllerApi<MesaController>, IDisposable
    {
        private readonly IMesasRepository _mesasRepository;

        public MesaController(ILogger<MesaController> logger, 
                              IMesasRepository mesas) :base(logger)
        {
            _mesasRepository = mesas;
        }

        /// <summary>
        /// Retorna uma lista de mesas disponiveis.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Desocupadas")]
        public async Task<IEnumerable<Mesa>> ObterDesocupadas()
        {
            return await _mesasRepository.ObterDesocupadas();
        }

        /// <summary>
        /// Cadastra uma mesa.
        /// </summary>
        /// <param name="numero">Numero para mesa desejado.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] int numero)
        {
            if (numero == 0) return BadRequest("O numero deve ser maior que 0.");

            if (!await _mesasRepository.Existe(numero))
                await _mesasRepository.Adicionar(new Mesa(numero));
            else
                return BadRequest(new { erro = $"Já existe uma mesa com o número {numero}." });

            return Created("", new { });
        }

        public void Dispose()
        {
            _mesasRepository.Dispose();
        }
    }
}
