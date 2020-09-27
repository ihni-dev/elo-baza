using EloBaza.Domain.SharedKernel;

namespace EloBaza.Domain.SubjectAggregate
{
    public class Test : Entity
    {
        public const int NameMaxLength = 70;
        public const short MinYear = 1950;
        public const short MaxYear = 2150;

        public string Name { get; private set; }
        public short Year { get; private set; }
        public Semester Semester { get; private set; }
        public byte OrderNumber { get; private set; }

        public Subject? Subject { get; private set; }

        protected Test()
        {

        }
    }
}
