

namespace IssueTrackerApp.Server.Domain
{

    public class IssueItem
    {
        public Guid Id { get; set; }// (Guid or int)
        public required string Title { get; set; }//(string, required)
        public string? Description { get; set; }//(string, optional)
        public string Priority { get; set; }//(enum/string: e.g., Low, Medium, High)
        public string Status { get; set; }//(enum/string: e.g., Open, InProgress, Done)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; //dAt(DateTime)
        public DateTime? UpdatedAt { get; set; }//(DateTime, optional)
    }
}