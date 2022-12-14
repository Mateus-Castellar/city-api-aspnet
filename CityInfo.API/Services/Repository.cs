using CityInfo.API.Data;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class Repository : IRepository
    {
        private readonly CityInfoContext _context;

        public Repository(CityInfoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Citys
                .OrderBy(lbda => lbda.Name)
                .ToListAsync();
        }

        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery,
            int pageNumber, int pageSize)
        {
            var collection = _context.Citys as IQueryable<City>;

            if (string.IsNullOrWhiteSpace(name) is false)
            {
                name = name.Trim();
                collection = collection.Where(lbda => lbda.Name == name);
            }

            if (string.IsNullOrWhiteSpace(searchQuery) is false)
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(lbda => lbda.Name.Contains(searchQuery) ||
                (lbda.Description != null && lbda.Description.Contains(searchQuery)));
            }

            var totalitemCount = await collection.CountAsync();

            var paginationMetadata = new
                PaginationMetadata(totalitemCount, pageSize, pageNumber);

            //a consulta so é enviada a base dados ao chamar "ToListAsync()"
            var collectionReturn = await collection
                .OrderBy(lbda => lbda.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionReturn, paginationMetadata);
        }

        public async Task<City?> GetCityAsync(int cityId)
        {
            return await _context.Citys.FirstOrDefaultAsync(lbda => lbda.Id == cityId);
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointId)
        {
            return await _context.PointsOfInterest
                .FirstOrDefaultAsync(lbda => lbda.CityId == cityId && lbda.Id == pointId);
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Citys.AnyAsync(lbda => lbda.Id == cityId);
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterest
                .Where(lbda => lbda.CityId == cityId).ToListAsync();
        }

        public async Task AddPointIntesertForCityAsync(int cityId, PointOfInterest point)
        {
            var city = await GetCityAsync(cityId);

            if (city is not null)
                city.PointsOfInterest.Add(point);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void DeletePointIntesert(PointOfInterest point)
        {
            _context.PointsOfInterest.Remove(point);
        }
    }
}