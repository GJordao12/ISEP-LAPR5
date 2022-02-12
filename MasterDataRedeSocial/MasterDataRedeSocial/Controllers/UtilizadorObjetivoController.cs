using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.UtilizadorObjetivo;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizadorObjetivoController : ControllerBase
    {
        private readonly UtilizadorObjetivoService _service;

        public UtilizadorObjetivoController(UtilizadorObjetivoService service)
        {
            _service = service;
        }
        
        
        [HttpGet]
        public async Task<ActionResult<List<PerfilPrologDTO>>> GetByIdAndSearchField()
        {
            try
            {
                return await _service.GetUtilizadoresObjetivo();
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        
        
        
    }
}