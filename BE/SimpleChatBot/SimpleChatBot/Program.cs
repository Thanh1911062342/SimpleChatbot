using Microsoft.EntityFrameworkCore;
using SimpleChatBot.Databases;
using SimpleChatBot.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(c => c.AddDefaultPolicy(option => option.AllowCredentials()
                                                                .AllowAnyHeader()
                                                                .AllowAnyMethod()
                                                                .WithOrigins("http://localhost:4200")
                                                                .SetIsOriginAllowed(_ => true)));

builder.Services.AddDbContext<ChatbotContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("myconn")));

builder.Services.DependencyGroup();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
