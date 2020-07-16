using EloBaza.Domain.SharedKernel;

namespace EloBaza.Domain.SubjectAggregate
{
    public class Month : Enumeration
    {
        public const int NameMaxLength = 11;

        public static readonly Month Unspecified = new Month(0, "Unspecified");
        public static readonly Month January = new Month(1, "January");
        public static readonly Month February = new Month(2, "February");
        public static readonly Month March = new Month(3, "March");
        public static readonly Month April = new Month(4, "April");
        public static readonly Month May = new Month(5, "May");
        public static readonly Month June = new Month(6, "June");
        public static readonly Month July = new Month(7, "July");
        public static readonly Month August = new Month(8, "August");
        public static readonly Month September = new Month(9, "September");
        public static readonly Month October = new Month(10, "October");
        public static readonly Month November = new Month(11, "November");
        public static readonly Month December = new Month(12, "December");

        private Month(int id, string name) : base(id, name) { }
    }
}
