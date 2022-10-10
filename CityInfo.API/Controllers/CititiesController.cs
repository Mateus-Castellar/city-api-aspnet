using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/citities")]
    public class CititiesController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetCitites()
        {
            return new JsonResult(CititiesDataStore.Current.Citities);
        }
    }
}
