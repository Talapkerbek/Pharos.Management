using Pharos.Organization.API.HostedServices;
using Pharos.Organization.Application.Exceptions;
using Pharos.Organization.Domain.DomainServices;
using Pharos.Organization.Infra.Logging;
using Pharos.Organization.Infra.Marten;
using Pharos.Organization.Infra.Marten.QueryServices;
using Pharos.Organization.Infra.Repositories;
using Pharos.Organization.Infra.Wolverine;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureExceptionHandler()
    .AddAndConfigureSerilog(builder.Configuration)
    .AddAndConfigureMarten(builder.Configuration)
    .AddRepositories()
    .AddDomainServices()
    .AddQueryServices();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddHostedService<RebuildProjectionHostedService>();

builder.Host.AddWolverineWithAssemblyDiscovery(builder.Configuration, [typeof(ExceptionHandler).Assembly]);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();