using IssueTrackerApp.Server.Application.Issues.Commands;
using IssueTrackerApp.Server.Application.Issues.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace IssueTrackerApp.Server.Controllers
{
    //[Route("api/[controller]")]
    [Route("[controller]")]

    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateIssueCommand> _createIssueCommandValidator;
        private readonly IValidator<UpdateIssueCommand> _updateIssueCommandValidator;
        public IssuesController(IMediator mediator,
            IValidator<CreateIssueCommand> createIssueCommandValidator,
            IValidator<UpdateIssueCommand> updateIssueCommandValidator)
        {
            _mediator = mediator;
            _createIssueCommandValidator = createIssueCommandValidator;
            _updateIssueCommandValidator = updateIssueCommandValidator;

        }

        [HttpGet(Name = "GetIssues")]
        public async Task<IActionResult> GetIssues([FromQuery] GetIssuesQuery query)
        {
            var issues = await _mediator.Send(query);
            return Ok(issues);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue([FromBody] CreateIssueCommand command)
        {
            var validation = _createIssueCommandValidator.Validate(command);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var issue = await _mediator.Send(command);
            return Ok(issue);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIssue(Guid id, [FromBody] UpdateIssueCommand command)
        {
            var validation = _updateIssueCommandValidator.Validate(command);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            if (id != command.Id) return BadRequest();


            var issue = await _mediator.Send(command);
            if (issue == null)
            {
                return NotFound();
            }
            return Ok(issue);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(Guid id)
        {
            var result = await _mediator.Send(new DeleteIssueCommand { Id = id });
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
