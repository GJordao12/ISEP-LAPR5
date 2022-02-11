using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.PedidoLigacao;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDDSample1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class PedidoLigacaoController : ControllerBase
    {
        private readonly PedidoLigacaoService _service;
        private ILogger<PedidoLigacao> _loggerPedidoLigacao;
        public PedidoLigacaoController(PedidoLigacaoService service,ILoggerFactory loggerFactory)
        {
            _service = service;
            _loggerPedidoLigacao = loggerFactory.CreateLogger<PedidoLigacao>();
        }

        // GET: api/PedidoLigacao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoLigacaoDTO>>> GetAll()
        {
            _loggerPedidoLigacao.LogInformation("Pedido GET inicializado");
            return await _service.GetAllAsync();
        }
        
        // GET: api/PedidoLigacao/pendentes/{userId}
        [HttpGet("pendentes/{userId}")]
        public async Task<List<PedidoLigacaoDTO>> GetGetByEstadoPendente(string userId)
        {
            _loggerPedidoLigacao.LogInformation("Pedido GET by ID dos pedidos de ligacao pendentes inicializado");
            var listaPedidos= await _service.GetByEstadoPendente(userId);

            return listaPedidos;
        }
        
        // PUT: api/PedidoLigacao/{idPedido}/{decisao}
        [HttpPut("{id}")]
        public async Task<ActionResult<PedidoLigacaoDTO>> Update(Guid id, Estado decisao)
        {
            _loggerPedidoLigacao.LogInformation("Pedido PUT inicializado");
            try
            {
                var pedido = await _service.UpdatePedidoLigacaoAsync(id,decisao);
                
                if (pedido == null)
                {
                    _loggerPedidoLigacao.LogInformation("Nao foi encontrado nenhum pedido com o dado id.");
                    return NotFound();
                }
                return Ok(pedido);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        // POST: api/PedidoLigacao
        [HttpPost]
        public async Task<ActionResult<PedidoLigacaoDTO>> Create(CreatingPedidoLigacaoDTO dto)
        {
            _loggerPedidoLigacao.LogInformation("Pedido POST inicializado");
            var req = await _service.AddAsync(dto);
            if (req == null)
            {
                _loggerPedidoLigacao.LogInformation("Os dados do JSON que introduziu não são os corretos.");
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetGetById), new { id = req.Id }, req);
        }
        
        // GET: api/PedidoLigacao/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoLigacaoDTO>> GetGetById(string id)
        {
            _loggerPedidoLigacao.LogInformation("Pedido GET by ID inicializado");
            var req= await _service.GetById(new PedidoLigacaoId(id));

            if (req == null)
            {
                return NotFound();
            }

            return req;
        }
    }
}