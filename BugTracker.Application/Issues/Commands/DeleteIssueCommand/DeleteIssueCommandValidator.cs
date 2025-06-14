using FluentValidation;

namespace BugTracker.Application.Issues.Commands.DeleteIssueCommand;

public class DeleteIssueCommandValidator : AbstractValidator<DeleteIssueCommand>
{
    public DeleteIssueCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Issue ID must not be empty.");
    }
}