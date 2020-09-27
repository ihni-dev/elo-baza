using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EloBaza.Domain.SubjectAggregate
{
    public class Category : Entity
    {
        public const int NameMaxLength = 50;

        public Subject? Subject { get; private set; }

        public string Name { get; private set; }
        public Category? ParentCategory { get; private set; }
        public ICollection<Category> SubCategories { get; private set; } = new List<Category>();
        public bool IsLeafCategory => !SubCategories.Any();
        public bool IsRootCategory => ParentCategory is null;

        protected Category() { }

        protected Category(Subject subject, Category? parentCategory, string name)
        {
            Key = Guid.NewGuid();
            Subject = subject;
            ParentCategory = parentCategory;
            Name = name;
        }

        internal static Category Create(int creatorId, Subject subject, string name, Category? parentCategory = null)
        {
            Validate(name);

            var category = new Category(subject, parentCategory, name);
            category.SetCreationData(creatorId);

            return category;
        }

        internal void Update(int userId, Category? newParentCategoryKey, string newName)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrWhiteSpace(newName), nameof(newName), "Category name must be provided");
            }

            Name = newName;
            ParentCategory = newParentCategoryKey;
            
            SetModificationData(userId);
        }

        internal void AddSubCategory(Category subCategory)
        {
            if (SubCategoryAlreadyExists(subCategory))
                throw new AlreadyExistsException($"Category with name: {subCategory.Name} already exists for parent category: {Key}");

            SubCategories.Add(subCategory);
        }

        internal Category? Seek(Guid categoryKey)
        {
            if (Key == categoryKey)
                return this;

            foreach (var category in SubCategories)
            {
                if (category.Key == categoryKey)
                    return category;
                else
                    return category.Seek(categoryKey);
            }

            return null;
        }

        internal bool SubCategoryAlreadyExists(Category subCategory)
        {
            return SubCategories.Any(sc => sc.Name.Equals(subCategory.Name, StringComparison.OrdinalIgnoreCase));
        }

        private static void Validate(string name)
        {
            using var validationContext = new ValidationContext();
            validationContext.Validate(() => string.IsNullOrWhiteSpace(name), nameof(name), "Category name must be provided");
            validationContext.Validate(() => name.Length <= NameMaxLength, nameof(name), "Category name maximum length (50) exceeded");
        }
    }
}