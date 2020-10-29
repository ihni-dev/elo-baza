using System;
using System.Collections.Generic;

namespace EloBaza.Application.Queries.SubjectAggregate.Category.GetAll
{
    /// <summary>
    /// Category representation model
    /// </summary>
    public class CategoryReadModel
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
        /// Child categories
        /// </summary>
        public ICollection<CategoryReadModel> ChildCategories { get; private set; } = new List<CategoryReadModel>();
    }
}
