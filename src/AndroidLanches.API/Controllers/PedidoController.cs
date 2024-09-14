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
        private readonly IPedidosRepository _pedidosRepository;
        private readonly IMesasRepository _mesasRepository;

        public PedidoController(IPedidosRepository pedidos, 
                                IMesasRepository mesas, 
                                ILogger<PedidoController> logger)
        {
            _logger = logger;
            _pedidosRepository = pedidos;
            _mesasRepository = mesas;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _pedidosRepository.ObterTodos());

        [HttpGet, Route("ObterPorNumero/{numeroPedido}")]
        public async Task<IActionResult> Get(long numeroPedido)
        {
            Pedido pedido = await _pedidosRepository.Obter(numeroPedido);
            if (pedido == null) return NotFound();

            return Ok(pedido);            
        }

        [HttpGet,Route("ObterTodosSemPagamentoEfetuado")]
        public async Task<IActionResult> ObterTodosSemPagamentoEfetuado() => Ok(await _pedidosRepository.ObterTodosSemPagamentoEfetuado());

        [HttpPost, Route("")]
        public async Task<IActionResult> Criar(PedidoInputModel model)
        {   
            var itens = model.Item.Select(e => new PedidoItem(e.ProdutoId, e.Quantidade)).ToList();

            Mesa mesa = await _mesasRepository.ObterDesocupadaPorNumero(model.NumeroMesa);

            if (mesa == null) return BadRequest(new { erro = "Mesa indisponivel" });

            long numeroPedido = await _pedidosRepository.Criar(new Pedido(mesa, itens));

            return Created("", new { numeroPedido });
        }

        [HttpPost, Route("{mesaId}/{produtoId}")]
        public async Task<IActionResult> Criar(int mesaId, int produtoId)
        {
            try
            {
                Mesa mesa = await _mesasRepository.ObterDesocupadaPorId(mesaId);

                if (mesa == null) return BadRequest(new { erro = "Mesa indisponivel" });

                var numeroPedido = await _pedidosRepository.Criar(mesaId, produtoId);

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
                await _pedidosRepository.AdicionarItem(numeroPedido, produtoId);

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
            await _pedidosRepository.IncrementarQuantidadeProduto(pedidoItemId);
            return Ok();
        }

        [HttpPut, Route("DecrementarQuantidadeProduto/{pedidoItemId}")]
        public async Task<IActionResult> DecrementarQuantidadeProduto(int pedidoItemId)
        {
            await _pedidosRepository.DecrementarQuantidadeProduto(pedidoItemId);
            return Ok();
        }

        [HttpPut, Route("Pagar/{numeroPedido}")]
        public async Task<IActionResult> Pagar(long numeroPedido)
        {
            Pedido pedido = await _pedidosRepository.Obter(numeroPedido);
            if (pedido.Pago)
                return BadRequest(new { erro = "Pedido já está pago." });

            await _pedidosRepository.Pagar(numeroPedido);
            return Ok();
        }

        [HttpPut, Route("PagarComGorjeta/{numeroPedido}")]
        public async Task<IActionResult> PagarComGorjeta(long numeroPedido)
        {
            try
            {
                Pedido pedido = await _pedidosRepository.Obter(numeroPedido);
                if (pedido.Pago)
                    return BadRequest(new { erro = "Pedido já está pago." });
                await _pedidosRepository.PagarComGorjeta(numeroPedido);
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
