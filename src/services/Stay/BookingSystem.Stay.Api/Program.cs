using BookingSystem.Stay.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
   .AddAndConfigApiVersioning()
   .AddAndConfigSwagger();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
