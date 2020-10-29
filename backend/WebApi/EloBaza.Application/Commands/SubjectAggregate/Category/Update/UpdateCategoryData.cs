using EloBaza.Domain.SharedKernel.Exceptions;
using System;

namespace EloBaza.Application.Commands.SubjectAggregate.Category.Update
{
    public class UpdateCategoryData
    {
        public string Name { get; private set; }
        public Guid? ParentCateogryKey { get; private set; }

        public UpdateCategoryData(string name, Guid? parentCateogryKey)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(
                    () => string.IsNullOrEmpty(name),
                    nameof(name),
                    $"Category name must be provided");

                validationContext.Validate(
                    () => name.Length > Domain.SubjectAggregate.Category.NameMaxLength,
                    nameof(name),
                    $"Category name length must be less or equal to {Domain.SubjectAggregate.Category.NameMaxLength}");
            }

            Name = name;
            ParentCateogryKey = parentCateogryKey;
        }
    }
}
