using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/citities/{cityId}/points")]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly ILocalMailService _mailService;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            ILocalMailService mailService, IRepository repository, IMapper mapper)
        {
            _logger = logger;
            _mailService = mailService;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDTO>>> GetPoints(int cityId)
        {
            if (await _repository.CityExistsAsync(cityId) is false)
            {
                _logger.LogInformation($"City with id {cityId} wasn't when acessing points of interest");
                return NotFound();
            }

            var points = await _repository.GetPointsOfInterestForCityAsync(cityId);
            return Ok(_mapper.Map<IEnumerable<PointOfInterestDTO>>(points));
        }

        [HttpGet("{pointId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDTO>> GetPointInterest(int cityId, int pointId)
        {
            if (await _repository.CityExistsAsync(cityId) is false)
            {
                _logger.LogInformation($"City with id {cityId} wasn't when acessing points of interest");
                return NotFound();
            }

            var point = await _repository.GetPointOfInterestForCityAsync(cityId, pointId);

            if (point is null) return NotFound();

            return Ok(_mapper.Map<PointOfInterestDTO>(point));
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDTO>> CreatePointInterest(int cityId,
            PointOfInterestForCreateDTO point)
        {
            if (ModelState.IsValid is false) return BadRequest();

            if (await _repository.CityExistsAsync(cityId) is false)
                return NotFound();

            var finalPoint = _mapper.Map<PointOfInterest>(point);

            await _repository.AddPointIntesertForCityAsync(cityId, finalPoint);

            await _repository.SaveChangesAsync();

            var createdPointOfInterestToReturn = _mapper
                .Map<PointOfInterestDTO>(finalPoint);

            //retorna no head a url para realizar o get do ponto recem criado..
            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointId = createdPointOfInterestToReturn.Id,
                }, createdPointOfInterestToReturn);
        }

        [HttpPut("{pointOfInterestId}")]
        public async Task<IActionResult> UpdatePointInterest(int cityId, int pointOfInterestId,
            PointOfInterestForUpdateDTO point)
        {
            if (await _repository.CityExistsAsync(cityId) is false)
                return NotFound();

            var pointInterest = await _repository
                .GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointInterest is null) return NotFound();

            //automapper ira substiuir o valor do 2 parametro para o do 1 parametro
            _mapper.Map(point, pointInterest);

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public async Task<IActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            if (await _repository.CityExistsAsync(cityId) is false)
                return NotFound();

            var pointInterest = await _repository
               .GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointInterest is null) return NotFound();

            _repository.DeletePointIntesert(pointInterest);

            await _repository.SaveChangesAsync();

            _mailService.Send("Point deleted", $"point delete in cityId [{cityId}]");

            return NoContent();
        }
    }
}