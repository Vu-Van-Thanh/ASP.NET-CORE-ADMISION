using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;
using RepositoryContracts;
using Repositories;
using Serilog;
using Admission.Filters.ActionFilters;
using Admission;
using Admission.Middleware;
using Admission.Core.ServiceContracts;

var builder = WebApplication.CreateBuilder(args);

//Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) => {

 loggerConfiguration
 .ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
 .ReadFrom.Services(services); //read out current app's services and make them available to serilog
} );


builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();


//create application pipeline
if (builder.Environment.IsDevelopment())
{
 app.UseDeveloperExceptionPage();
}
else
{
 app.UseExceptionHandler("/Error");
 app.UseExceptionHandlingMiddleware();
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseSerilogRequestLogging();


app.UseHttpLogging();

if (builder.Environment.IsEnvironment("Test") == false)
 Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");

app.UseStaticFiles();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); //Execute the filter pipiline (action + filters)

app.UseEndpoints(endpoints => {
 endpoints.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}");

 //Admin/Home/Index
 //Admin

 endpoints.MapControllerRoute(
  name: "default",
  pattern: "{controller}/{action}/{id?}"
  );
});


app.Run();

public partial class Program { } //make the auto-generated Program accessible programmatically

