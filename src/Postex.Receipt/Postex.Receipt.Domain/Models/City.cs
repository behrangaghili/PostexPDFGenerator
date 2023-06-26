using Postex.SharedKernel.Domain;

namespace Postex.receipt.Domain.Models;

public class City : BaseEntity<int>
{
    public string CityName { get; set; }


}
