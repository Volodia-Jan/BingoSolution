using BingoAPI.Middleware;
using BingoAPI.StartupExtentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();
app.UseExceptionHandlerMiddleware();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
