using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TreeStructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(o => 
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=TreeStructure;Trusted_Connection=true");
});

var app = builder.Build();

// Configure the HTTP request pipeline.

await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetService<AppDbContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedFolderTree(context);
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
