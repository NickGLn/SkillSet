using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkillSet.Application;
using SkillSet.Application.Commands;
using SkillSet.Application.Models;
using SkillSet.Application.Queries;

namespace SkillSet.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class PersonController : ControllerBase
    {
        private readonly ISender _mediator;

        public PersonController(ISender mediator) => _mediator = mediator;

        [HttpGet]
        [Route("people")]
        public async Task<ActionResult> GetPeople()
        {
            var people = await _mediator.Send(new GetPeopleQuery());

            return Ok(people);
        }

        [HttpGet]
        [Route("person/{id}")]
        public async Task<ActionResult> GetPerson([FromRoute] long id)
        {
            var query = new GetPersonQuery(id);
            var person = await _mediator.Send(query);

            return Ok(person);
        }

        [HttpPost]
        [Route("person")]
        public async Task<ActionResult> CreatePerson([FromBody] CreatePersonCommand newPerson)
        {
            return Ok(await _mediator.Send(newPerson));
        }

        [HttpPut]
        [Route("person")]
        public async Task<ActionResult> UpdatePerson([FromBody] UpdatePersonCommand person)
        {
            return Ok(await _mediator.Send(person));
        }
    }
}