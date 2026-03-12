using WebApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Implicit routing and enpoint handling
app.MapGet("/", () => "Hello World!");

app.MapGet("/employees", () =>
{
    var employees = EmployeesRepository.GetEmployees();
    // As an object is being returned, it will be converted
    // to JSON by the framework

    // The results.ok will also provide the appropiate status
    // code along with serialization, this is currently returning
    // an array of objects.
    // This is returning an IResult type.
    return Results.Ok(employees);

    //// Another more robust option is to retun Typed results
    //// The framework will extract the tpe when building documentation for example.
    //return TypedResults.Ok(employees);
});

//app.MapPost("/employees", (Employee employee) =>
//{
//    // Using TypedResults in the following code would result
//    // in a type mismatch error.
//    if (employee is null || employee.Id <= 0)
//    {
//        //// The following codewould still forward astatus code of 200 Ok
//        //// as a response is being sent albeit an undesirable one.
//        //return "Provided employee is invalid";
//        // Results.BadRequest() is the helper function that allows sending
//        // the correct status code along with a message if needed.
//        return Results.BadRequest("Provided employee is invalid") ;
//    }
//    EmployeesRepository.AddEmployee(employee);
//    return Results.Ok("Employee added successfully");
//}).WithParameterValidation();

// Reimplementing the above endpoint by mixing Results
// and TypedResults

app.MapPost("/employees", (Employee employee) =>
{
    if (employee is null || employee.Id <= 0)
    {
        // The following response follows the problem details standard.
        return Results.ValidationProblem(
            new Dictionary<string, string[]>
            {
            { "error", new[] { "Id not provided" }}
            }
                                        );
    }
    EmployeesRepository.AddEmployee(employee);
    return TypedResults.Created($"/employees/{employee.Id}", employee);
}).WithParameterValidation();


app.Run();
