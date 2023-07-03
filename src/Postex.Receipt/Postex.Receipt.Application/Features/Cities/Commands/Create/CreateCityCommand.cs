using Postex.receipt.Application.Contracts;
using Postex.receipt.Application.Dtos;

namespace Postex.receipt.Application.Features.Cities.Commands.Create
{
    public class CreateCityCommand : ITransactionRequest<CityDto>
    {
        public string CityName { get; set; }

    }
}
