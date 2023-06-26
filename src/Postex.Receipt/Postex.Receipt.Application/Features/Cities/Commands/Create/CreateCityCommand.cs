using Postex.receipt.Application.Contracts;
using Postex.receipt.Application.Dtos;
using Postex.receipt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postex.receipt.Application.Features.Cities.Commands.Create
{
    public class CreateCityCommand : ITransactionRequest<CityDto>
    {
        public string CityName { get; set; }
      
    }
}
