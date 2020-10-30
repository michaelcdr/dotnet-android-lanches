using AndroidLanches.API.Domain;
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

        [HttpGet]
        public IEnumerable<Mesa> ObterDesocupadas()
        {
            var rng = new Random();
            return _mesas.ObterDesocupadas();
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(Mesa mesa)
        {
            try
            {
                await _mesas.Adicionar(mesa);
                return Created("",new { });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            
        }
    }
}
