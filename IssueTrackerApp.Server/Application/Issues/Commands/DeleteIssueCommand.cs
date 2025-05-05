using MediatR;
using IssueTrackerApp.Server.Domain;
using IssueTrackerApp.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IssueTrackerApp.Server.Application.Issues.Commands

{
    public class DeleteIssueCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
    }

    public class DeleteIssueCommandHandler : IRequestHandler<DeleteIssueCommand, bool>
    {
        private readonly IssueDbContext _context;
        public DeleteIssueCommandHandler(IssueDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
        {
            var issue = await _context.Issues.FindAsync(new object[] { request.Id }, cancellationToken);
            if (issue == null) return false;

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}