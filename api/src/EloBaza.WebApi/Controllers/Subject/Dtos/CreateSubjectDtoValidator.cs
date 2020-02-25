﻿using FluentValidation;

namespace EloBaza.WebApi.Controllers.Subject.Dtos
{
    class CreateSubjectDtoValidator : AbstractValidator<CreateSubjectDto>
    {
        public CreateSubjectDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Subject name must be provided");
        }
    }
}
