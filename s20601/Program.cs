using Azure;
using CurrieTechnologies.Razor.Clipboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using MudBlazor;
using MudBlazor.Services;
using s20601.Components;
using s20601.Components.Account;
using s20601.Components.Account.Policies;
using s20601.Data;
using s20601.Data.Models;
using s20601.Hubs;
using s20601.Services;
using s20601.Services.External.Azure;
using s20601.Services.External.TMDB;

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

builder.Services.AddAzureClients(azureBuilder =>
{
    azureBuilder.AddBlobServiceClient(builder.Configuration.GetSection("Azure")
        .GetConnectionString("BlobStorage") ??
        throw new InvalidOperationException(
                "Connection string 'BlobStorage' not found."));

    var languageEndpoint = builder.Configuration["Azure:Endpoints:LanguageServices"];
    var languageKey = builder.Configuration["Azure:Keys:LanguageServicesKey"];

    if (string.IsNullOrEmpty(languageEndpoint) || string.IsNullOrEmpty(languageKey))
    {
        throw new InvalidOperationException("Azure Text Analytics Endpoint or Key is missing.");
    }

    azureBuilder.AddTextAnalyticsClient(new Uri(languageEndpoint), new AzureKeyCredential(languageKey));
});

builder.Services.AddScoped<IAzureBlobService, AzureBlobService>();

builder.Services
    .AddCascadingAuthenticationState();

builder.Services
    .AddScoped<IdentityUserAccessor>();
builder.Services
    .AddScoped<IdentityRedirectManager>();
builder.Services
    .AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddSingleton<IAuthorizationHandler, MinimumPointsHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ReviewFrequencyHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PointsAtLeast1", policy =>
        policy.Requirements.Add(new MinimumPointsRequirement(1)));
    
    options.AddPolicy("PointsAtLeast21", policy =>
        policy.Requirements.Add(new MinimumPointsRequirement(21)));
    
    options.AddPolicy("CanPostReview", policy =>
    {
        policy.Requirements.Add(new MinimumPointsRequirement(1));
        policy.Requirements.Add(new ReviewFrequencyRequirement(1, 21, TimeSpan.FromHours(24)));
    });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentityCore<ApplicationUser>()
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

builder.Services.AddClipboard();

builder.Services.AddHttpClient<ITmdbLibClient, TmdbLibClient>(client =>
{
    client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        [ "application/octet-stream" ]);
});

builder.Services
    .AddScoped<IAzureSentimentAnalysisService, AzureSentimentAnalysisService>();

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