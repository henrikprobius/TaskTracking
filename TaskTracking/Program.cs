
using TaskTracking.Database;

using TaskTrackingService.Datastore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//hur läsa från Secrets.json??
//var movieApiKey = builder.Configuration["LocalSQLExpress"];
//https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows&viewFallbackFrom=aspnetcore-2.2
//builder.Services.AddDbContext<TaskTracking.Database.DatabaseContext>(dbContextOptions => dbContextOptions.UseSqlServer(@"Server=localhost\sqlexpress;Database=TaskTracker;Trusted_Connection=True;MultipleActiveResultSets=true"));
builder.Services.AddScoped<IDatastore, Datastore>();// scoped is once per request
builder.Services.AddScoped<DatabaseContext>();// scoped is once per request

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
