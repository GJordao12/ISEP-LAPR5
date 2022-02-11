using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Ligacoes;
using DDDSample1.Domain.RedesConexoes;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedesConexoesController : ControllerBase
    {
        private readonly RedeDeConexoesService _service;

        public RedesConexoesController(RedeDeConexoesService service)
        {
            _service = service;
        }

        //GET: api/RedesConexoes/userId/niveis=niveis
        // nivel = 0 -> significa que quer ver a rede de ligações com todos os níveis
        // nivel > 0 -> significa que quer ver a rede de ligações com n niveis
        // nivel < 0 -> bad request
        [HttpGet("{id}/niveis={niveis}")]
        public async Task<ActionResult<Dictionary<int, List<LigacaoRedeDeConexoesDTO>>>> GetByIdAndLevel(Guid id, int niveis)
        {
            try
            {
                if (niveis < 0)
                {
                    throw new BusinessRuleValidationException("[ERROR] Nível de Ligação Inválido. O Nível de Ligação deve ser maior ou igual 0");
                }

                return await _service.GetByIdAndLevelAsync(new UserId(id), niveis);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
    }
}