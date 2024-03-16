using Assignment3.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<ReaderRepository>();
builder.Services.AddScoped<BorrowingRepository>();

var app = builder.Build();


app.MapControllers();

//app.MapGet("/", () => "Hello World!");

app.Run();
