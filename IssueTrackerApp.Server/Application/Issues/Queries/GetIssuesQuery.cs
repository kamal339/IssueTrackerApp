using MediatR;
using Microsoft.EntityFrameworkCore;
using IssueTrackerApp.Server.Infrastructure;

namespace IssueTrackerApp.Server.Application.Issues.Queries
{

    public class GetIssuesQuery : IRequest<List<IssueDto>>
    {
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public string? Search { get; set; }
    }

    public class GetIssuesQueryHandler : IRequestHandler<GetIssuesQuery, List<IssueDto>>
    {
        private readonly IssueDbContext _context;
        public GetIssuesQueryHandler(IssueDbContext context) => _context = context;

        public async Task<List<IssueDto>> Handle(GetIssuesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Issues.AsQueryable();

            if (!string.IsNullOrEmpty(request.Status))
                query = query.Where(i => i.Status == request.Status);

            if (!string.IsNullOrEmpty(request.Priority))
                query = query.Where(i => i.Priority == request.Priority);

            if (!string.IsNullOrEmpty(request.Search))
                query = query.Where(i => i.Title.Contains(request.Search) || i.Description.Contains(request.Search));

            return await query.Select(i => new IssueDto
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                Priority = i.Priority,
                Status = i.Status,
                CreatedAt = i.CreatedAt,
                UpdatedAt = i.UpdatedAt
            }).ToListAsync(cancellationToken);
        }
    }
}