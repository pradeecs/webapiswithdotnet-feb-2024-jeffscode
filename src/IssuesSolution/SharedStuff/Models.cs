
using FluentValidation;

namespace SharedStuff;

public record AppointmentRequestModel
{
    public string For { get; set; } = string.Empty;
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }

}

public class AppointmentRequestModelValidator : AbstractValidator<AppointmentRequestModel>
{
    public AppointmentRequestModelValidator()
    {
        RuleFor(a => a.For).NotEmpty();
        RuleFor(a => a.StartTime).NotNull();
        RuleFor(a => a.EndTime).NotNull();

        RuleFor(a => a.EndTime).GreaterThan(a => a.StartTime);
    }
}