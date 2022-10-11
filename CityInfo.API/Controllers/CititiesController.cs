using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/citities")]
    public class CititiesController : ControllerBase
    {
        private readonly CititiesDataStore _cityStore;

        public CititiesController(CititiesDataStore cityStore)
        {
            _cityStore = cityStore;
        }

        [HttpGet]
        public IActionResult GetCitites()
        {
            return Ok(_cityStore.Citities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var city = _cityStore.Citities
                .FirstOrDefault(cidade => cidade.Id == id);

            if (city is null) return NotFound();

            return Ok(city);
        }
    }
}
