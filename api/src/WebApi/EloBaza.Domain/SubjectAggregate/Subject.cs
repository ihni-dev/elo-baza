using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EloBaza.Domain.SubjectAggregate
{
    public class Subject : AggregateRoot
    {
        public const int NameMaxLength = 50;

        public string Name { get; private set; } = string.Empty;
        public ICollection<ExamSession> ExamSessions { get; private set; } = new List<ExamSession>();
        public ICollection<Category> Categories { get; private set; } = new List<Category>();
        public ICollection<Test> Tests { get; private set; } = new List<Test>();

        protected Subject() { }

        protected Subject(string name)
        {
            Key = Guid.NewGuid();
            Name = name;
        }

        public static Subject Create(int userId, string name)
        {
            Validate(name);

            var subject = new Subject(name);
            subject.SetCreationData(userId);

            return subject;
        }

        public void UpdateName(int userId, string name)
        {
            Validate(name);

            Name = name;

            SetModificationData(userId);
        }

        public void Delete(int userId)
        {
            MarkAsDeleted(userId);
        }

        private static void Validate(string name)
        {
            using var validationContext = new ValidationContext();
            validationContext.Validate(() => string.IsNullOrEmpty(name), nameof(Name), "Subject's name must be provided");
        }

        #region ExamSession

        public ExamSession CreateExamSession(int userId, short year, Semester semester, byte resitNumber)
        {
            var examSession = ExamSession.Create(userId, this, year, semester, resitNumber);
            if (ExamSessionAlreadyExists(year, semester, resitNumber))
                throw new AlreadyExistsException($"Exam session: {examSession.Name} already exists");

            ExamSessions.Add(examSession);
            return examSession;
        }

        public void UpdateExamSession(int userId, Guid examSessionKey, short? year, Semester? semester, byte? resitNumber)
        {
            var examSession = FindExamSession(examSessionKey);
            if (examSession is null)
                throw new NotFoundException($"Exam session with Key: {examSessionKey} does not exists");

            var newYear = year ?? examSession.Year;
            var newSemester = semester ?? examSession.Semester;
            var newResitNumber = resitNumber ?? examSession.ResitNumber;

            var hasChanged = examSession.Year != newYear || examSession.Semester != newSemester || examSession.ResitNumber != newResitNumber;
            if (hasChanged)
            {
                if (ExamSessionAlreadyExists(newYear, newSemester, newResitNumber))
                    throw new AlreadyExistsException($"Exam session for year: {year}, semester: {semester} and resit: {resitNumber ?? 0} already exists");
                else
                    examSession.Update(userId, newYear, newSemester, newResitNumber);
            }
        }

        public void DeleteExamSession(int userId, Guid examSessionKey)
        {
            var examSession = FindExamSession(examSessionKey);
            if (examSession is null)
                throw new NotFoundException($"Exam session with Key: {examSessionKey} does not exists for subject: {Key}");

            examSession.MarkAsDeleted(userId);
        }

        private ExamSession FindExamSession(Guid examSessionKey)
        {
            return ExamSessions.FirstOrDefault(es => es.Key == examSessionKey);
        }

        private bool ExamSessionAlreadyExists(short year, Semester semester, byte resitNumber)
        {
            return ExamSessions.Any(es => es.Year == year && es.Semester == semester && es.ResitNumber == resitNumber);
        }

        #endregion


        #region Category

        public Category CreateRootCategory(int userId, string name)
        {
            var category = Category.Create(userId, this, name);
            if (CategoryAlreadyExists(category.Name))
                throw new AlreadyExistsException($"Category: {category.Name} already exists");

            Categories.Add(category);
            return category;
        }

        public Category CreateLeafCategory(int userId, string name, Guid parentCategoryKey)
        {
            var parentCategory = FindCategory(parentCategoryKey);
            if (parentCategory is null)
                throw new NotFoundException($"Parent category with Key: {parentCategoryKey} does not exists");

            var category = Category.Create(userId, this, name, parentCategory);
            parentCategory.AddSubCategory(category);

            return category;
        }

        public void UpdateCategory(int userId, Guid categoryKey, string? name)
        {
            var category = FindCategory(categoryKey);
            if (category is null)
                throw new NotFoundException($"Category with Key: {categoryKey} does not exists");

            var newName = name ?? category.Name;

            var hasChanged = category.Name != newName;
            if (hasChanged)
                category.Update(userId, newName);
        }

        public void DeleteCategory(int userId, Guid categoryKey)
        {
            var category = FindCategory(categoryKey);
            if (category is null)
                throw new NotFoundException($"Category with Key: {categoryKey} does not exists");

            category.MarkAsDeleted(userId);
        }

        private Category? FindCategory(Guid categoryKey)
        {
            foreach (var category in Categories)
            {
                var foundedCategory = category.Seek(categoryKey);

                if (!(foundedCategory is null))
                    return foundedCategory;
            }

            return null;
        }

        private bool CategoryAlreadyExists(string categoryName)
        {
            return Categories.Any(c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        }

        #endregion
    }
}
