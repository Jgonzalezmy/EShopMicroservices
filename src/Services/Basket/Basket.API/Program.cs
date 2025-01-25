var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add services to the container
builder.Services.AddCarterWithAssemblies(assembly); //Add service Carter - minimal APIs

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); //Config validation behavior   
    config.AddOpenBehavior(typeof(LogginBehavior<,>)); //Config loggin behavior
});

var app = builder.Build();


//Configure the http request pipeline
app.MapCarter();

app.Run();
