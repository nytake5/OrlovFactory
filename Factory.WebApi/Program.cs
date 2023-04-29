using System.Text.Json.Serialization;
using AutoMapper;
using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Factory.WebApi;
using Factory.WebApi.EnvironmentVariables;
using Factory.WebApi.Service;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

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
builder.Services.AddScoped<IUserDao, UserDao>();
builder.Services.AddScoped<ICheckpointLogic, CheckpointLogic>();
builder.Services.AddScoped<IHrDeparmentLogic, HrDepartmentLogic>();
builder.Services.AddScoped<IUserLogic, UserLogic>();

builder.Services.AddHostedService<UserMessageHandler>();

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

builder.Services.AddSingleton(s =>
{
    var factory = new ConnectionFactory()
    {
        HostName = config?.RabbitMqServerHostName,
        UserName = "orlov",
        Password = "2323"
    };
    var connection = factory.CreateConnection();
    return connection;
});

builder.Services.AddSingleton(s =>
{
    var connection = s.GetService<IConnection>();
    return connection?.CreateModel();
});

builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = "localhost:6379";
    options.InstanceName = "local";
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
