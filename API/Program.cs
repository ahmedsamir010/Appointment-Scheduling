using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)  
        .Enrich.FromLogContext()  
        .WriteTo.Console()  
        .WriteTo.MSSqlServer(
            context.Configuration.GetConnectionString("DefaultConnection"),
            new MSSqlServerSinkOptions
            {
                TableName = "Logs", 
                AutoCreateSqlTable = true  
            });
});


builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddApplicationService();
builder.Services.AddSwaggerServices();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseMiddleware<ExceptionMiddleware>(); 
app.UseStatusCodePagesWithRedirects("/errors/{0}");
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.UseSerilogRequestLogging(); 
app.MapControllers();

app.Run();

Log.CloseAndFlush();
