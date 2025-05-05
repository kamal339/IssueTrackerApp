using MediatR;
using IssueTrackerApp.Server.Domain;
using IssueTrackerApp.Server.Infrastructure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IssueTrackerApp.Server.Application.Issues.Commands

{
    public class UpdateIssueCommand : IRequest<IssueDto>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }

    public class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueCommand, IssueDto>
    {
        private readonly IssueDbContext _context;
        public UpdateIssueCommandHandler(IssueDbContext context)
        {
            _context = context;
        }

        public async Task<IssueDto> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
        {
            var issue = await _context.Issues.FindAsync(new object[] { request.Id }, cancellationToken);
            if (issue == null) return null;

            issue.Title = request.Title;
            issue.Description = request.Description;
            issue.Priority = request.Priority;
            issue.Status = request.Status;
            issue.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return new IssueDto
            {
                Id = issue.Id,
                Title = issue.Title,
                Description = issue.Description,
                Priority = issue.Priority,
                Status = issue.Status,
                CreatedAt = issue.CreatedAt,
                UpdatedAt = issue.UpdatedAt
            };
        }
    }
}