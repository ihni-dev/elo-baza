using EloBaza.Domain.QuestionAggregate.Links;
using EloBaza.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EloBaza.Domain.QuestionAggregate
{
    public class Question : AggregateRoot
    {
        private int? SubjectId { get; set; }
        private ICollection<QuestionCategory> QuestionCategories { get; set; } = new List<QuestionCategory>();
        private ICollection<QuestionExamSession> QuestionExamSessions { get; set; } = new List<QuestionExamSession>();
        private ICollection<QuestionTest> QuestionTests { get; set; } = new List<QuestionTest>();

        public bool IsCategorized => QuestionCategories.Any();
        public bool IsExamQuestion => QuestionExamSessions.Any();
        public bool IsTestQuestion => QuestionTests.Any();

        public string Content { get; private set; }
        public ICollection<Attachment> Attachments { get; private set; } = new List<Attachment>();
        public ICollection<Answer> Answers { get; private set; } = new List<Answer>();
        public ICollection<Explanation> Explanations { get; private set; }
        public bool HasExplanation => Explanations.Any();
        public bool IsPublished { get; private set; }

        public Question(int? subjectId, string content, bool isPublished)
        {
            Key = Guid.NewGuid();

            SubjectId = subjectId;

            Content = content;
            IsPublished = isPublished;
        }
    }
}