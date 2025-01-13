using BuildingBlocks.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.AddCarterWithAssemblies(typeof(Program).Assembly);
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});




var app = builder.Build();

//Configure the http request pipeline
app.MapCarter();
app.Run();
