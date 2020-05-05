using FluentValidation;

namespace Skinshare.Web.Contracts.Requests
{
    public class StepRequestValidator : AbstractValidator<StepRequest>
    {
        public StepRequestValidator()
        {
            RuleFor(s => s.Description).NotEmpty();
            RuleFor(s => s.Order).GreaterThanOrEqualTo(0);
            RuleFor(s => s.PartOfDay).IsInEnum();
        }
    }
}