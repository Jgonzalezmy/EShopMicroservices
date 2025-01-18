

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

//Add services to the container

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);

    //Config validation behavior
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

//Add service Carter - minimal APIs
builder.Services.AddCarterWithAssemblies(assembly);

//Add service Fluent validation
builder.Services.AddValidatorsFromAssembly(assembly);

//Marten configuration
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

//Exception Handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure the http request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });


app.Run();
