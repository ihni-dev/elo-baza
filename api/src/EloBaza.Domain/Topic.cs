﻿using System;
using System.Collections.Generic;

namespace EloBaza.Domain
{
    public class Topic
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<Question> Questions { get; private set; }

        public Topic(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Subject name must be provided");

            Id = Guid.NewGuid();
            Name = name;
            Questions = new List<Question>();
        }
    }
}