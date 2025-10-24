using AuthApex.Application;
using AuthApex.Infrastructure;
using AuthApex.Infrastructure.Persistence;
using AuthApex.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.AddKeyVaultIfConfigured();
builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Swagger na raiz
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthApex API v1");
    c.RoutePrefix = "swagger";
});

app.MapControllers();

await app.RunAsync();
