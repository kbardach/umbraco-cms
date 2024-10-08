using kim_umbraco.Business.ScheduledJobs;
using kim_umbraco.Business.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

//tillagt f�r blazor
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ISitemapService, SitemapService>();
builder.Services.AddScoped<IMoviesJob, MoviesJob>();

WebApplication app = builder.Build();

//tillagt f�r blazor
app.MapBlazorHub();

await app.BootUmbracoAsync();

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
