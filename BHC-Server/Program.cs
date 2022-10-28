using BHC_Server.Models;
using EmailSend.Core.HostServices;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddCors(p => p.AddPolicy("cors", build =>
{
    build.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddDbContext<DB_BHCContext>(Option =>
{
    Option.UseSqlServer(builder.Configuration.GetConnectionString("DBCon"));
   
});


builder.Services.AddSingleton<EmailHostedService>();
builder.Services.AddHostedService(provider => provider.GetService<EmailHostedService>());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("cors");

app.UseHttpsRedirection(); 

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
