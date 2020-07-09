using EloBaza.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EloBaza.Domain.Subject
{
    public class Category : Entity
    {
        public const int CategoryNameMaxLength = 50;

        public string Name { get; private set; }
        public Category? ParentCategory { get; private set; }
        public ICollection<Category> SubCategories { get; private set; } = new List<Category>();
        public bool IsLeafCategory => !SubCategories.Any();
        public bool IsRootCategory => ParentCategory is null;

        public SubjectAggregate? Subject { get; private set; }

        protected Category() { }

        internal Category(Category parentCategory, string name)
        {
            Key = Guid.NewGuid();

            ParentCategory = parentCategory;

            Name = name;
        }
    }
}