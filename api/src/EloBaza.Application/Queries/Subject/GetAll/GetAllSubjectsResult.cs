using System;
using System.Collections.Generic;
using System.Text;

namespace EloBaza.Application.Queries.Subject.GetAll
{
    public class GetAllSubjectsResult
    {
        IEnumerable<SubjectReadModel> Subjects { get; set; }

        public GetAllSubjectsResult(IEnumerable<SubjectReadModel> subjects)
        {
            Subjects = subjects;
        }
    }
}
