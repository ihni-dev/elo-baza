using AutoMapper;
using EloBaza.Application.Commands.SubjectAggregate.Category.Create;
using EloBaza.Application.Commands.SubjectAggregate.Category.Delete;
using EloBaza.Application.Commands.SubjectAggregate.Category.Restore;
using EloBaza.Application.Commands.SubjectAggregate.Category.Update;
using EloBaza.Application.Commands.SubjectAggregate.Create;
using EloBaza.Application.Commands.SubjectAggregate.Delete;
using EloBaza.Application.Commands.SubjectAggregate.ExamSession.Create;
using EloBaza.Application.Commands.SubjectAggregate.ExamSession.Delete;
using EloBaza.Application.Commands.SubjectAggregate.ExamSession.Restore;
using EloBaza.Application.Commands.SubjectAggregate.ExamSession.Update;
using EloBaza.Application.Commands.SubjectAggregate.Restore;
using EloBaza.Application.Commands.SubjectAggregate.Update;
using EloBaza.Application.Queries.Common;
using EloBaza.Application.Queries.SubjectAggregate.Category.Get;
using EloBaza.Application.Queries.SubjectAggregate.Category.GetAll;
using EloBaza.Application.Queries.SubjectAggregate.ExamSession.Get;
using EloBaza.Application.Queries.SubjectAggregate.ExamSession.GetAll;
using EloBaza.Application.Queries.SubjectAggregate.Get;
using EloBaza.Application.Queries.SubjectAggregate.GetAll;
using EloBaza.WebApi.Controllers.Common;
using EloBaza.WebApi.Controllers.Subject.Models;
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
        /// <param name="pagingParametersModel">Pagination parameters</param>
        /// <param name="subjectFilteringParametersModel">Parameters to filter result by</param>
        /// <response code="200">A list of subjects</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetAllSubjectsResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] PagingParametersModel pagingParametersModel,
            [FromQuery] SubjectFilteringParametersModel subjectFilteringParametersModel)
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
        /// <response code="200">Subject setails read model if found</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If not found</response>
        [HttpGet("{subjectKey}")]
        [ProducesResponseType(typeof(SubjectDetailsReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid subjectKey)
        {
            var subject = await _mediator.Send(new GetSubjectDetails(subjectKey));

            return Ok(subject);
        }

        /// <summary>
        /// Create a subject
        /// </summary>
        /// <param name="createSubjectModel">Data required to create subject</param>
        /// <response code="201">Subject details read model if succeeded</response>
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

            return CreatedAtAction(nameof(Get), new { subjectKey = subject.Key }, subject);
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
        /// Restore a subject
        /// </summary>
        /// <param name="subjectKey">Key of a subject to resore</param>
        /// <response code="204">If resotration succeeded</response>
        /// <response code="400">If validation ailed</response> 
        /// <response code="404">If subject does not exists</response>
        [HttpPatch("{subjectKey}/restore")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Restore(Guid subjectKey)
        {
            await _mediator.Send(new RestoreSubject(requestorId: 1, subjectKey));

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
        /// Get all exam sessions 
        /// </summary>
        /// <param name="subjectKey">Key of an exam session subject</param>
        /// <response code="200">A list of exam sessions</response>
        [HttpGet("{subjectKey}/exam-session")]
        [ProducesResponseType(typeof(GetAllExamSessionsResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllExamSessions(Guid subjectKey)
        {
            var examSessions = await _mediator.Send(new GetAllExamSessions(subjectKey));

            return Ok(examSessions);
        }

        /// <summary>
        /// Get an exam session by key
        /// </summary>
        /// <param name="subjectKey">Key of an exam session subject</param>
        /// <param name="examSessionKey">Key of an exam session</param>
        /// <response code="200">Exam session read model if found</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If not found</response>
        [HttpGet("{subjectKey}/exam-session/{examSessionKey}")]
        [ProducesResponseType(typeof(ExamSessionDetailsReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExamSession(Guid subjectKey, Guid examSessionKey)
        {
            var examSession = await _mediator.Send(new GetExamSessionDetails(subjectKey, examSessionKey));

            return Ok(examSession);
        }

        /// <summary>
        /// Create an exam session for a subject
        /// </summary>
        /// <param name="subjectKey">Key of a subject to create exam session for</param>
        /// <param name="createExamSessionModel">Data required to create an exam session</param>
        /// <response code="201">Exam session details read model if succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject does not exists</response>
        /// <response code="409">If exam session in given subject already exists</response>
        [HttpPost("{subjectKey}/exam-session")]
        [ProducesResponseType(typeof(ExamSessionDetailsReadModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateExamSession(Guid subjectKey, CreateExamSessionModel createExamSessionModel)
        {
            var createExamSessionData = _mapper.Map<CreateExamSessionData>(createExamSessionModel);
            var examSession = await _mediator.Send(new CreateExamSession(requestorId: 1, subjectKey, createExamSessionData));

            return CreatedAtAction(nameof(GetExamSession), new { subjectKey, examSessionKey = examSession.Key }, examSession);
        }

        /// <summary>
        /// Delete an exam session
        /// </summary>
        /// <param name="subjectKey">Key of an exam session subject</param>
        /// <param name="examSessionKey">Key of an exam session to delete</param>
        /// <response code="204">If deletion succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject or exam session does not exists</response>
        [HttpDelete("{subjectKey}/exam-session/{examSessionKey}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExamSession(Guid subjectKey, Guid examSessionKey)
        {
            await _mediator.Send(new DeleteExamSession(requestorId: 1, subjectKey, examSessionKey));

            return NoContent();
        }

        /// <summary>
        /// Restore an exam session
        /// </summary>
        /// <param name="subjectKey">Key of an exam session subject</param>
        /// <param name="examSessionKey">Key of an exam session to restore</param>
        /// <response code="204">If restoration succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject or exam session does not exists</response>
        [HttpPatch("{subjectKey}/exam-session/{examSessionKey}/restore")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RestoreExamSession(Guid subjectKey, Guid examSessionKey)
        {
            await _mediator.Send(new RestoreExamSession(requestorId: 1, subjectKey, examSessionKey));

            return NoContent();
        }

        /// <summary>
        /// Update an exam session
        /// </summary>
        /// <param name="subjectKey">Key of an exam session subject</param>
        /// <param name="examSessionKey">Key of an exam session to update</param>
        /// <param name="updateExamSessionModel">Data to update</param>
        /// <response code="204">If update succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject or exam session does not exists</response>
        /// <response code="409">If exam session with provided data already exists</response>
        [HttpPatch("{subjectKey}/exam-session/{examSessionKey}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateExamSession(Guid subjectKey, Guid examSessionKey, UpdateExamSessionModel updateExamSessionModel)
        {
            var updateExamSessionData = _mapper.Map<UpdateExamSessionData>(updateExamSessionModel);
            await _mediator.Send(new UpdateExamSession(requestorId: 1, subjectKey, examSessionKey, updateExamSessionData));

            return NoContent();
        }

        #endregion

        #region Category

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <param name="subjectKey">Key of a category subject</param>
        /// <response code="200">A list of categories</response>
        [HttpGet("{subjectKey}/category")]
        [ProducesResponseType(typeof(GetAllCategoriesResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories(Guid subjectKey)
        {
            var categories = await _mediator.Send(new GetAllCategories(subjectKey));

            return Ok(categories);
        }

        /// <summary>
        /// Get a category by key
        /// </summary>
        /// <param name="subjectKey">Key of a category subject</param>
        /// <param name="categoryKey">Key of a category</param>
        /// <response code="200">Category read model if found</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If not found</response>
        [HttpGet("{subjectKey}/category/{categoryKey}")]
        [ProducesResponseType(typeof(CategoryDetailsReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategory(Guid subjectKey, Guid categoryKey)
        {
            var category = await _mediator.Send(new GetCategoryDetails(subjectKey, categoryKey));

            return Ok(category);
        }

        /// <summary>
        /// Create a category for a subject
        /// </summary>
        /// <param name="subjectKey">Key of a subject to create category for</param>
        /// <param name="createCategoryModel">Data required to create a category</param>
        /// <response code="201">Category details read model if succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject does not exists</response>
        /// <response code="409">If category in given subject on same level already exists</response>
        [HttpPost("{subjectKey}/category")]
        [ProducesResponseType(typeof(CategoryDetailsReadModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateCategory(Guid subjectKey, CreateCategoryModel createCategoryModel)
        {
            var createCategoryData = _mapper.Map<CreateCategoryData>(createCategoryModel);
            var category = await _mediator.Send(new CreateCategory(requestorId: 1, subjectKey, createCategoryData));

            return CreatedAtAction(nameof(GetCategory), new { subjectKey, examSessionKey = category.Key }, category);
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="subjectKey">Key of a category subject</param>
        /// <param name="categoryKey">Key of a category to delete</param>
        /// <response code="204">If deletion succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject or category does not exists</response>
        [HttpDelete("{subjectKey}/category/{categoryKey}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(Guid subjectKey, Guid categoryKey)
        {
            await _mediator.Send(new DeleteCategory(requestorId: 1, subjectKey, categoryKey));

            return NoContent();
        }

        /// <summary>
        /// Restore a category
        /// </summary>
        /// <param name="subjectKey">Key of a category subject</param>
        /// <param name="categoryKey">Key of a category to restore</param>
        /// <response code="204">If restoration succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject or category does not exists</response>
        [HttpPatch("{subjectKey}/category/{categoryKey}/restore")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RestoreCategory(Guid subjectKey, Guid categoryKey)
        {
            await _mediator.Send(new RestoreCategory(requestorId: 1, subjectKey, categoryKey));

            return NoContent();
        }

        /// <summary>
        /// Update a category
        /// </summary>
        /// <param name="subjectKey">Key of a category subject</param>
        /// <param name="categoryKey">Key of a category to update</param>
        /// <param name="updateCategoryModel">Data to update</param>
        /// <response code="204">If update succeeded</response>
        /// <response code="400">If validation failed</response> 
        /// <response code="404">If subject or category does not exists</response>
        /// <response code="409">If category with provided data on same level already exists or trying to assign parent as a child of its child</response>
        [HttpPatch("{subjectKey}/category/{categoryKey}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateCategory(Guid subjectKey, Guid categoryKey, UpdateCategoryModel updateCategoryModel)
        {
            var updateCategoryData = _mapper.Map<UpdateCategoryData>(updateCategoryModel);
            await _mediator.Send(new UpdateCategory(requestorId: 1, subjectKey, categoryKey, updateCategoryData));

            return NoContent();
        }

        #endregion
    }
}
