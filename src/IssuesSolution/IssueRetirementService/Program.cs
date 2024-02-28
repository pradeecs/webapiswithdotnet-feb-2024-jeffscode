using SharedStuff;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapPost("/retirements", (AppointmentRequestModel request) =>
{
    var validator = new AppointmentRequestModelValidator();
    // validate the thing, all that jazz
    Console.WriteLine(request.For);
    Console.WriteLine("Just another Change");
    return Results.Ok();
});

app.Run();


