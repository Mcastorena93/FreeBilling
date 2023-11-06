using FreeBilling.Data.Entities;
using FreeBilling.Web.Data;
using FreeBilling.Web.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

IConfigurationBuilder configBuilder = builder.Configuration;

configBuilder.Sources.Clear();
configBuilder.AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json", true)
    .AddUserSecrets(Assembly.GetExecutingAssembly())
    .AddEnvironmentVariables()
    .AddCommandLine(args);

builder.Services.AddDbContext<BillingContext>();
builder.Services.AddScoped<IBillingRepository, BillingRepository>();

// Razor pages dependency injections 

builder.Services.AddRazorPages();

builder.Services.AddTransient<IEmailServices, DevTimeEmailServices>();

builder.Services.AddControllers();

var app = builder.Build();


if (builder.Environment.IsDevelopment())
{
    // Shows developer page if exception happens
    app.UseDeveloperExceptionPage();
}
//Allows us to serve index.html as the default webpage
app.UseDefaultFiles();


//Allows us to serve files from wwwroot.
app.UseStaticFiles();


app.MapRazorPages();

//app.run(async ctx =>
//{
//    await ctx.response.writeasync("welcome to freebilling");
//});

app.MapGet("/api/timebills/{id:int}", async (IBillingRepository repository, int id) =>
{
    var bill = await repository.GetTimeBill(id);

    if (bill is null) Results.NotFound();

    return Results.Ok(bill);
})
.WithName("GetTimeBill");

app.MapPost("api/timebills", async (IBillingRepository repository, TimeBill model) =>
{
    repository.AddEntity(model);
    if(await repository.SaveChanges())
    {
        var newBill = await repository.GetTimeBill(model.Id);   
        return Results.CreatedAtRoute("GetTimeBill", new {id = model.Id}, model);
    }
    else
    {
        return Results.BadRequest();
    }
});

app.MapControllers();

app.Run();
