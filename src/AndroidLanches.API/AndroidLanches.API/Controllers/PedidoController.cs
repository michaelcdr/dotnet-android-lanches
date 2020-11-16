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

        public PedidoController(IPedidos pedidos, IMesas mesas, ILogger<PedidoController> logger)
        {
            _logger = logger;
            _pedidos = pedidos;
            _mesas = mesas;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _pedidos.ObterTodos());

        [HttpGet, Route("ObterPorNumero/{numeroPedido}")]
        public async Task<IActionResult> Get(long numeroPedido)
        {
            try
            {
                Pedido pedido = await _pedidos.Obter(numeroPedido);
                if (pedido == null) return NotFound();

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet,Route("ObterTodosSemPagamentoEfetuado")]
        public async Task<IActionResult> ObterTodosSemPagamentoEfetuado()
            => Ok(await _pedidos.ObterTodosSemPagamentoEfetuado());

        [HttpPost, Route("")]
        public async Task<IActionResult> Criar(PedidoInputModel model)
        {
            try
            {
                var itens = model.Item.Select(e => new PedidoItem(e.ProdutoId, e.Quantidade)).ToList();

                Mesa mesa = await _mesas.ObterDesocupadaPorNumero(model.NumeroMesa);

                if (mesa == null) return BadRequest(new { erro = "Mesa indisponivel" });

                long numeroPedido = await _pedidos.Criar(new Pedido(mesa, itens));

                return Created("", new { numeroPedido });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { erro = "Não foi possível criar o pedido."});
            }
        }

        [HttpPost, Route("{mesaId}/{produtoId}")]
        public async Task<IActionResult> Criar(int mesaId, int produtoId)
        {
            try
            {
                Mesa mesa = await _mesas.ObterDesocupadaPorId(mesaId);

                if (mesa == null) return BadRequest(new { erro = "Mesa indisponivel" });

                var numeroPedido = await _pedidos.Criar(mesaId, produtoId);

                return Created("", numeroPedido);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost, Route("AdicionarItem/{numeroPedido}/{produtoId}")]
        public async Task<IActionResult> AdicionarItem(long numeroPedido, int produtoId)
        {
            try
            {
                await _pedidos.AdicionarItem(numeroPedido, produtoId);

                return Ok();
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

        [HttpPut, Route("Pagar/{numeroPedido}")]
        public async Task<IActionResult> Pagar(long numeroPedido)
        {
            try
            {
                Pedido pedido = await _pedidos.Obter(numeroPedido);
                if (pedido.Pago)
                    return BadRequest(new { erro = "Pedido já está pago." });

                await _pedidos.Pagar(numeroPedido);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { erro = "Não foi possivel atualizar a quantidade do produto." });
            }
        }

        [HttpPut, Route("PagarComGorjeta/{numeroPedido}")]
        public async Task<IActionResult> PagarComGorjeta(long numeroPedido)
        {
            try
            {
                Pedido pedido = await _pedidos.Obter(numeroPedido);
                if (pedido.Pago)
                    return BadRequest(new { erro = "Pedido já está pago." });
                await _pedidos.PagarComGorjeta(numeroPedido);
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
