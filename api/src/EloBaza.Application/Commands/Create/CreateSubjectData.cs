﻿using EloBaza.Domain.SharedKernel;

namespace EloBaza.Application.Commands.Create
{
    public class CreateSubjectData
    {
        public string Name { get; private set; }

        public CreateSubjectData(string name)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrWhiteSpace(name), nameof(name), "Subject name must be provided");
            }

            Name = name;
        }
    }
}
