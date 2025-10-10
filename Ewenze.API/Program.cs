using Ewenze.API.Converters;
using Ewenze.API.Middleware;
using Ewenze.Application.Extensions;
using Ewenze.Infrastructure.Extensions; 


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplication();
builder.Services.AddEwenzeInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<UserConverter>();
builder.Services.AddSingleton<ListingTypeConverter>();
builder.Services.AddSingleton<ListingConverter>();

var app = builder.Build();

app.UseExceptionMiddleware();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
