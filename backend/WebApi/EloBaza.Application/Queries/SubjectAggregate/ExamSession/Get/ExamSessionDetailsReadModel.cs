﻿using System;

namespace EloBaza.Application.Queries.SubjectAggregate.ExamSession.Get
{
    /// <summary>
    /// Exam session details representation model
    /// </summary>
    public class ExamSessionDetailsReadModel
    {
        /// <summary>
        /// Exam session key
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// Exam session name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Exam session year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Exam session semester
        /// </summary>
        public string? Semester { get; set; }

        /// <summary>
        /// Exam session resit number
        /// </summary>
        public byte? ResitNumber { get; set; }
    }
}
