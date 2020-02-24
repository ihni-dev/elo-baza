using EloBaza.Domain.SharedKernel;
using System;

namespace EloBaza.Domain
{
    public class ExamSession : ValueObject
    {
        public DateTime Year { get; private set; }
        public Semester Semester { get; private set; }
    }
}