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
builder.Services.AddScoped<ISitemapService, SitemapService>();
builder.Services.AddScoped<IMoviesJob, MoviesJob>();

WebApplication app = builder.Build();

//tillagt för blazor
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
