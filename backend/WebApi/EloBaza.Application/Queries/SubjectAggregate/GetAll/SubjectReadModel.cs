using System;

namespace EloBaza.Application.Queries.SubjectAggregate.GetAll
{
    /// <summary>
    /// Subject representation model
    /// </summary>
    public class SubjectReadModel
    {
        /// <summary>
        /// Subject key
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// Subject name
        /// </summary>
        public string? Name { get; set; }
    }
}