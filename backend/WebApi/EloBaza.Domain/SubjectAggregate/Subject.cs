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

        #region Subject

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

        public void Update(int userId, string name)
        {
            Validate(name);

            Name = name;

            SetModificationData(userId);
        }

        public void Delete(int userId)
        {
            MarkAsDeleted(userId);
        }

        public void Restore(int userId)
        {
            MarkAsNotDeleted(userId);
        }

        private static void Validate(string name)
        {
            using var validationContext = new ValidationContext();
            validationContext.Validate(() => string.IsNullOrEmpty(name), nameof(Name), "Subject name must be provided");
            validationContext.Validate(() => name.Length > NameMaxLength, nameof(name), "Category name maximum length (50) exceeded");
        }

        #endregion

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
                throw new NotFoundException($"Exam session with Key: {examSessionKey} does not exists");

            examSession.MarkAsDeleted(userId);
        }

        public void RestoreExamSession(int userId, Guid examSessionKey)
        {
            var examSession = FindExamSession(examSessionKey);
            if (examSession is null)
                throw new NotFoundException($"Exam session with Key: {examSessionKey} does not exists for subject: {Key}");

            examSession.MarkAsNotDeleted(userId);
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

        public Category CreateCategory(int userId, string name, Guid? parentCategoryKey)
        {
            var parentCategory = FindCategory(parentCategoryKey);

            Category category;
            if (parentCategory is null)
            {
                category = Category.CreateRoot(userId, this, name);
                if (CategoryAlreadyExists(category, parentCategory))
                    throw new AlreadyExistsException($"Category: {category.Name} already exists");
                Categories.Add(category);
            }
            else
            {
                category = parentCategory.CreateSubCategory(userId, name);
            }

            return category;
        }

        public void UpdateCategory(int userId, Guid categoryKey, string name, Guid? parentCategoryKey)
        {
            var category = FindCategory(categoryKey);
            if (category is null)
                throw new NotFoundException($"Category with Key: {categoryKey} does not exists");

            var newName = name;
            var newParentCategory = FindCategory(parentCategoryKey);

            if (parentCategoryKey.HasValue && newParentCategory is null)
                throw new NotFoundException($"Category with key: {parentCategoryKey} does not exists");

            var hasChanged = category.Name != newName || category.ParentCategory != newParentCategory;
            if (hasChanged)
            {
                if (!(newParentCategory is null) && PotentialParentIsChildOf(category, newParentCategory))
                    throw new AlreadyExistsException($"Cannot assign parent as a child of its child");

                category.Update(userId, newParentCategory, newName);

                if (CategoryAlreadyExists(category, newParentCategory))
                    throw new AlreadyExistsException($"Category: {category.Name} already exists");
            }
        }

        public void DeleteCategory(int userId, Guid categoryKey)
        {
            var category = FindCategory(categoryKey);
            if (category is null)
                throw new NotFoundException($"Category with Key: {categoryKey} does not exists");

            category.MarkAsDeleted(userId);
        }

        public void RestoreCategory(int userId, Guid categoryKey)
        {
            var category = FindCategory(categoryKey);
            if (category is null)
                throw new NotFoundException($"Category with Key: {categoryKey} does not exists");

            category.MarkAsNotDeleted(userId);
        }

        private Category? FindCategory(Guid? categoryKey)
        {
            if (categoryKey is null)
                return null;

            var rootCategory = Categories.SingleOrDefault(c => c.Key == categoryKey);
            if (!(rootCategory is null))
                return rootCategory;

            foreach (var category in Categories)
            {
                var foundCategory = category.Seek(categoryKey.Value);

                if (!(foundCategory is null))
                    return foundCategory;
            }

            return null;
        }

        private bool CategoryAlreadyExists(Category category, Category? parentCategory)
        {
            if (parentCategory is null)
            {
                var foundCategory = Categories.FirstOrDefault(sc => sc.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase));
                if (foundCategory is null)
                    return false;

                return category != foundCategory;
            }
            else
            {
                return parentCategory.SubCategoryAlreadyExists(category);
            }
        }

        private bool PotentialParentIsChildOf(Category category, Category? potentialParentCategory)
        {
            if (category == potentialParentCategory)
                return true;

            if (potentialParentCategory is null || potentialParentCategory.IsRootCategory)
                return false;

            return PotentialParentIsChildOf(category, potentialParentCategory.ParentCategory);
        }

        #endregion
    }
}
