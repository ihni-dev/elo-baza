﻿using EloBaza.Domain.SharedKernel;

namespace EloBaza.Domain.Subject
{
    public class Semester : Enumeration
    {
        public const int NameMaxLength = 6;

        public static readonly Semester Winter = new Semester(1, "Winter");
        public static readonly Semester Summer = new Semester(2, "Summer");

        public Semester(int id, string name) : base(id, name) { }
    }
}