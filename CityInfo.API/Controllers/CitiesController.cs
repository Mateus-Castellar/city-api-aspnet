using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public CitiesController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CityWithoutPointsOfInterestDTO>>> GetCitites([FromQuery] string? name,
            string? searchQuery)
        {
            var cityEntities = await _repository.GetCitiesAsync(name, searchQuery);
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDTO>>(cityEntities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDTO>> GetCity(int id)
        {
            var city = await _repository.GetCityAsync(id);

            if (city is null) return NotFound();

            return Ok(_mapper.Map<CityWithoutPointsOfInterestDTO>(city));
        }
    }
}
