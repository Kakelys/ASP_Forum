using app.Extensions;
using app.Interfaces;
using app.Middlewares;
using app.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositoryService(builder.Configuration);
builder.Services.AddAuthService(builder.Configuration);
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddControllers();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

app.UseMiddleware<ResponseExceptionMiddleware>();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoint => {
    endpoint.MapControllers();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();