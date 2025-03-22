using s20601.Components;
using MudBlazor.Services;
using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<S20601Context>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default") ??
        throw new InvalidOperationException(
            "Connection string 'Default' not found.")));

// Add MudBlazor services
builder.Services
    .AddMudServices();

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddScoped<IMovieService, MovieService>();

builder.Services
    .AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
