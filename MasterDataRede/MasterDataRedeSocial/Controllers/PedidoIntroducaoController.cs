using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Core.Internal;
using DDDSample1.Domain.PedidoIntroducao;
using DDDSample1.Domain.PedidoLigacao;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDDSample1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class PedidoIntroducaoController: ControllerBase
    {
        private readonly PedidoIntroducaoService _service;
        private ILogger<PedidoIntroducao> _loggerPedidoIntroducao;
        public PedidoIntroducaoController(PedidoIntroducaoService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _loggerPedidoIntroducao = loggerFactory.CreateLogger<PedidoIntroducao>();

        }

        // GET: api/PedidoIntroducao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoIntroducaoDTO>>> GetAll()
        {
            _loggerPedidoIntroducao.LogInformation("Pedido GET inicializado.");
            return await _service.GetAllAsync();
        }
        
        // GET: api/PedidoIntroducao/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoIntroducaoDTO>> GetGetById(string id)
        {
            _loggerPedidoIntroducao.LogInformation("Pedido GET by ID inicializado.");
            var pedido= await _service.GetById(new PedidoIntroducaoId(id));

            if (pedido == null)
            {
                _loggerPedidoIntroducao.LogInformation("Não existe nenhum pedido com esse ID.");
                return NotFound();
            }
            
            return pedido;
        }

        // GET: api/PedidoIntroducao/pendentes/intermediario={userId}
        [HttpGet("pendentes/intermediario={userId}")]
        public async Task<List<PedidoIntroducaoDTO>> GetGetByEstadoPendente(string userId)
        {
            _loggerPedidoIntroducao.LogInformation("Pedido GET de pedidos de introducao do utilizador intermediario inicializado.");
            
            var listaPedidos= await _service.GetByEstadoPendente(userId);

            if (listaPedidos.IsNullOrEmpty())
            {
                _loggerPedidoIntroducao.LogInformation("A lista dos pedidos encontra-se vazia.");
                return null;
            }

            return listaPedidos;
        }
        
        // GET: api/PedidoIntroducao/intermediarios/x={userIdX}/z={userIdZ}
        [HttpGet("intermediarios/x={userIdX}/z={userIdZ}")]
        public async Task<List<UserDTO>> GetPossibleIntermediaryUsers(string userIdX, string userIdZ)
        {
            _loggerPedidoIntroducao.LogInformation("Pedido GET de utilizadores que podem ser intermediários");
            
            var listaIntermediarios= await _service.GetPossibleIntermediaryUsers(new UserId(userIdX), new UserId(userIdZ));

            if (listaIntermediarios.IsNullOrEmpty())
            {
                _loggerPedidoIntroducao.LogInformation("A lista dos possíveis intermediários encontra-se vazia.");
                return null;
            }

            return listaIntermediarios;
        }
        
        // POST: api/PedidoIntroducao
        [HttpPost]
        public async Task<ActionResult<PedidoIntroducaoDTO>> Create(PedidoIntroducaoPutDTO dto)
        {
            _loggerPedidoIntroducao.LogInformation("Pedido POST inicializado.");
            
            var pedido = await _service.AddAsync(dto);

            if (pedido == null)
            {
                _loggerPedidoIntroducao.LogInformation("Indicou argumentos no JSON para o pedido POST que são invalidos.");
                return BadRequest();
            }
            
            return CreatedAtAction(nameof(GetGetById), new { id = pedido.Id }, pedido);
        }
        
        // PUT: api/PedidoIntroducao/{id}}
        [HttpPut("{id}")]
        public async Task<ActionResult<PedidoIntroducaoDTO>> Update(Guid id, EstadoPedidoIntroducao estado)
        {
            _loggerPedidoIntroducao.LogInformation("Pedido PUT inicializado.");
            
            try
            {
                PedidoIntroducaoDTO novo =(PedidoIntroducaoDTO) await _service.UpdateAsync(id,estado);

                if (novo == null)
                {
                    _loggerPedidoIntroducao.LogInformation("Indicou argumentos no JSON para o pedido PUT que são invalidos.");
                    return NotFound();
                }
                
                return Ok(novo);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        
    }
}