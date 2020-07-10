using EloBaza.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EloBaza.Domain.QuestionAggregate
{
    public class Question : AggregateRoot
    {
        private int? SubjectId { get; set; }
        private int? CategoryId { get; set; }
        private int? ExamSessionId { get; set; }
        public bool IsExamQuestion => !ExamSessionId.HasValue;

        public string Content { get; private set; }
        public ICollection<Attachment> Attachments { get; private set; }
        public ICollection<Answer> Answers { get; private set; } = new List<Answer>();
        public ICollection<Explanation> Explanations { get; private set; }
        public bool HasExplanation => Explanations.Any();
        public bool IsPublished { get; private set; }

        public Question(int? subjectId, int? categoryId, string content, bool isPublished)
        {
            Key = Guid.NewGuid();

            SubjectId = subjectId;
            CategoryId = categoryId;

            Content = content;
            IsPublished = isPublished;
        }

        public Question(int? subjectId, int? categoryId, int? examSessionId, string content, bool isPublished)
        {
            SubjectId = subjectId;
            CategoryId = categoryId;
            ExamSessionId = examSessionId;

            Content = content;
            IsPublished = isPublished;
        }
    }
}