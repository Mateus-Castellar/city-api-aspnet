using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/citities")]
    public class CititiesController : ControllerBase
    {
        private readonly IRepository _repository;

        public CititiesController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CityWithoutPointsOfInterestDTO>>> GetCitites()
        {
            var cityEntities = await _repository.GetCitiesAsync();
            var results = new List<CityWithoutPointsOfInterestDTO>();

            foreach (var item in cityEntities)
            {
                results.Add(new CityWithoutPointsOfInterestDTO
                {
                    Id = item.Id,
                    Description = item.Description,
                    Name = item.Name,
                });
            }

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            //var city = _cityStore.Citities
            //    .FirstOrDefault(cidade => cidade.Id == id);

            //if (city is null) return NotFound();

            //return Ok(city);

            return Ok();
        }
    }
}
