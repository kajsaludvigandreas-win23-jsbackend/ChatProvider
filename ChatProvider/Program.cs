using ChatProvider.Data;
using ChatProvider.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddDbContext<DataContext>(x => x.UseInMemoryDatabase("ChatDatabase"));

builder.Services.AddCors(x =>
{
    x.AddPolicy("ChatPolicy", builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.WithOrigins("http://localhost:3000").AllowCredentials();
    });
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("ChatPolicy");

app.MapHub<ChatHub>("/accountmessages");


app.Run();



