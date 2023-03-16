using app.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositoryService(builder.Configuration);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.Run();