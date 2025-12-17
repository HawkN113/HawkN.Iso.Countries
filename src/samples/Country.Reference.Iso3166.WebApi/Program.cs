using System.Text.Json;
using System.Text.Json.Serialization;
using Country.Reference.Iso3166.Extensions;
using Country.Reference.Iso3166.WebApi.Endpoints;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure JSON options for Minimal API
builder.Services.Configure<JsonOptions>(options =>
{
    // This line forces enums to be serialized as strings
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    // Optional: Use camelCase for properties (usually default)
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// ---- Register Currency service ----
builder.Services.AddCountryCodeService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ---- Endpoints ----
app.MapCountryEndpoints();

await app.RunAsync();