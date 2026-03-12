using WebApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Implicit routing and enpoint handling
app.MapGet("/", () => "Hello World!");

app.MapGet("/emplyoees", () =>
{
    var employees = EmployeesRepository.GetEmployees();
    // As an object is being returned, it will be converted
    // to JSON by the framework
    return employees;
});

app.MapPost("/employees", (Employee employee) =>
{
    if (employee is null || employee.Id <= 0)
    {
        return "Provided employee is invalid";
    }
    EmployeesRepository.AddEmployee(employee);
    return "Employee added successfully";
}).WithParameterValidation();


app.Run();
