using AuthApex.Application;
using AuthApex.Application.Common.Constants;
using AuthApex.Infrastructure;
using AuthApex.Infrastructure.Persistence;
using AuthApex.WebApi;
using AuthApex.WebApi.Filters;
using AuthApex.WebApi.Middleware;
using Scalar.AspNetCore;

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
    var context = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
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
    c.SwaggerEndpoint("/swagger/v1/swagger.json", ConstantesAuth.ApiTitle);
    c.RoutePrefix = "swagger";
});

app.MapOpenApi();
app.MapScalarApiReference("/scalar", options =>
{
    options.Title = ConstantesAuth.ApiTitle;
    options.Theme = ScalarTheme.Laserwave;
});

app.MapControllers();

await app.RunAsync();
