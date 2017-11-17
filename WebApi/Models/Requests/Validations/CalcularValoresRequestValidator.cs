using FluentValidation;

namespace WebApi.Models.Requests.Validations
{
    public class CalcularValoresRequestValidator : AbstractValidator<CalcularValoresRequest>
    {
        public CalcularValoresRequestValidator()
        {
            RuleFor(x => x.Montante).GreaterThan(0).WithMessage("O Montante precisa ser maior que zero");
            RuleFor(x => x.ValorCompra).GreaterThan(0).WithMessage("O ValorCompra precisa ser maior que zero");
            RuleFor(x => x.ValorVenda).GreaterThan(0).WithMessage("O ValorVenda precisa ser maior que zero");
        }
    }
}