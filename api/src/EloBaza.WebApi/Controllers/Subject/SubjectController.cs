using AutoMapper;
using EloBaza.Application.Commands.ExamSession.Create;
using EloBaza.Application.Commands.Subject.Create;
using EloBaza.Application.Commands.Subject.Delete;
using EloBaza.Application.Commands.Subject.Update;
using EloBaza.Application.Queries.Common;
using EloBaza.Application.Queries.Subject;
using EloBaza.Application.Queries.Subject.Get;
using EloBaza.Application.Queries.Subject.GetAll;
using EloBaza.WebApi.Controllers.Common;
using EloBaza.WebApi.Controllers.Subject.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EloBaza.WebApi.Controllers.Subject
{
    public class SubjectController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SubjectController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all subjects 
        /// </summary>
        /// <param name="subjectFilteringParametersModel">Parameters to filter result by</param>
        /// <param name="pagingParametersModel">Pagination parameters</param>
        /// <response code="200">A list of subjects</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetAllSubjectsResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] SubjectFilteringParametersModel subjectFilteringParametersModel, [FromQuery] PagingParametersModel pagingParametersModel)
        {
            var subjectFilteringParameters = _mapper.Map<SubjectFilteringParameters>(subjectFilteringParametersModel);
            var pagingParameters = _mapper.Map<PagingParameters>(pagingParametersModel);
            var subjects = await _mediator.Send(new GetAllSubjects(subjectFilteringParameters, pagingParameters));

            return Ok(subjects);
        }

        /// <summary>
        /// Get a subject by name
        /// </summary>
        /// <param name="name">Name of a subject</param>
        /// <response code="200">Subject read model if found</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If not found</response>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(SubjectReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByName(string name)
        {
            var subject = await _mediator.Send(new GetSubject(name));

            return Ok(subject);
        }

        /// <summary>
        /// Create a subject
        /// </summary>
        /// <param name="createSubjectModel">Data required to create subject</param>
        /// <response code="201">Subject read model if succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="409">If subject with that name already exists</response> 
        [HttpPost]
        [ProducesResponseType(typeof(SubjectReadModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(CreateSubjectModel createSubjectModel)
        {
            var createSubjectData = _mapper.Map<CreateSubjectData>(createSubjectModel);
            var subject = await _mediator.Send(new CreateSubject(createSubjectData));

            return CreatedAtAction(nameof(GetByName), new { subject.Name }, subject);
        }

        /// <summary>
        /// Delete a subject
        /// </summary>
        /// <param name="name">Name of subject to delete</param>
        /// <response code="204">If deletion succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject does not exists</response>
        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string name)
        {
            await _mediator.Send(new DeleteSubject(name));

            return NoContent();
        }

        /// <summary>
        /// Update a subject
        /// </summary>
        /// <param name="name">Name of subject to update</param>
        /// <param name="updateSubjectModel">Data to update</param>
        /// <response code="204">If update succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject does not exists</response>
        /// <response code="409">If subject with given name already exists</response>
        [HttpPatch("{name}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(string name, UpdateSubjectModel updateSubjectModel)
        {
            var updateSubjectData = _mapper.Map<UpdateSubjectData>(updateSubjectModel);
            await _mediator.Send(new UpdateSubject(name, updateSubjectData));

            return NoContent();
        }

        /// <summary>
        /// Get an exam session by name
        /// </summary>
        /// <param name="subjectName">Name of a subject</param>
        /// <param name="name">Name of an exam session</param>
        /// <response code="200">Exam session read model if found</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If not found</response>
        [HttpGet("{subjectName}/ExamSession/{name}")]
        [ProducesResponseType(typeof(SubjectReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExamSessionByName(string subjectName, string name)
        {
            return Ok();
        }

        /// <summary>
        /// Create an exam session for a subject
        /// </summary>
        /// <param name="subjectName">Name of subject to create exam session for</param>
        /// <param name="createExamSessionModel">Data required to create exam session</param>
        /// <response code="201">Exam session read model if succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject does not exists</response>
        /// <response code="409">If exam session in given subject already exists</response>
        [HttpPost("{subjectName}/ExamSession")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateExamSession(string subjectName, CreateExamSessionForSubjectModel createExamSessionModel)
        {
            var createExamSessionForSubjectData = _mapper.Map<CreateExamSessionForSubjectData>(createExamSessionModel);
            var examSession = await _mediator.Send(new CreateExamSessionForSubject(subjectName, createExamSessionForSubjectData));

            return CreatedAtAction(nameof(GetExamSessionByName), new { subjectName, name = examSession.Name }, examSession);
        }
    }
}
