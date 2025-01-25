var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

//Add services to the container

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); //Config validation behavior   
    config.AddOpenBehavior(typeof(LogginBehavior<,>)); //Config loggin behavior
});

builder.Services.AddCarterWithAssemblies(assembly); //Add service Carter - minimal APIs
builder.Services.AddValidatorsFromAssembly(assembly); //Add service Fluent validation

//Marten configuration
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();


builder.Services.AddExceptionHandler<CustomExceptionHandler>(); //Exception Handler

builder.Services.AddHealthChecks() //Healhchecks
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);


var app = builder.Build();

//Configure the http request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health", //Use checkHealt in JSON Format
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
