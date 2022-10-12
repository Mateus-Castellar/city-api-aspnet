using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.API.Profiles
{
    public class PointsOfInterest : Profile
    {
        public PointsOfInterest()
        {
            CreateMap<PointOfInterest, PointOfInterestDTO>();
            CreateMap<PointOfInterestForCreateDTO, PointOfInterest>();
            CreateMap<PointOfInterestForUpdateDTO, PointOfInterest>();
        }
    }
}
