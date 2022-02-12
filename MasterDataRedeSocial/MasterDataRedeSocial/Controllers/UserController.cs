using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private ILogger<User> _loggerUser;

        public UserController(UserService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _loggerUser = loggerFactory.CreateLogger<User>();
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            _loggerUser.LogInformation("Pedido GET inicializado.");
            return await _service.GetAllAsync();
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetGetById(string id)
        {
            _loggerUser.LogInformation("Pedido GET by ID inicializado.");
            var user = await _service.GetById(new UserId(id));

            if (user == null)
            {
                _loggerUser.LogInformation("Nenhum User foi encontrado com o id dado.");
                return NotFound();
            }

            return user;
        }


        // GET: api/User/{name}
        [HttpGet("name/{name}")]
        public async Task<ActionResult<UserDTO>> GetByName(string name)
        {
            _loggerUser.LogInformation("Pedido GET by ID inicializado.");
            var user = await _service.GetByName(name);

            if (user == null)
            {
                _loggerUser.LogInformation("Nenhum User foi encontrado com o id dado.");
                return NotFound();
            }

            return user;
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Create(CreatingUserDTO dto)
        {
            _loggerUser.LogInformation("Pedido POST inicializado.");
            try
            {
                var user = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetGetById), new {id = user.Id}, user);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        //Delete User
        [HttpDelete("userId={id}")]
        public async Task<ActionResult<UserDTO>> HardDelete(string id)
        {
            try
            {
                var user = await _service.DeleteAsync(new UserId(id));

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
    }
}