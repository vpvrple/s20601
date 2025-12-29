using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;
using s20601.Components;
using s20601.Components.Account;
using s20601.Data;
using s20601.Data.Models;
using s20601.Hubs;
using s20601.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddMediatR(cfg => {
    // Scan project to find Handlers
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found.")));

builder.Services
    .AddCascadingAuthenticationState();
builder.Services
    .AddScoped<IdentityUserAccessor>();
builder.Services
    .AddScoped<IdentityRedirectManager>();
builder.Services
    .AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services
    .AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services
    .AddMudServices(config =>
    {
        config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

        config.SnackbarConfiguration.PreventDuplicates = false;
        config.SnackbarConfiguration.NewestOnTop = false;
        config.SnackbarConfiguration.ShowCloseIcon = true;
        config.SnackbarConfiguration.VisibleStateDuration = 10000;
        config.SnackbarConfiguration.HideTransitionDuration = 500;
        config.SnackbarConfiguration.ShowTransitionDuration = 500;
        config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
    });

builder.Services
    .AddScoped<IMovieService, MovieService>();

builder.Services
    .AddScoped<IUserService, UserService>();

builder.Services
    .AddScoped<IRatingService, RatingService>();

builder.Services
    .AddScoped<IReviewService, ReviewService>();

builder.Services
    .AddScoped<IRankingService, RankingService>();

builder.Services
    .AddScoped<IMovieCollectionService, MovieCollectionService>();

builder.Services
    .AddScoped<ISearchService, SearchService>();

builder.Services
    .AddScoped<IFriendService, FriendService>();

builder.Services
    .AddScoped<IChatService, ChatService>();

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        [ "application/octet-stream" ]);
});

var app = builder.Build();

app.UseResponseCompression();
app.MapHub<ChatHub>("/chathub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
