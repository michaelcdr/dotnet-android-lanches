using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AndroidLanches.API.Domain;
using AndroidLanches.API.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AndroidLanches.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
