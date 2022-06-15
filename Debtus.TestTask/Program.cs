using AutoMapper;
using BLL;
using BLL_Interface;
using Debtus.DAL;
using Debtus.IDAL;
using Debtus.TestTask;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IEmployee_DAO, Employee_DAO>();
builder.Services.AddTransient<IWorkingShift_DAO, WorkingShift_DAO>();
builder.Services.AddTransient<ICheckpointLogic, CheckpointLogic>();
builder.Services.AddTransient<IHRDeparmentLogic, HRDepartmentLogic>();
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles   
);
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter())
    );
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
