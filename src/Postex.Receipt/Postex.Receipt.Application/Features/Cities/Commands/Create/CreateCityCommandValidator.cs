using FluentValidation;

namespace Postex.receipt.Application.Features.Cities.Commands.Create
{
    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator()
        {
            RuleFor(p => p.CityName)
                  .NotEmpty().WithMessage(" نام شهر الزامی میباشد");
           
        }
    }
}
