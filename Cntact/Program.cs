using Cntact.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<ContactsDbContext>();

var app = builder.Build();

using var scope = app.Services.CreateScope();

await using var dbContext = scope.ServiceProvider.GetRequiredService<ContactsDbContext>();
await dbContext.Database.EnsureCreatedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
