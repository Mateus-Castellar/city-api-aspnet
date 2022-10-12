using AutoMapper;
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
        private readonly IMapper _mapper;

        public CititiesController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CityWithoutPointsOfInterestDTO>>> GetCitites()
        {
            var cityEntities = await _repository.GetCitiesAsync();
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDTO>>(cityEntities));
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
