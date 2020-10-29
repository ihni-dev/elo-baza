using System;
using System.Collections.Generic;

namespace EloBaza.Application.Queries.SubjectAggregate.Category.Get
{
    /// <summary>
    /// Category representation model
    /// </summary>
    public class CategoryDetailsReadModel
    {
        /// <summary>
        /// Category key
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Parent category
        /// </summary>
        public CategoryDetailsReadModel? ParentCategory { get; set; }

        /// <summary>
        /// Child categories
        /// </summary>
        public ICollection<CategoryDetailsReadModel> ChildCategories { get; private set; } = new List<CategoryDetailsReadModel>();
    }
}
