using System;
using System.Collections.Generic;

namespace EloBaza.Domain
{
    public class ExamSession
    {
        public Guid Id { get; private set; }
        public int Year { get; private set; }
        public Semester Semester { get; private set; }
        public List<Question> Questions { get; private set; }

        public ExamSession(int year, Semester semester)
        {
            if (year < 1950 || year > 2150)
                throw new ArgumentException($"Year {year} is invalid. Please provide proper year between 1950 and 2150.");

            Year = year;
            Semester = semester;
            Questions = new List<Question>();
        }
    }
}