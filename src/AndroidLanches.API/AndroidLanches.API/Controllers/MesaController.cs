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
        private readonly IMesas _mesas;

        public MesaController(ILogger<MesaController> logger, IMesas mesas) :base(logger)
        {
            _mesas = mesas;
        }

        /// <summary>
        /// Retorna uma lista de mesas disponiveis
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Desocupadas")]
        public async Task<IEnumerable<Mesa>> ObterDesocupadas()
        {
            return await _mesas.ObterDesocupadas();
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> Post(int numero)
        {
            try
            {
                if (!await _mesas.Existe(numero))
                    await _mesas.Adicionar(new Mesa(numero));
                else
                    return BadRequest(new { erro = $"Já existe uma mesa com o número {numero}." });

                return Created("", new { });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { erro = BAD_REQUEST_MSG });
            }
        }

        public void Dispose()
        {
            _mesas.Dispose();
        }
    }
}
