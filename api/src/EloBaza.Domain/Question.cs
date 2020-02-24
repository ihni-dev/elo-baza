using System;
using System.Collections.Generic;

namespace EloBaza.Domain
{
    public class Question
    {
        public Guid Id { get; private set; }
        public string Header { get; private set; }
        public string Answer { get; private set; }
        public List<string> Topics { get; private set; }
        public List<ExamSession> ExamSessions { get; private set; }
        public List<string> Annotations { get; private set; }

        public Question(string header, string answer, List<string> topics, List<ExamSession> examSessions, List<string> annotations)
        {
            if (string.IsNullOrWhiteSpace(header))
                throw new ArgumentException("Question name must have a value");
            if (string.IsNullOrWhiteSpace(answer))
                throw new ArgumentException("Question answer must have a value");

            Id = Guid.NewGuid();
            Header = header;
            Answer = answer;
            Topics = topics ?? throw new ArgumentNullException("Question topics must be provided");
            ExamSessions = examSessions ?? throw new ArgumentNullException("Question exams must be provided");
            Annotations = annotations ?? throw new ArgumentNullException("Question annotations must be provided");
        }
    }
}