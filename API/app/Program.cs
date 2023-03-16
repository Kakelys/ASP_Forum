using app.Extensions;
using Nancy.Owin;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositoryService(builder.Configuration);

var app = builder.Build();

app.UseOwin(x => x.UseNancy());
app.Run();