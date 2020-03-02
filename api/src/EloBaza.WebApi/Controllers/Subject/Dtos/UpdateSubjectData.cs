using EloBaza.Application.Commands.Update;
using System.ComponentModel.DataAnnotations;

namespace EloBaza.WebApi.Controllers.Subject.Dtos
{
    public class UpdateSubjectData : IUpdateSubjectData
    {
        /// <summary>
        /// Name of the subject to update
        /// </summary>
        [MinLength(1)]
        public string? Name { get; set; }
    }
}
