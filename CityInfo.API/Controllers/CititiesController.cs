using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/citities")]
    public class CititiesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCitites()
        {
            return Ok(CititiesDataStore.Current.Citities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var city = CititiesDataStore.Current.Citities
                .FirstOrDefault(cidade => cidade.Id == id);

            if (city is null) return NotFound();

            return Ok(city);
        }
    }
}
