using FluentValidation;

namespace BugTracker.Application.Issues.Commands.UpdateIssueCommand;

public class UpdateIssueCommandValidator : AbstractValidator<UpdateIssueCommand>
{
    public UpdateIssueCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");
        RuleFor(x => x.AssigneeId)
            .NotEmpty().WithMessage("Assignee ID is required.")
            .Must(x => x != Guid.Empty).WithMessage("Assignee ID cannot be an empty GUID.");
        RuleFor(x => x.ReporterId)
            .NotEmpty().WithMessage("Reporter ID is required.")
            .Must(x => x != Guid.Empty).WithMessage("Reporter ID cannot be an empty GUID.");
        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage("Project ID is required.")
            .Must(x => x != Guid.Empty).WithMessage("Project ID cannot be an empty GUID.");
    }
}