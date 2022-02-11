using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Pesquisa;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class PesquisaController : ControllerBase
        {
            private readonly PesquisaService _service;

            public PesquisaController(PesquisaService service)
            {
                _service = service;
            }
            
        [HttpGet("{id}/{nome}/{email}/{tag}")]
        public async Task<ActionResult<List<UserDTO>>> GetByIdAndSearchField(Guid id, string nome,string email,string tag)
        {
            try
            {
                return await _service.GetUsersWithSearchField(new UserId(id),nome,email,tag);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
    }
}