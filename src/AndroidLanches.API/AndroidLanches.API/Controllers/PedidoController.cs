using AndroidLanches.API.Domain.Repositories;
using AndroidLanches.Domain.Entities;
using AndroidLanches.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndroidLanches.API.Controllers
{
    [ApiController]
    [Route("v1/pedidos")]
    public partial class PedidoController : ControllerBase
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IPedidos _pedidos;
        private readonly IMesas _mesas;

        public PedidoController(
            ILogger<PedidoController> logger, 
            IPedidos pedidos, 
            IMesas mesas
            )
        {
            _logger = logger;
            _pedidos = pedidos;
            _mesas = mesas;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _pedidos.ObterTodos());

        [HttpGet,Route("ObterPorNumero/{numeroPedido}")]
        public async Task<IActionResult> Get(int numeroPedido)
            => Ok(await _pedidos.Obter(numeroPedido));

        [HttpGet,Route("ObterTodosSemPagamentoEfetuado")]
        public async Task<IActionResult> ObterTodosSemPagamentoEfetuado()
            => Ok(await _pedidos.ObterTodosSemPagamentoEfetuado());

        [HttpPost, Route("")]
        public async Task<IActionResult> Criar(PedidoInputModel model)
        {
            try
            {
                List<PedidoItem> itens = model.Item.Select(e => new PedidoItem(e.ProdutoId, e.Quantidade)).ToList();

                Mesa mesa = await _mesas.ObterDesocupadaPorNumero(model.NumeroMesa);

                if (mesa == null) return BadRequest(new { erro = "Mesa indisponivel" });

                int pedidoId = await _pedidos.Criar(new Pedido(mesa, itens));

                return Created("", new { pedidoId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut, Route("IncrementarQuantidadeProduto/{pedidoItemId}")]
        public async Task<IActionResult> IncrementarQuantidadeProduto(int pedidoItemId)
        {
            try
            {
                await _pedidos.IncrementarQuantidadeProduto(pedidoItemId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { erro = "Não foi possivel atualizar a quantidade do produto." });
            }
        }

        [HttpPut, Route("DecrementarQuantidadeProduto/{pedidoItemId}")]
        public async Task<IActionResult> DecrementarQuantidadeProduto(int pedidoItemId)
        {
            try
            {
                await _pedidos.DecrementarQuantidadeProduto(pedidoItemId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { erro = "Não foi possivel atualizar a quantidade do produto." });
            }
        }
    }
}
