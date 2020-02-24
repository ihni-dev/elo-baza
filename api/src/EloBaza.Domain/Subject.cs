﻿using System;
using System.Collections.Generic;

namespace EloBaza.Domain
{
    public class Subject
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<string> Topics { get; private set; }
        public List<ExamSession> Exams { get; private set; }
        public List<Question> Questions { get; private set; }

        public Subject(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Subject name must be provided");

            Id = Guid.NewGuid();
            Name = name;
            Topics = new List<string>();
            Exams = new List<ExamSession>();
            Questions = new List<Question>();
        }
    }
}
