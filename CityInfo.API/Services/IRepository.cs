using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface IRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchQuery);
        Task<City?> GetCityAsync(int cityId);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointId);
        Task<bool> CityExistsAsync(int cityId);
        Task AddPointIntesertForCityAsync(int cityId, PointOfInterest point);
        void DeletePointIntesert(PointOfInterest point);
        Task<bool> SaveChangesAsync();
    }
}