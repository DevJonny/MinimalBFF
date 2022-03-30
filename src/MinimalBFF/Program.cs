using Paramore.Darker.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDarker().AddHandlersFromAssemblies();
    
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
