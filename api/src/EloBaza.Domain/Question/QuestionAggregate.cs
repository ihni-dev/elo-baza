using EloBaza.Domain.SharedKernel;
using System;
using System.Collections.Generic;

namespace EloBaza.Domain.Question
{
    public class QuestionAggregate : AggregateRoot
    {
        private int? _subjectId;
        private int? _categoryId;
        private int? _examSessionId;
        public bool IsExamQuestion => !_examSessionId.HasValue;

        public string Content { get; private set; }
        public Attachment? Attachment { get; private set; }
        public ICollection<Answer> Answers { get; private set; } = new List<Answer>();
        public Explanation? Explanation { get; private set; }
        public bool HasExplanation => !(Explanation is null);
        public bool IsPublished { get; private set; }

        public QuestionAggregate(int? subjectId, int? categoryId, string content, bool isPublished)
        {
            Key = Guid.NewGuid();

            _subjectId = subjectId;
            _categoryId = categoryId;

            Content = content;
            IsPublished = isPublished;
        }

        public QuestionAggregate(int? subjectId, int? categoryId, int? examSessionId, string content, bool isPublished)
        {
            _subjectId = subjectId;
            _categoryId = categoryId;
            _examSessionId = examSessionId;

            Content = content;
            IsPublished = isPublished;
        }
    }
}