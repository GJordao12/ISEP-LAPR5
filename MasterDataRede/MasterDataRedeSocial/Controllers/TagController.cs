using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDDSample1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class TagController : ControllerBase
    {
        private readonly TagService _service;
        private ILogger<Tag> _loggerTag;

        public TagController(TagService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _loggerTag = loggerFactory.CreateLogger<Tag>();
        }

        // GET: api/Tag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetAllAsync()
        {
            _loggerTag.LogInformation("Pedido GET inicializado.");
            return await _service.GetAllAsync();
        }

        // GET: api/Tag/AllUsers
        [HttpGet("AllUsers")]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetTagsAllUsers()
        {
            _loggerTag.LogInformation("Pedido GET inicializado.");
            return await _service.GetTagsAllUsersAsync();
        }
        
        // GET: api/Tag/CloudAllUsers
        [HttpGet("CloudAllUsers")]
        public async Task<ActionResult<Dictionary<string,int>>> GetTagsCloudAllUsers()
        {
            _loggerTag.LogInformation("Pedido GET inicializado.");
            return await _service.GetTagsCloudAllUsersAsync();
        }
        
        // GET: api/Tag/id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetTagsByUser(string id)
        {
            _loggerTag.LogInformation("Pedido GET inicializado.");
            return await _service.GetTagsByUserAsync(id);
        }
        
        // GET: api/Tag/AllConnections
        [HttpGet("AllConnections")]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetTagsConnectionsAsync()
        {
            _loggerTag.LogInformation("Pedido GET inicializado.");
            return await _service.GetTagsConnectionsAsync();
        }
        
        // GET: api/Tag/CloudAllConnections
        [HttpGet("CloudAllConnections")]
        public async Task<ActionResult<Dictionary<string, int>>> GetTagsCloudConnectionsAsync()
        {
            _loggerTag.LogInformation("Pedido GET inicializado.");
            return await _service.GetTagsCloudConnectionsAsync();
        }
        
        // GET: api/Tag/Connections/{id}
        [HttpGet("Connections/{id}")]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetTagsConnectionsByUser(string id)
        {
            _loggerTag.LogInformation("Pedido GET inicializado.");
            return await _service.GetTagsConnectionsByUserAsync(id);
        }
        
        // GET: api/Tag/TagById/{id}
        [HttpGet("TagById/{id}")]
        public async Task<ActionResult<TagDTO>> GetTagsByID(string id)
        {
            return await _service.GetTagsByIdAsync(id);
        }

        // PUT: api/Tag/AddTags
        [HttpPut("AddTags/{nome}")]
        public async Task<ActionResult<TagDTO>> PutTags(string nome)
        {
            _loggerTag.LogInformation("Pedido PUT tags.");
            return TagDTOParser.ParaDTO(_service.AddTags(nome).Result);
        }
        

    }
}