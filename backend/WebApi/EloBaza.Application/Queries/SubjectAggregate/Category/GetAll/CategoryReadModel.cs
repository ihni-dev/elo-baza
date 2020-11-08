using System;

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
        /// Parent category key
        /// </summary>
        public Guid? ParentKey { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string? Name { get; set; }
    }
}
