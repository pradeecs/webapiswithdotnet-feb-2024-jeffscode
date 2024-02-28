namespace RecordsAndStuffRedux;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var sue = new Employee()
        {
            FirstName = "Susan",
            LastName = "Jones"
        };



        Assert.Equal("Jones, Susan", sue.FullName);
        // Assert.Equal("blah", sue.ToString());
    }

    [Fact]
    public void ContractorTests()
    {
        var jeff = new Contractor("Jeff", "Gonzalez")
        {
            Email = "jeff@hypertheory.com"
        };

        Assert.Equal("Jeff", jeff.FirstName);


    }

    [Fact]
    public void PrimaryConstructorsOnClasses()
    {
        var wa = new WorkAssignment(new Contractor("Tim", "S"));


    }

    [Fact]
    public void ImmutabilityOfRecords()
    {
        var sue = new Employee()
        {
            FirstName = "Susan",
            LastName = "Jones"
        };

        //var newSue = new Employee
        //{
        //    FirstName = sue.FirstName,
        //    LastName = sue.LastName,
        //    Email = "sue@aol.com"
        //};

        var newSue = sue with { Email = "sue@aol.com" };

        var quote = "Eat Your Wheaties";

        var updatedQuote = quote.ToUpper();
        Assert.Equal("EAT YOUR WHEATIES", updatedQuote);

        var ed = new Contractor("Ed", "Jones");

    }
}


public record Employee
{
    public required string FirstName { get; init; } = string.Empty;
    public required string LastName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string FullName
    {
        get
        {

            return $"{LastName}, {FirstName}";
        }
    }
}

public record Contractor(string FirstName, string LastName)
{
    public string Email { get; init; } = string.Empty;
};

public class WorkAssignment(Contractor contractor)
{
    public string WhoIsDoingTheWork()
    {
        return $"This is being done by {contractor.FirstName}";
    }
}