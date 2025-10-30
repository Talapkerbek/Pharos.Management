using Pharos.Management.API.HostedServices;
using Pharos.Management.Application.Exceptions;
using Pharos.Management.Domain.DomainServices;
using Pharos.Management.Infra.Logging;
using Pharos.Management.Infra.Marten;
using Pharos.Management.Infra.Marten.QueryServices;
using Pharos.Management.Infra.Repositories;
using Pharos.Management.Infra.Wolverine;

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