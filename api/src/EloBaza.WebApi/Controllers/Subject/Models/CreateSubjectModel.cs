﻿using System.ComponentModel.DataAnnotations;

namespace EloBaza.WebApi.Controllers.Subject.Models
{
    /// <summary>
    /// Data required for creating a subject
    /// </summary>
    public class CreateSubjectModel
    {
        /// <summary>
        /// Name of the subject
        /// </summary>
        [Required]
        [MinLength(1)]
        public string Name { get; set; } = default!;
    }
}