using MediatR;
using IssueTrackerApp.Server.Domain;
using IssueTrackerApp.Server.Infrastructure;

namespace IssueTrackerApp.Server.Application.Issues.Commands

{
    public class CreateIssueCommand : IRequest<IssueDto>
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string Priority { get; set; } = "Medium";
        public string Status { get; set; } = "Open";
    }

    public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, IssueDto>
    {
        private readonly IssueDbContext _context;
        public CreateIssueCommandHandler(IssueDbContext context) => _context = context;

        public async Task<IssueDto> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            var issue = new IssueItem
            {
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                Status = request.Status
            };

            _context.Issues.Add(issue);
            await _context.SaveChangesAsync(cancellationToken);

            return new IssueDto
            {
                Id = issue.Id,
                Title = issue.Title,
                Description = issue.Description,
                Priority = issue.Priority,
                Status = issue.Status,
                CreatedAt = issue.CreatedAt
            };
        }
    }
}