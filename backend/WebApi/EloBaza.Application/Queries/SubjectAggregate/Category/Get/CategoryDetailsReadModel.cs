using System;

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
    }
}
