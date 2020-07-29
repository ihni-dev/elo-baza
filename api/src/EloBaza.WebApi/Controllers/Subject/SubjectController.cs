using AutoMapper;
using EloBaza.Application.Commands.SubjectAggregate.Create;
using EloBaza.Application.Commands.SubjectAggregate.Delete;
using EloBaza.Application.Commands.SubjectAggregate.ExamSession.Create;
using EloBaza.Application.Commands.SubjectAggregate.ExamSession.Delete;
using EloBaza.Application.Commands.SubjectAggregate.ExamSession.Update;
using EloBaza.Application.Commands.SubjectAggregate.Update;
using EloBaza.Application.Queries.Common;
using EloBaza.Application.Queries.SubjectAggregate.ExamSession.Get;
using EloBaza.Application.Queries.SubjectAggregate.Get;
using EloBaza.Application.Queries.SubjectAggregate.GetAll;
using EloBaza.WebApi.Controllers.Common;
using EloBaza.WebApi.Controllers.Subject.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EloBaza.WebApi.Controllers.Subject
{
    [Authorize]
    public class SubjectController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SubjectController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        #region Subject

        /// <summary>
        /// Get all subjects 
        /// </summary>
        /// <param name="subjectFilteringParametersModel">Parameters to filter result by</param>
        /// <param name="pagingParametersModel">Pagination parameters</param>
        /// <response code="200">A list of subjects</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetAllSubjectsResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] SubjectFilteringParametersModel subjectFilteringParametersModel,
            [FromQuery] PagingParametersModel pagingParametersModel)
        {
            var subjectFilteringParameters = _mapper.Map<SubjectFilteringParameters>(subjectFilteringParametersModel);
            var pagingParameters = _mapper.Map<PagingParameters>(pagingParametersModel);

            var subjects = await _mediator.Send(new GetAllSubjects(subjectFilteringParameters, pagingParameters));

            return Ok(subjects);
        }

        /// <summary>
        /// Get a subject by key
        /// </summary>
        /// <param name="subjectKey">Key of a subject</param>
        /// <response code="200">Subject read model if found</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If not found</response>
        [HttpGet("{subjectKey}")]
        [ProducesResponseType(typeof(SubjectReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByKey(Guid subjectKey)
        {
            var subject = await _mediator.Send(new GetSubjectDetails(subjectKey));

            return Ok(subject);
        }

        /// <summary>
        /// Create a subject
        /// </summary>
        /// <param name="createSubjectModel">Data required to create subject</param>
        /// <response code="201">Subject's details read model if succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="409">If subject with that name already exists</response> 
        [HttpPost]
        [ProducesResponseType(typeof(SubjectDetailsReadModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(CreateSubjectModel createSubjectModel)
        {
            var createSubjectData = _mapper.Map<CreateSubjectData>(createSubjectModel);
            var subject = await _mediator.Send(new CreateSubject(requestorId: 1, createSubjectData));

            return CreatedAtAction(nameof(GetByKey), new { subjectKey = subject.Key }, subject);
        }

        /// <summary>
        /// Delete a subject
        /// </summary>
        /// <param name="subjectKey">Key of a subject to delete</param>
        /// <response code="204">If deletion succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject does not exists</response>
        [HttpDelete("{subjectKey}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid subjectKey)
        {
            await _mediator.Send(new DeleteSubject(requestorId: 1, subjectKey));

            return NoContent();
        }

        /// <summary>
        /// Update a subject
        /// </summary>
        /// <param name="subjectKey">Key of a subject to update</param>
        /// <param name="updateSubjectModel">Data to update</param>
        /// <response code="204">If update succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject does not exists</response>
        [HttpPatch("{subjectKey}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(Guid subjectKey, UpdateSubjectModel updateSubjectModel)
        {
            var updateSubjectData = _mapper.Map<UpdateSubjectData>(updateSubjectModel);
            await _mediator.Send(new UpdateSubject(requestorId: 1, subjectKey, updateSubjectData));

            return NoContent();
        }

        #endregion

        #region Exam session

        /// <summary>
        /// Get an exam session by key
        /// </summary>
        /// <param name="subjectKey">Key of an exam session's subject</param>
        /// <param name="examSessionKey">Key of an exam session</param>
        /// <response code="200">Exam session read model if found</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If not found</response>
        [HttpGet("{subjectKey}/ExamSession/{examSessionKey}")]
        [ProducesResponseType(typeof(ExamSessionDetailsReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExamSessionByName(Guid subjectKey, Guid examSessionKey)
        {
            var examSession = await _mediator.Send(new GetExamSessionDetails(subjectKey, examSessionKey));

            return Ok(examSession);
        }

        /// <summary>
        /// Create an exam session for a subject
        /// </summary>
        /// <param name="subjectKey">Key of a subject to create exam session for</param>
        /// <param name="createExamSessionModel">Data required to create an exam session</param>
        /// <response code="201">Exam session's details read model if succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject does not exists</response>
        /// <response code="409">If exam session in given subject already exists</response>
        [HttpPost("{subjectKey}/ExamSession")]
        [ProducesResponseType(typeof(ExamSessionDetailsReadModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateExamSession(Guid subjectKey, CreateExamSessionModel createExamSessionModel)
        {
            var createExamSessionData = _mapper.Map<CreateExamSessionData>(createExamSessionModel);
            var examSession = await _mediator.Send(new CreateExamSession(requestorId: 1, subjectKey, createExamSessionData));

            return CreatedAtAction(nameof(GetExamSessionByName), new { subjectKey, examSessionKey = examSession.Key }, examSession);
        }

        /// <summary>
        /// Delete an exam session
        /// </summary>
        /// <param name="subjectKey">Key of an exam session's subject</param>
        /// <param name="examSessionKey">Key of an exam session to delete</param>
        /// <response code="204">If deletion succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject or exam session does not exists</response>
        [HttpDelete("{subjectKey}/ExamSession/{examSessionKey}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExamSession(Guid subjectKey, Guid examSessionKey)
        {
            await _mediator.Send(new DeleteExamSession(requestorId: 1, subjectKey, examSessionKey));

            return NoContent();
        }

        /// <summary>
        /// Update an exam session
        /// </summary>
        /// <param name="subjectKey">Key of an exam session's subject</param>
        /// <param name="examSessionKey">Key of an exam session to update</param>
        /// <param name="updateExamSessionModel">Data to update</param>
        /// <response code="204">If update succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject or exam session does not exists</response>
        /// <response code="409">If exam session with provided data already exists</response>
        [HttpPatch("{subjectKey}/ExamSession/{examSessionKey}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(Guid subjectKey, Guid examSessionKey, UpdateExamSessionModel updateExamSessionModel)
        {
            var updateExamSessionData = _mapper.Map<UpdateExamSessionData>(updateExamSessionModel);
            await _mediator.Send(new UpdateExamSession(requestorId: 1, subjectKey, examSessionKey, updateExamSessionData));

            return NoContent();
        }

        #endregion
    }
}
