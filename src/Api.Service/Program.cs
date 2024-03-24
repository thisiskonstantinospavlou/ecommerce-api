using Api.Service;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

var configuration =
    new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "Api.Service.xml");
    c.IncludeXmlComments(filePath);
});
builder.Services.AddServices(configuration);

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
