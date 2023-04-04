using System.Text.Json.Serialization;
using AutoMapper;
using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Entities.EnvironmentVariables;
using Factory.WebApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration
    .GetSection("EnvironmentVariables")
    .Get<EnvironmentVariables>();

var mappingConfig = new MapperConfiguration(mc =>
{
    var mappingProfile = new MappingProfile();
    mc.AddProfile(mappingProfile);
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();  

builder.Services.AddScoped<IEmployeeDao, EmployeeDao>();
builder.Services.AddScoped<IWorkingShiftDao, WorkingShiftDao>();
builder.Services.AddScoped<ICheckpointLogic, CheckpointLogic>();
builder.Services.AddScoped<IHrDeparmentLogic, HrDepartmentLogic>();


builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        var jsonConverter = new JsonStringEnumConverter();
        options.JsonSerializerOptions.Converters.Add(jsonConverter);
    }
);

builder.Services.AddDbContextFactory<FactoryContext>(
    optionsAction =>
    {
        optionsAction
            .UseNpgsql(config?.NpgsqlConnectionString);
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
