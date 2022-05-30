//yonatan

using DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection").ToString();
builder.Services.AddDbContext<DataAccess.AssignmentContext>(options => options.UseSqlServer(connectionString).LogTo(Console.WriteLine));

builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<IAssignmentTypeService, AssignmentTypeService>();

builder.Services.AddHostedService<AssignmentsBackgroundService>();

builder.Services.AddCors(options => options.AddPolicy("EnableCORS", builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .Build();
}));

ILogger logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
logger.LogInformation("This is a testlog");

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("EnableCORS");

app.MapControllers();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

using (var scope = app.Services.CreateScope()) { 
    var context = scope.ServiceProvider.GetRequiredService<AssignmentContext>();
    context.Database.Migrate();
};

app.Run();
