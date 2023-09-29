using Customer.WebApi.DB;
using Customer.WebApi.HealthChecks;
using Customer.WebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = new ConfigService(builder.Configuration);

builder.Services.AddSingleton(config);
builder.Services.AddDbContext<CustomerContext>(options => options.UseNpgsql(config.ConnectionStringPostgres));
builder.Services.AddScoped<CustomerService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks().AddCheck<HealthCheck>("Healthz");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.MapHealthChecks("/hc");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
