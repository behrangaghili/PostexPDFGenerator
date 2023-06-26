using MediatR;
using Microsoft.EntityFrameworkCore;
using Postex.receipt.Application.Dtos;
using Postex.receipt.Domain.Models;
using Postex.SharedKernel.Interfaces;

namespace Postex.receipt.Application.Features.Cities.Queries.GetById
{
    public class GetByCustomerContractBoxPriceQueryHandler : IRequestHandler<GetByIdCityQuery, List<CityDto>>
    {
        private readonly IReadRepository<City> _readRepository;

        public GetByCustomerContractBoxPriceQueryHandler(IReadRepository<City> readRepository)
        {
            _readRepository = readRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<CityDto>> Handle(GetByIdCityQuery request, CancellationToken cancellationToken)
        {
            var cityDto = await _readRepository.Table
                .Select(c => new CityDto
                {
                    Id = c.Id,
                    CityName = c.CityName

                })
                .Where(c => c.Id == request.Id)
                .ToListAsync(cancellationToken);
            return cityDto;
        }
    }
}
