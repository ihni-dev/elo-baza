using System.Collections.Generic;
using System.Linq;

namespace EloBaza.Domain
{
    public class Category
    {
        public const int CategoryNameMaxLength = 50;

        public int Id { get; private set; }
        public string Name { get; private set; }
        public Category? ParentCategory { get; private set; }
        public ICollection<Category> SubCategories { get; private set; } = new List<Category>();
        public bool IsLeafCategory => !SubCategories.Any();
        public bool IsRootCategory => ParentCategory is null;

        public ICollection<Question> Questions { get; private set; } = new List<Question>();

        public Subject Subject { get; private set; }

        protected Category() { }

        public Category(Category parentCategory, string name)
        {
            ParentCategory = parentCategory;

            Name = name;
        }
    }
}