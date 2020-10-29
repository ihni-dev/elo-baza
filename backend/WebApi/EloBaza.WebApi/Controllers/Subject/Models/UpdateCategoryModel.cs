using System;
using System.ComponentModel.DataAnnotations;

namespace EloBaza.WebApi.Controllers.Subject.Models
{
    /// <summary>
    /// Data required for updating a category
    /// </summary>
    public class UpdateCategoryModel
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        [Required]
        [MinLength(1)]
        [MaxLength(Domain.SubjectAggregate.Category.NameMaxLength)]
        public string Name { get; set; } = default!;

        /// <summary>
        /// Key of a parent category
        /// </summary>
        public Guid? ParentCateogryKey { get; set; }
    }
}
