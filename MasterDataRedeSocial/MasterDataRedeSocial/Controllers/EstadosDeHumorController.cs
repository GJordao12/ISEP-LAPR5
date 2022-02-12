using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.EstadosDeHumor;
using DDDSample1.Domain.Utilizador;
using Microsoft.Extensions.Logging;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosDeHumorController : ControllerBase
    {
        private readonly EstadoDeHumorService _service;
        private ILogger<EstadoDeHumor> _loggerEstadosHumor;

        public EstadosDeHumorController(EstadoDeHumorService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _loggerEstadosHumor = loggerFactory.CreateLogger<EstadoDeHumor>();
        }

        // GET: api/EstadosDeHumor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoDeHumorDto>>> GetAll()
        {
            _loggerEstadosHumor.LogInformation("Pedido GET inicializado.");
            return await _service.GetAllAsync();
        }

        // GET: api/EstadosDeHumor/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoDeHumorDto>> GetGetById(string id)
        {
            _loggerEstadosHumor.LogInformation("Pedido GET by ID inicializado.");
            var estado = await _service.GetById(new EstadoDeHumorId(id));

            if (estado == null)
            {
                _loggerEstadosHumor.LogInformation("Nenhum Estado De Humor foi encontrado com o id dado.");
                return NotFound();
            }

            return estado;
        }
        
        // GET: api/EstadosDeHumor/idUser={idUser}
        [HttpGet("estadoHumorUser/{idUser}")]
        public async Task<ActionResult<EstadoDeHumorDto>> GetEstadoHumorByUserId(string idUser)
        {
            _loggerEstadosHumor.LogInformation("Pedido GetEstadoHumorByUserId inicializado.");
            var estado = await _service.GetEstadoHumorByUserId(new UserId(idUser));

            if (estado == null)
            {
                _loggerEstadosHumor.LogInformation("Nenhum Estado De Humor foi encontrado para o user Name fornecido.");
                return NotFound();
            }

            return estado;
        }
    }
}