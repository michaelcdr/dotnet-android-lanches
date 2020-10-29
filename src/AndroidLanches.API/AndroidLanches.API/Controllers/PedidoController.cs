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
    public class PedidoController : ControllerBase
    {
        private  ILogger<PedidoController> _logger;
        private  IPedidos _pedidos;

        public PedidoController(ILogger<PedidoController> logger, IPedidos pedidos)
        {
            _logger = logger;
            _pedidos = pedidos;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_pedidos.ObterTodos());
        }
    }
}
