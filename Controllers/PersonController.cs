using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SkillSet.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult> GetPeople()
        {
            var people = await _mediator.Send(new GetPeopleQuery());

            return Ok(people);
        }
    }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
}