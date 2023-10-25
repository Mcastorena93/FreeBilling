var builder = WebApplication.CreateBuilder(args);


// Razor pages dependency injections 

builder.Services.AddRazorPages();

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

app.Run();
