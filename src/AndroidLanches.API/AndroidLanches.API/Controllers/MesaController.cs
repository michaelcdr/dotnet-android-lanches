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
    public class MesaController : ControllerBase
    {
        private readonly ILogger<MesaController> _logger;
        private readonly IMesas _mesas;

        public MesaController(ILogger<MesaController> logger, IMesas mesas) 
        {
            _logger = logger;
            _mesas = mesas;
        }

        /// <summary>
        /// Retorna uma lista de mesas disponiveis
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        public async Task<IEnumerable<Mesa>> ObterDesocupadas()
        {
            return await _mesas.ObterDesocupadas();
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> Post(int numero)
        {
            try
            {
                await _mesas.Adicionar(new Mesa(numero));
                return Created("", new { });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Método responsavel por gerar mesas default do app
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GerarMesas")]
        public async Task<IActionResult> GerarMesas(int numeroDeMesas)
        {
            try
            {
                if (!await _mesas.TemAoMenosUma())
                    for (int i = 1; i <= numeroDeMesas; i++)
                        await _mesas.Adicionar(new Mesa(i));

                return Created("", new { });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
    }
}
