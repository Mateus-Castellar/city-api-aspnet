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

        [HttpGet("{pointId}")]
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
    }
}