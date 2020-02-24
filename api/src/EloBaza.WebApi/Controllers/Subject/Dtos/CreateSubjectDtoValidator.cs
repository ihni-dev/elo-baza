using FluentValidation;

namespace EloBaza.WebApi.Controllers.Subject.Dtos
{
    public class CreateSubjectDtoValidator : AbstractValidator<CreateSubjectDto>
    {
        public CreateSubjectDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Subject name must be provided");
        }
    }
}
