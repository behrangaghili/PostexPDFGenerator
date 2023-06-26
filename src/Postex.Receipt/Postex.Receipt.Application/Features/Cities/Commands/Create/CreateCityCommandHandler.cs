using AutoMapper;
using MediatR;
using Postex.SharedKernel.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postex.receipt.Application.Dtos;
using Postex.receipt.Domain.Models;

namespace Postex.receipt.Application.Features.Cities.Commands.Create
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CityDto>
    {
        private readonly IWriteRepository<City> _writeRepository;
        private readonly IMapper _mapper;

        public CreateCityCommandHandler(IWriteRepository<City> contratcItemTypeWriteRepository, IMapper mapper)
        {
            _writeRepository = contratcItemTypeWriteRepository;
            _mapper = mapper;
        }

       

        async Task<CityDto> IRequestHandler<CreateCityCommand, CityDto>.Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var city = _mapper.Map<City>(request);
            await _writeRepository.AddAsync(city, cancellationToken);
            await _writeRepository.SaveChangeAsync(cancellationToken);
            var dto = _mapper.Map<CityDto>(city);
            return dto;
        }
    }
}
