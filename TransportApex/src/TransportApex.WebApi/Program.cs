using Scalar.AspNetCore;
using TransportApex.Application;
using TransportApex.Application.Common.Constants;
using TransportApex.Infrastructure;
using TransportApex.Infrastructure.Persistence;
using TransportApex.WebApi;
using TransportApex.WebApi.Filters;
using TransportApex.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelAttribute>();
});

builder.AddKeyVaultIfConfigured();
builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TransportDbContext>();
    await context.Database.EnsureCreatedAsync();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", ConstantesTransport.ApiTitle);
    c.RoutePrefix = "swagger";
});

app.MapOpenApi();
app.MapScalarApiReference("/scalar", options =>
{
    options.Title = ConstantesTransport.ApiTitle;
    options.Theme = ScalarTheme.Mars;
});

app.MapControllers();

await app.RunAsync();
