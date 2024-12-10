using Mapper;
using API;
using Data;
using Repo;
using Newtonsoft.Json;
using System.Globalization;
using Swashbuckle.AspNetCore.SwaggerUI;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.Culture = CultureInfo.InvariantCulture;
    opt.SerializerSettings.DateFormatString = "dddd, dd, MMMM, yyyy hh:mm:ss tt K";
    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Student-Management.API",
        Version = "v1",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email = "sahil.parsaniya270@gmail.com",
            Name = "Sahil Parsaniya",
        }
    });
});

builder.Services.AddDBContextDependencies(builder.Configuration.GetConnectionString("SQLServer")!);
builder.Services.AddRepositories();
builder.Services.AddMappers();
builder.Services.AddServices();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Student-Management.API";
        options.DocExpansion(DocExpansion.None);
        options.EnableFilter();
        options.EnableTryItOutByDefault();
    });
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
