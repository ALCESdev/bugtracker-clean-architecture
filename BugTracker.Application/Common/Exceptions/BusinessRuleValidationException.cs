namespace BugTracker.Application.Common.Exceptions;

public class BusinessRuleValidationException : Exception
{
    public BusinessRuleValidationException(string message) : base(message) { }
}