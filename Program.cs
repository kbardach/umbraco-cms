using kim_umbraco.Business.ScheduledJobs;
using kim_umbraco.Business.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

//tillagt för blazor
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMoviesJob, MoviesJob>();

WebApplication app = builder.Build();

//tillagt för blazor
app.MapBlazorHub();

await app.BootUmbracoAsync();

//app.UseStatusCodePages(async context =>
//{
//	if (context.HttpContext.Response.StatusCode == 404)
//	{
//		context.HttpContext.Response.Redirect("/error");
//		await Task.Yield();
//	}
//});

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
