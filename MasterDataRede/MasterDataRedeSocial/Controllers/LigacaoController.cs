
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Ligacoes;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDDSample1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class LigacaoController : ControllerBase
    {
        private readonly LigacaoService _service;
        private ILogger<Ligacao> _loggerLigacao;

        public LigacaoController(LigacaoService service,ILoggerFactory loggerFactory)
        {
            _service = service;
            _loggerLigacao = loggerFactory.CreateLogger<Ligacao>();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LigacaoDTO>>> GetAll()
        {
            _loggerLigacao.LogInformation("Pedido GET inicializado.");
            return await _service.GetAllAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<LigacaoDTO>> GetGetById(string id)
        {
            _loggerLigacao.LogInformation("Pedido GET by ID inicializado.");
            var req= await _service.GetById(new LigacaoID(id));

            if (req == null)
            {
                _loggerLigacao.LogInformation("Nenhuma ligação foi encontrada.");
                return NotFound();
            }

            return req;
        }
        
        [HttpGet("user={id}")]
        public async Task<ActionResult<List<LigacaoDTO>>> GetLigacoesByUser(string id)
        {
            _loggerLigacao.LogInformation("Pedido GET by User ID inicializado.");
            var req= await _service.GetLigacoesByUser(new UserId(id));

            if (req == null)
            {
                _loggerLigacao.LogInformation("Nenhuma ligação foi encontrada.");
                return NotFound();
            }

            return req;
        }

        [HttpGet("Ligacoes/Prolog")]
        public async Task<ActionResult<List<LigacaoDTOProlog>>> GetLigacoesProlog()
        {
            _loggerLigacao.LogInformation("Pedido GET para a base de conhecimento PROLOG.");
            var req = await _service.GetAllProlog();

            if (req == null)
            {
                _loggerLigacao.LogInformation("Não existe nenhuma ligação.");
                return NotFound();
            }
            return req;
        }
        
        [HttpGet("LeaderBoard/Fortaleza/{id}")]
        public async Task<ActionResult<List<LeaderBoardDTO>>> GetLeaderBoardFortaleza(string id)
        {
            _loggerLigacao.LogInformation("Pedido GET by User ID inicializado.");
            var req = await _service.GetLeaderBoardFortaleza(id);

            if (req == null)
            {
                _loggerLigacao.LogInformation("Nenhuma ligação foi encontrada.");
                return NotFound();
            }

            return req;
        }
        
        [HttpGet("LeaderBoard/Dimensao/{id}")]
        public async Task<ActionResult<List<LeaderBoardDTO>>> GetLeaderBoardDimensao(string id)
        {
            _loggerLigacao.LogInformation("Pedido GET by User ID inicializado.");
            var req = await _service.GetLeaderBoardDimensao(id);

            if (req == null)
            {
                _loggerLigacao.LogInformation("Nenhuma ligação foi encontrada.");
                return NotFound();
            }

            return req;
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<LigacaoDTO>> Update(Guid id,LigacaoPutDTO dto)
        {
            _loggerLigacao.LogInformation("Pedido PUT inicializado.");
            LigacaoDTO ligacao = await _service.UpdateAsync(id,dto);
            
            return Ok(ligacao);
        }
        [HttpPut("ForcaRelacao/{idRemetente}/{idDestinatario}/{status}")]
        public async Task<ActionResult<LigacaoDTO>> UpdateForcaRelacao(Guid idRemetente,Guid idDestinatario,string status)
        {
            _loggerLigacao.LogInformation("Pedido PUT inicializado.");
            LigacaoDTO ligacao = await _service.UpdateForcaRelacaoAsync(idRemetente,idDestinatario,status);
            
            return Ok(ligacao);
        }
    }

}