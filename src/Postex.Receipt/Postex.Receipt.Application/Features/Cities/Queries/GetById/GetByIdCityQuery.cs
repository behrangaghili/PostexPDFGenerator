using MediatR;
using Postex.receipt.Application.Dtos;

namespace Postex.receipt.Application.Features.Cities.Queries.GetById
{
    public class GetByIdCityQuery : IRequest<List<CityDto>>
    {
        public int Id { get; set; }
    }
}
