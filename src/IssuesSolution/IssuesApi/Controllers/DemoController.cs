
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace IssuesApi.Controllers;

//[ApiController]
public class DemoController(ILogger<DemoController> logger) : ControllerBase
{
    // GET /demo
    [HttpGet("/demo")] // hey, if I get an GET request to /Demo, run this.
    public async Task<ActionResult<DemoResponse>> GetTheDemo(CancellationToken token)
    {
        // Some Long Running Operation like a database call.
        logger.LogInformation("Starting the database call");
        await Task.Delay(3000, token); // Classroom crap! Don't do this.
        logger.LogInformation("Done with the database call");

        var response = new DemoResponse("Looks Good!", DateTimeOffset.Now);
        return Ok(response);
    }


    // OData
    [HttpGet("/employees")]
    public ActionResult GetEmployees([FromQuery] string department = "All", [FromQuery] decimal minimalSalary = 0)
    {
        var employees = new List<EmployeeResponse>
        {
            new EmployeeResponse(Guid.NewGuid(), "Leland Palmer", "DEV" ),
            new EmployeeResponse(Guid.NewGuid(), "Harry S. Truman","Law" )
        };
        if (department == "All")
        {
            var response = new CollectionResponse<EmployeeResponse>(employees);
            return Ok(response);
        }
        else
        {
            var filteredEmployees = employees.Where(e => e.Department == department).ToList();
            var response = new CollectionResponse<EmployeeResponse>(filteredEmployees);
            return Ok(response);
        }
    }

    [HttpGet("/employees/{employeeId}")]
    public ActionResult GetAnEmployee(int employeeId)
    {
        if (employeeId % 2 == 0)
        {
            return Ok(new EmployeeResponse(Guid.NewGuid(), null, "DEV"));
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost("/employees")]
    public async Task<ActionResult> HireAnEmployee([FromBody] EmployeeHiringRequest2 request)
    {

        var validator = new EmployeeHiringValidator();

        var results = await validator.ValidateAsync(request);

        if (!results.IsValid)
        {
            return BadRequest(results);
        }

        if (request.StartingSalary.HasValue)
        {
            var requestedSalary = request.StartingSalary.Value;
        }

        return Ok(request);
    }

}

public record EmployeeHireRequest : IValidatableObject
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "We need a pay!")]
    public decimal? StartingSalary { get; set; }
    [Required]
    public EmployeeType? EmployeeType { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EmployeeType == Controllers.EmployeeType.FullTime && StartingSalary < 80000)
        {
            yield return new ValidationResult("You have to pay full time people more", [nameof(StartingSalary), nameof(EmployeeType)]);

        }
    }
}

public record EmployeeHiringRequest2
{

    public string? Name { get; set; }

    public string? EmailAddress { get; set; }


    public decimal? StartingSalary { get; set; }

    public EmployeeType? EmployeeType { get; set; }
}

public class EmployeeHiringValidator : AbstractValidator<EmployeeHiringRequest2>
{
    public EmployeeHiringValidator()
    {
        RuleFor(e => e.Name).NotNull().MaximumLength(100);
        RuleFor(e => e.EmailAddress).NotNull().EmailAddress();
        RuleFor(e => e.StartingSalary).NotNull().GreaterThan(100000).When(e => e.EmployeeType == EmployeeType.FullTime);
        RuleFor(e => e.EmailAddress).NotNull();
        RuleFor(e => e.EmailAddress).MustAsync(async (email, cancellationToken) =>
        {
            await Task.Delay(200);
            if (email == "jeff@hypertheory.com") { return false; }
            return true;
        }).WithMessage("We aren't going to hire THAT joker!");

    }

}


public enum EmployeeType { Intern, PartTime, FullTime }


public record DemoResponse(string Message, DateTimeOffset CreatedAt);

public record EmployeeResponse(Guid Id, string? Name, string Department);


public record CollectionResponse<T>(List<T> Data);