using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CititiesDataStore
    {
        public List<CityDTO> Citities { get; set; }
        public static CititiesDataStore Current { get; set; } = new CititiesDataStore();

        public CititiesDataStore()
        {
            Citities = new List<CityDTO>()
            {
                new CityDTO()
                {
                    Id = 1,
                    Name ="Jaú",
                    Description="Descricao da cidade"
                },
                new CityDTO()
                {
                    Id = 2,
                    Name ="Itapui",
                    Description="Descricao da cidade"
                },
                new CityDTO()
                {
                    Id = 3,
                    Name ="Franca",
                    Description="Descricao da cidade"
                },
                new CityDTO()
                {
                    Id = 4,
                    Name ="Bariri",
                    Description="Descricao da cidade"
                },
            };
        }
    }
}
