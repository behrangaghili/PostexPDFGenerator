using AutoMapper;
using Postex.receipt.Application.Dtos;
using Postex.receipt.Application.Features.Cities.Commands.Create;
using Postex.receipt.Domain.Models;

namespace Postex.receipt.Application.Mapping
{
    public class receiptMapping : Profile
    {
        public receiptMapping()
        {
            CreateMap<City, CityDto>();
            CreateMap<CreateCityCommand, City>();


        }
    }
}
