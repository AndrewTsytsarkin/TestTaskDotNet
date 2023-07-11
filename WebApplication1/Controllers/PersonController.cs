using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private AbstractPersonService _personServ;
        public PersonController(ILogger<PersonController> logger, AbstractPersonService personServ)
        {
            _personServ = personServ;
            _logger = logger;
        }

        [HttpGet(Name = "Persons")]
        public ActionResult<IEnumerable<PersonDto>> Get()
        {
            var result = _personServ.GetPersons().Select(p => new PersonDto
            {
                Id = p.Id,
                Position = p.Position,
                SecondName = p.SecondName,
                FirstName = p.FirstName
            })
            .ToArray();
            return Ok(result);

        }

        [HttpPost(Name = "CreatePerson")]
        public ActionResult<PersonDto> CreatePerson([FromBody] PersonCreateDto p)
        {
            PersonEntity personEntity;
            try
            {
                personEntity = _personServ.CreatePerson(p);
                PersonDto result = new()
                {
                    Id = personEntity.Id,
                    Position = personEntity.Position,
                    SecondName = personEntity.SecondName,
                    FirstName = personEntity.FirstName
                };
                return Ok(result);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "error creating person");
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }

        }

        [HttpPut(Name = "EditPerson")]
        public ActionResult<PersonDto> EditPerson([FromBody] PersonEditDto p)
        {
            PersonEntity personEntity;
            try
            {
                personEntity = _personServ.EditPerson(p);
                PersonDto result = new()
                {
                    Id = personEntity.Id,
                    Position = personEntity.Position,
                    SecondName = personEntity.SecondName,
                    FirstName = personEntity.FirstName
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "error edit person");
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }

        }

        [HttpDelete(Name = "DeletePerson")]
        public ActionResult<bool> DeletePerson([FromBody] PersonDeleteDto p)
        {
            try
            {
                return Ok(_personServ.DeletePerson(p));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "error delete person");
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}