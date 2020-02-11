using FluentValidation;

namespace Skinshare.Web.Contracts.Requests
{
    public class RoutineRequestValidator : AbstractValidator<RoutineRequest>
    {
        public RoutineRequestValidator()
        {
            RuleFor(r => r.Steps).NotEmpty();
            RuleForEach(r => r.Steps).SetValidator(new StepRequestValidator());
        }
    }
}