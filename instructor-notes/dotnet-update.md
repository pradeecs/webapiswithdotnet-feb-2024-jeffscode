# .NET Update Stuff

## Top Level Statements

If yes, everything in `Program.cs` gets compiled into the `static Main` method of a internal class called "Program". It is your *entry point*.

## File-Scoped Namespaces

While you can have multiple nested namespaces in a source code file, your fellow programmers will hate you.

Usually it is everything in one file in a namespace.

Just declare the namespace at that top of a source code file like this:

```csharp
namespace MyApi;

public class Thingy {}

public record Tacos {}
```

Everything in that file will be in the `MyApi` Namespace.

### Global Usings

### Implicit Usings

Set in your `.csproj` file.

https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview#implicit-using-directives


## Records 

## Primary Constructors

