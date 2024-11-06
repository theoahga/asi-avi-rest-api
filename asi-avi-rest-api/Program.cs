using asi_avi_rest_api.Models.DataManager;
using asi_avi_rest_api.Models.Repository;
using Microsoft.EntityFrameworkCore;
using TP2Console.Models.EntityFramework;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7183");
                      });
});

// Add services to the container.
builder.Services.AddScoped<IDataRepository<Utilisateur>, UtilisateurManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NotationDbContext>(options =>
  options.UseNpgsql(builder.Configuration.GetConnectionString("NotationDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
