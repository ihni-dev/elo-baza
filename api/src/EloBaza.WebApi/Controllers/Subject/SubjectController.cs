using EloBaza.Application.Commands.Create;
using EloBaza.Application.Commands.Delete;
using EloBaza.Application.Commands.Update;
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
        /// <response code="200">A list of subjects</response>
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
        /// <response code="200">Subject if found</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If not found</response>
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
        /// <param name="createSubjectData">Data required to create subject</param>
        /// <response code="201">Subject Id if created</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="409">If subject with that name already exists</response> 
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(CreateSubjectData createSubjectData)
        {
            var id = await _mediator.Send(new CreateSubject(createSubjectData));

            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        /// <summary>
        /// Deletes a Subject
        /// </summary>
        /// <param name="id">Id of subject to delete</param>
        /// <response code="204">If deletion succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject does not exists</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteSubject(id));

            return NoContent();
        }

        /// <summary>
        /// Updates a Subject
        /// </summary>
        /// <param name="id">Id of subject to update</param>
        /// <param name="updateSubjectData">Data to update</param>
        /// <response code="204">If deletion succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject does not exists</response>
        /// <response code="409">If subject with given name already exists</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(Guid id, UpdateSubjectData updateSubjectData)
        {
            await _mediator.Send(new UpdateSubject(id, updateSubjectData));

            return NoContent();
        }
    }
}
