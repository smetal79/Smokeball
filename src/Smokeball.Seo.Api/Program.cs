using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Smokeball.Seo.Api.Constants;
using Smokeball.Seo.Api.Handlers;
using Smokeball.Seo.Api.Search;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opts =>
{
    opts.CacheProfiles.Add(
        Cache.HourlyCache,
        new CacheProfile
        {
            Location = ResponseCacheLocation.Any,
            Duration = 3600
        });

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddTransient<ISearchRequestHandler, SeachRequestHandler>();

builder.Services
    .AddHttpClient()
    .AddTransient<ISearchRequest, SearchRequest>();

builder.Services.AddTransient<BingEngine>();
builder.Services.AddTransient<GoogleEngine>();


builder.Services.AddTransient<EngineMapper>(provider => key => key switch
{
    SearchEngineSettings.Google => provider.GetRequiredService<GoogleEngine>(),
    SearchEngineSettings.Bing => provider.GetRequiredService<BingEngine>(),
    _ => throw new NotSupportedException()
});

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseResponseCaching();
app.UseAuthorization();

app.MapControllers();

app.Run();
