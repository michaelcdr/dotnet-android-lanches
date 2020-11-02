using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AndroidLanches.Infra.DBConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace AndroidLanches.API.Controllers
{
    public class SeedController : Controller
    {
        private readonly ICriadorBancoDeDados _criadorBancoDeDados;

        public SeedController(ICriadorBancoDeDados criadorBancoDeDados)
        {
            _criadorBancoDeDados = criadorBancoDeDados;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> Seed()
        {
            try
            {
                await _criadorBancoDeDados.Criar();
                return Created("",new { });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
        }
    }
}
