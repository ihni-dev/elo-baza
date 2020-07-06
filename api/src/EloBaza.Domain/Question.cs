using System;
using System.Collections.Generic;

namespace EloBaza.Domain
{
    public class Question
    {
        public int Id { get; private set; }
        public string Content { get; private set; }
        public Attachment? Attachment { get; private set; }
        public ICollection<Answer> Answers { get; private set; } = new List<Answer>();
        public Explanation? Explanation { get; private set; }
        public bool HasExplanation => !(Explanation is null);
        public bool IsPublished { get; private set; }

        public Subject? Subject { get; private set; }
        public Category? Category { get; private set; }
        public ExamSession? ExamSession { get; private set; }
        public bool IsExamQuestion => !(ExamSession is null);

        protected Question() { }

        public Question(Subject subject, Category category, string content, bool isPublished)
        {
            Subject = subject;
            Category = category;

            Content = content;
            IsPublished = isPublished;
        }

        public Question(Subject subject, Category category, ExamSession examSession, string content, bool isPublished)
        {
            Subject = subject;
            Category = category;
            ExamSession = examSession;

            Content = content;
            IsPublished = isPublished;
        }
    }
}