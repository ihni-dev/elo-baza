using System;

namespace EloBaza.Application.Queries.Subject
{
    /// <summary>
    /// Subject representation
    /// </summary>
    public class SubjectReadModel
    {
        /// <summary>
        /// Subject id
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// Subject name
        /// </summary>
        public string Name { get; private set; }

        public SubjectReadModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}