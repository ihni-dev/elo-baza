using System;

namespace EloBaza.Application.Queries.SubjectAggregate.GetAll
{
    /// <summary>
    /// Subject representation model
    /// </summary>
    public class SubjectReadModel
    {
        /// <summary>
        /// Subject's key
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// Subject's name
        /// </summary>
        public string? Name { get; set; }
    }
}