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

        /// <summary>
        /// Get list of all people and their skills
        /// </summary>
        /// <returns>Collection of people</returns>
        [HttpGet]
        [Route("people")]
        public async Task<ActionResult> GetPeople()
        {
            var people = await _mediator.Send(new GetPeopleQuery());

            return Ok(people);
        }

        /// <summary>
        /// Get person
        /// </summary>
        /// <param name="id"> Person's ID </param>
        /// <returns>Person's data with specified ID</returns>
        [HttpGet]
        [Route("person/{id}")]
        public async Task<ActionResult> GetPerson([FromRoute] long id)
        {
            var query = new GetPersonQuery(id);
            var person = await _mediator.Send(query);

            return Ok(person);
        }

        /// <summary>
        /// Create a new person
        /// </summary>
        /// <param name="newPerson">Payload with Person's data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("person")]
        public async Task<ActionResult> CreatePerson([FromBody] CreatePersonCommand newPerson)
        {
            return Ok(await _mediator.Send(newPerson));
        }

        /// <summary>
        /// Modify an existing person
        /// </summary>
        /// <param name="person">Payload with Person's data</param>
        /// <returns></returns>
        [HttpPut]
        [Route("person")]
        public async Task<ActionResult> UpdatePerson([FromBody] UpdatePersonCommand person)
        {
            return Ok(await _mediator.Send(person));
        }

        /// <summary>
        /// Delete person from system
        /// </summary>
        /// <param name="id"> Person's ID </param>
        /// <returns></returns>
        [HttpDelete]
        [Route("person/{id}")]
        public async Task<ActionResult> DeletePerson([FromRoute] long id)
        {
            var query = new DeletePersonCommand(id);

            return Ok(await _mediator.Send(query));
        }
    }
}