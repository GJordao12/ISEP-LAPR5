using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.EstadosDeHumor;
using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly PerfilService _service;
        private ILogger<Perfil> _loggerPerfil;

        public PerfilController(PerfilService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _loggerPerfil = loggerFactory.CreateLogger<Perfil>();
        }

        // GET: api/Perfil
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfilDto>>> GetAll()
        {
            _loggerPerfil.LogInformation("Pedido GET inicializado.");
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PerfilDto>> GetGetById(string id)
        {
            _loggerPerfil.LogInformation("Pedido GET by ID inicializado.");
            var perfil = await _service.GetById(new PerfilId(id));

            if (perfil == null)
            {
                _loggerPerfil.LogInformation("Não foi encontrado nenhum perfil.");
                return NotFound();
            }

            return perfil;
        }

        // GET : perfil/userId=userId
        [HttpGet("userId={id}")]
        public async Task<ActionResult<PerfilDto>> GetGetByUserId(string id)
        {
            _loggerPerfil.LogInformation("Pedido GET by User ID Inicializado.");

            try
            {
                var perfil = await _service.GetByUserId(new UserId(id));
                return perfil;
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // PUT: api/Perfil/perfilId/editarPerfil
        [HttpPut("{id}/EditarPerfil")]
        public async Task<ActionResult<PerfilDto>> UpdatePerfil(Guid id, CreatingPerfilPutDto perfilPutDto)
        {
            _loggerPerfil.LogInformation("Pedido PUT incializado.");

            try
            {
                _loggerPerfil.LogInformation("Não foi encontrado nenhum perfil com esse ID.");
                var perfil = await _service.UpdateAsync(id, perfilPutDto);

                return Ok(perfil);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // PUT: api/Perfil/perfilId/EditarEstadoDeHumor
        [HttpPut("{id}/EditarEstadoDeHumor")]
        public async Task<ActionResult<PerfilDto>> Update(Guid id, EstadoDeHumorDto estadoDeHumorDto)
        {
            _loggerPerfil.LogInformation("[INFO] Pedido PUT Editar Estado De Humor Inicializado.");

            try
            {
                var perfil = await _service.UpdateEstadoDeHumorAsync(id, estadoDeHumorDto);
                return Ok(perfil);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        
        // PUT: api/Perfil/userId=userId/EditarEstadoDeHumor
        [HttpPut("userId={id}/EditarEstadoDeHumor")]
        public async Task<ActionResult<PerfilDto>> Update2(Guid id, EstadoDeHumorDto estadoDeHumorDto)
        {
            _loggerPerfil.LogInformation("[INFO] Pedido PUT Editar Estado De Humor Inicializado.");

            try
            {
                var perfil = await _service.UpdateEstadoDeHumorAsync2(new UserId(id), estadoDeHumorDto);
                return Ok(perfil);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
    }
}