var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.UseStaticFiles();

//app.run(async ctx =>
//{
//    await ctx.response.writeasync("welcome to freebilling");
//});

app.Run();
