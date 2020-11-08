using AndroidLanches.Domain.Entities;
using AndroidLanches.Domain.Repositories;
using AndroidLanches.Infra.DBConfiguration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AndroidLanches.API.Controllers
{
    public class SeedController : Controller
    {
        private readonly ICriadorBancoDeDados _criadorBancoDeDados;
        private readonly IProdutos _produtos;

        public SeedController(
            ICriadorBancoDeDados criadorBancoDeDados,
            IProdutos produtos)
        {
            _criadorBancoDeDados = criadorBancoDeDados;
            _produtos = produtos;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> Seed()
        {
            try
            {
                await _criadorBancoDeDados.Criar();

                await _produtos.AdicionarBebida(new Bebida("Coca cola", "Zero", 8, "2 litros", "cocacola_zero_2l"));
                await _produtos.AdicionarBebida(new Bebida("Coca cola", "Zero", 5, "600 ml", "cocacola_zero_600ml"));
                await _produtos.AdicionarBebida(new Bebida("Coca cola", "Zero Lata", 3, "350 ml", "cocacola_zero_350ml"));
                await _produtos.AdicionarBebida(new Bebida("Coca cola", "Normal", 8, "2 litros", "cocacola_2l"));
                await _produtos.AdicionarBebida(new Bebida("Coca cola", "Normal", 5, "600 ml", "cocacola_600ml"));
                await _produtos.AdicionarBebida(new Bebida("Coca cola", "Normal Lata", 3.5m, "350 ml", "cocacola_350ml"));

                await _produtos.AdicionarPrato(new Prato("Xis salada", "Hamburguer, alface, queijo, presunto, tomate, milho, erviolha, salada, acompanha fritas ", 20.0m, 1, "xis_salada"));
                await _produtos.AdicionarPrato(new Prato("Xis Calabresa", "Calabresa, alface, queijo, presunto, tomate, milho, erviolha, salada, acompanha fritas ", 20.0m, 1, "xis_calabresa"));
                await _produtos.AdicionarPrato(new Prato("Pizza Calabresa", "Calabresa, alface, queijo, presunto, tomate, milho, erviolha, salada, acompanha fritas ", 50.0m, 3, "pizza_calabresa"));
                await _produtos.AdicionarPrato(new Prato("Pizza Palmito", "Calabresa, alface, queijo, presunto, tomate, milho, erviolha, salada, acompanha fritas ", 50.0m, 3, "pizza_calabresa"));
                await _produtos.AdicionarPrato(new Prato("Pizza Bacon", "Calabresa, alface, queijo, presunto, tomate, milho, erviolha, salada, acompanha fritas ", 50.0m, 3, "pizza_calabresa"));
                await _produtos.AdicionarPrato(new Prato("Pizza 4 Queijos", "Calabresa, alface, queijo, presunto, tomate, milho, erviolha, salada, acompanha fritas ", 50.0m, 3, "pizza_calabresa"));
                await _produtos.AdicionarPrato(new Prato("Pizza 5 Queijos", "Calabresa, alface, queijo, presunto, tomate, milho, erviolha, salada, acompanha fritas ", 50.0m, 3, "pizza_calabresa"));
                await _produtos.AdicionarPrato(new Prato("Pizza 8 Queijos", "Calabresa, alface, queijo, presunto, tomate, milho, erviolha, salada, acompanha fritas ", 50.0m, 3, "pizza_calabresa"));

                return Created("",new { });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
