using FluentValidation;
using FreeBilling.Data.Entities;
using FreeBilling.Web.Apis;
using FreeBilling.Web.Data;
using FreeBilling.Web.Services;
using FreeBilling.Web.Validators;
using Mapster;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FreeBilling.Web.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BillingDb") ?? throw new InvalidOperationException("Connection string 'BillingContextConnection' not found.");

IConfigurationBuilder configBuilder = builder.Configuration;

configBuilder.Sources.Clear();
configBuilder.AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json", true)
    .AddUserSecrets(Assembly.GetExecutingAssembly())
    .AddEnvironmentVariables()
    .AddCommandLine(args);

builder.Services.AddDbContext<BillingContext>();

builder.Services.AddDefaultIdentity<TimeBillUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<BillingContext>();

builder.Services.AddAuthentication()
    .AddJwtBearer(cfg =>
    {
        cfg.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"]))
        };
    });

builder.Services.AddScoped<IBillingRepository, BillingRepository>();

// Razor pages dependency injections 

builder.Services.AddRazorPages();

builder.Services.AddTransient<IEmailServices, DevTimeEmailServices>();

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<TimeBillModelValidator>();

TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetEntryAssembly()!);

var app = builder.Build();


if (builder.Environment.IsDevelopment())
{
    // Shows developer page if exception happens
    app.UseDeveloperExceptionPage();
}
//Allows us to serve index.html as the default webpage
//app.UseDefaultFiles();


//Allows us to serve files from wwwroot.
app.UseStaticFiles();

//Add Routing
app.UseRouting();
app.UseAuthentication();;

//Add auth middleware
app.UseAuthorization();

app.MapRazorPages();

//app.run(async ctx =>
//{
//    await ctx.response.writeasync("welcome to freebilling");
//});

TimeBillsApi.Register(app);
AuthApi.Register(app);

app.MapControllers();

app.Run();
