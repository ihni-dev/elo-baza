using EloBaza.Application.Commands.Create;
using EloBaza.Application.Queries;
using EloBaza.Domain.SharedKernel;
using EloBaza.WebApi.Controllers.Subject.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        /// Gets a subject by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns code="200">Subject if found</returns>
        /// <returns code="404">If not found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var subject = await _mediator.Send(new GetSubject(id));

            return Ok(subject);
        }

        /// <summary>
        /// Creates a Subject.
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateSubjectDto dto)
        {
            var id = await _mediator.Send(new CreateSubject(dto));

            return CreatedAtAction(nameof(GetById), new { id }, id);
        }
    }
}
