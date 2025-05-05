using IssueTrackerApp.Server.Application.Issues.Commands;
using FluentValidation;


namespace IssueTrackerApp.Server.Application.Issues
{
    public class CreateIssueCommandValidator : AbstractValidator<CreateIssueCommand>
    {
        public CreateIssueCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotEqual("").NotNull().MaximumLength(100).WithMessage("Title is required");
            RuleFor(x => x.Priority).NotEmpty().WithMessage("Priority is required").Must(p => new[] { "Low", "Medium", "High" }.Contains(p));
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required").Must(s => new[] { "Open", "InProgress", "Closed" }.Contains(s));
        }
    }

    public class UpdateIssueCommandValidator : AbstractValidator<UpdateIssueCommand>
    {
        public UpdateIssueCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotEqual("").NotNull().MaximumLength(100).WithMessage("Title is required");
            RuleFor(x => x.Priority).NotEmpty().WithMessage("Priority is required").Must(p => new[] { "Low", "Medium", "High" }.Contains(p));
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required").Must(s => new[] { "Open", "InProgress", "Closed" }.Contains(s));
        }
    }
}