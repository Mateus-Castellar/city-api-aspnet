using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 20;

        public CitiesController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CityWithoutPointsOfInterestDTO>>> GetCitites([FromQuery] string? name,
            string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {

            if (pageSize > maxCitiesPageSize)
                pageSize = maxCitiesPageSize;

            var (cityEntities, paginatioMetadata) = await _repository
                .GetCitiesAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginatioMetadata));

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
