using FluentValidation;

namespace Swimlane.Core.DTOs
{

    public class TestRequestDto
    {
        public Guid Id { get; set; }
        public string TestName { get; set; } = string.Empty;
    }
    public class UpsertTestRequestValidator : AbstractValidator<TestRequestDto>
    {
        public UpsertTestRequestValidator()
        {
            RuleFor(x => x.TestName)
                .NotEmpty().WithMessage("Please enter the test name.")
                .MaximumLength(100).WithMessage("Test name must be less than 100 characters.");
        }
    }

}
