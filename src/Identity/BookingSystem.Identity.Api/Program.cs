using BookingSystem.Identity.Api.Extensions;
using BookingSystem.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
   .AddAndConfigApiVersioning()
   .AddAndConfigSwagger();

builder.Services.AddIdentityServerConfig(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { options.RouteTemplate = $"swagger/{{documentName}}/docs.json"; });
    app.UseSwaggerUI(options =>
    {
        foreach (var description in app.DescribeApiVersions())
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/docs.json", description.GroupName.ToUpperInvariant());
    });
}

app.UseHttpsRedirection();

app.UseIdentityServer();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
