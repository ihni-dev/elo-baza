using EloBaza.Application.Commands.Create;
using EloBaza.Application.Queries.Subject.Get;
using EloBaza.Application.Queries.Subject.GetAll;
using EloBaza.WebApi.Controllers.Common;
using EloBaza.WebApi.Controllers.Subject.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EloBaza.WebApi.Controllers.Subject
{
    public class SubjectController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public SubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all subjects
        /// </summary>
        /// <param name="subjectFilteringParameters">Parameters to filter result by</param>
        /// <param name="pagingParameters">Pagination parameters</param>
        /// <returns code="200">A list of subjects</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetAllSubjectsResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] SubjectFilteringParameters subjectFilteringParameters, [FromQuery] PagingParameters pagingParameters)
        {
            var subjects = await _mediator.Send(new GetAllSubjects(subjectFilteringParameters, pagingParameters));

            return Ok(subjects);
        }

        /// <summary>
        /// Gets a subject by Id
        /// </summary>
        /// <param name="id">Id of a subject</param>
        /// <returns code="200">Subject if found</returns>
        /// <returns code="404">If not found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetSubjectResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var subject = await _mediator.Send(new GetSubject(id));

            return Ok(subject);
        }

        /// <summary>
        /// Creates a Subject
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /subject
        ///     {
        ///        "name": "SubjectName"
        ///     }
        ///
        /// </remarks>
        /// <param name="dto"></param>
        /// <returns>A newly created Subject's Id</returns>
        /// <response code="201">Subject Id</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="409">If subject with that name already exists</response> 
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(CreateSubjectData dto)
        {
            var id = await _mediator.Send(new CreateSubject(dto));

            return CreatedAtAction(nameof(GetById), new { id }, id);
        }
    }
}
