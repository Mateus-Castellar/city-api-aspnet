using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/citities/{cityId}/points")]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPoints(int cityId)
        {
            var city = CititiesDataStore.Current.Citities
                .FirstOrDefault(city => city.Id == cityId);

            if (city is null) return NotFound();

            return Ok(city.PointsOfInterests);
        }

        [HttpGet("{pointId}", Name = "GetPointOfInterest")]
        public IActionResult GetPointInterest(int cityId, int pointId)
        {
            var city = CititiesDataStore.Current.Citities
               .FirstOrDefault(city => city.Id == cityId);

            if (city is null) return NotFound();

            var point = city.PointsOfInterests
                .FirstOrDefault(point => point.Id == pointId);

            if (point is null) return NotFound();

            return Ok(point);
        }

        [HttpPost]
        public IActionResult CreatePointInterest(int cityId, PointOfInterestForCreateDTO point)
        {
            if (ModelState.IsValid is false) return BadRequest();

            var city = CititiesDataStore.Current.Citities
                .FirstOrDefault(city => city.Id == cityId);

            if (city is null) return NotFound();

            var maxPointInterestId = CititiesDataStore.Current.Citities
                .SelectMany(city => city.PointsOfInterests).Max(lbda => lbda.Id);

            var finailPointPost = new PointOfInterestDTO
            {
                Id = ++maxPointInterestId,
                Name = point.Name,
                Description = point.Description,
            };

            city.PointsOfInterests.Add(finailPointPost);

            //retorna no head a url para realizar o get do ponto recem criado..
            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointId = finailPointPost.Id,
                }, finailPointPost);
        }

        [HttpPut("{pointOfInterestId}")]
        public IActionResult UpdatePointInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateTO point)
        {
            var city = CititiesDataStore.Current.Citities
               .FirstOrDefault(city => city.Id == cityId);

            if (city is null) return NotFound();

            var pointInterest = city.PointsOfInterests
                .FirstOrDefault(point => point.Id == pointOfInterestId);

            if (pointInterest is null) return NotFound();

            //Segundo padrao http o put "deve atualizar o objeto por inteiro" e caso o usuario
            //não informe um valor para um campo ele deve receber seu defaultValue

            pointInterest.Name = point.Name;
            pointInterest.Description = point.Description;

            return NoContent();
        }
    }
}