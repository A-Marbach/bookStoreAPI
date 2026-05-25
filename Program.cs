// <snippet_UsingModels>
using Prometheus;
using BookStoreApi.Models;
// </snippet_UsingModels>
// <snippet_UsingServices>
using BookStoreApi.Services;
// </snippet_UsingServices>

// <snippet_AddControllers>
// <snippet_BooksService>
// <snippet_BookStoreDatabaseSettings>
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<BookStoreDatabaseSettings>(
    builder.Configuration.GetSection("BookStoreDatabase"));
// </snippet_BookStoreDatabaseSettings>

builder.Services.AddSingleton<BooksService>();
// </snippet_BooksService>

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// </snippet_AddControllers>

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapHealthChecks("/health");

app.UseAuthorization();

app.UseMetricServer();

app.MapControllers();

app.Run();
