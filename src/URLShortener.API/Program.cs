using Serilog;
using URLShortener.Application;
using URLShortener.Infrastructure;
using URLShortener.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File("Logs/App-.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();
builder.Host.UseSerilog(Log.Logger);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.RegisterApplication(builder.Configuration);
builder.Services.RegisterInfrastructure(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    var filePath = Path.Combine(AppContext.BaseDirectory, "URLShortener.API.xml");
    config.IncludeXmlComments(filePath);
});

var app = builder.Build();

app.Services.CreateScope().InitializeDatabase();

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.LocalRedirect("/swagger/index.html")).ExcludeFromDescription();
app.MapControllers();

app.Run();
