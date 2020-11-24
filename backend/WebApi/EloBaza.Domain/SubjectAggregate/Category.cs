using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EloBaza.Domain.SubjectAggregate
{
    public class Category : Entity
    {
        public const int MaxLevel = 5;
        public const int NameMaxLength = 50;

        public Subject? Subject { get; private set; }

        public string Name { get; private set; }
        public Category? ParentCategory { get; private set; }
        public ICollection<Category> SubCategories { get; private set; } = new List<Category>();
        public int Level { get; private set; }

        public bool IsLeafCategory => Level == MaxLevel;
        public bool IsRootCategory => ParentCategory is null;

        protected Category() { }

        protected Category(Subject subject, string name)
        {
            Key = Guid.NewGuid();
            Subject = subject;
            Name = name;
            Level = 0;
        }

        protected Category(Category parentCategory, string name)
        {
            Key = Guid.NewGuid();
            ParentCategory = parentCategory;
            Name = name;
            Level = parentCategory.Level + 1;
        }

        internal static Category CreateRoot(int creatorId, Subject subject, string name)
        {
            Validate(name);

            var category = new Category(subject, name);
            category.SetCreationData(creatorId);

            return category;
        }

        internal Category CreateSubCategory(int creatorId, string name)
        {
            Validate(name);

            if (IsLeafCategory)
                throw new InvalidOperationException("Category is already a leaf category, can not add another level");

            var category = new Category(this, name);
            category.SetCreationData(creatorId);

            SubCategories.Add(category);

            return category;
        }

        internal void Update(int userId, Category? newParentCategory, string newName)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrWhiteSpace(newName), nameof(newName), "Category name must be provided");
            }

            if (newParentCategory is not null && !CanAssignNewParent(newParentCategory))
                throw new InvalidOperationException("Can not change parent as it would exceed maximum depth of a category tree");

            Name = newName;
            ParentCategory = newParentCategory;

            RecalculateLevels();

            SetModificationData(userId);
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
            var foundSubCategory = SubCategories.FirstOrDefault(sc => sc.Name.Equals(subCategory.Name, StringComparison.OrdinalIgnoreCase));
            if (foundSubCategory is null)
                return false;

            return subCategory != foundSubCategory;
        }

        internal void RecalculateLevels()
        {
            if (ParentCategory is null)
                Level = 0;
            else
                Level = ParentCategory.Level + 1;

            foreach (var subCategory in SubCategories)
            {
                subCategory.RecalculateLevels();
            }
        }

        internal int HeightDown()
        {
            return LowestLevelDown() - Level;
        }

        private int LowestLevelDown()
        {
            if (!SubCategories.Any())
                return Level;

            return SubCategories.Max(c => c.LowestLevelDown());
        }

        private bool CanAssignNewParent(Category newParent)
        {
            if (newParent.IsLeafCategory)
                return false;
             
            return HeightDown() + newParent.Level < MaxLevel;
        }

        private static void Validate(string name)
        {
            using var validationContext = new ValidationContext();
            validationContext.Validate(() => string.IsNullOrWhiteSpace(name), nameof(name), "Category name must be provided");
            validationContext.Validate(() => name.Length > NameMaxLength, nameof(name), $"Category name maximum length ({NameMaxLength}) exceeded");
        }
    }
}