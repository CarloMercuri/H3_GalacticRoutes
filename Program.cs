using Galactic.Data.Processing;
using Galactic.Data.Encryption;
using Galactic.Data.Interfaces;
using Galactic.Data.Processing;
using Galactic.Processing;
using Galactic.Processing.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injections
builder.Services.AddSingleton<ITokenEncryption, Encryptor>();
builder.Services.AddSingleton<ITokenDataFetching, CsvDataFetcher>();
builder.Services.AddSingleton<ITokenProcessor, TokenProcessor>();
builder.Services.AddSingleton<IRouteFetcher, FileRouteFetcher>();
builder.Services.AddSingleton<ISecureRequestsProcessor, AdminRequestProcessor>();
builder.Services.AddSingleton<IPublicRequestsProcessor, PublicRequestsProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
