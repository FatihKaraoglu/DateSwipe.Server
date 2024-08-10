using Blazored.LocalStorage;
using DateSwipe.Client.SignalR;
using DateSwipe.Client;
using DateSwipe.Client.AuthMessageHandler;
using DateSwipe.Client.Services.AuthService;
using DateSwipe.Client.Services.DateDecisionService;
using DateSwipe.Client.Services.InvitationService;
using DateSwipe.Client.Services.ProfileService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using DateSwipe.Client.Services.LoadingService;
using DateSwipe.Client.Services.ChatService;
using DateSwipe.Client.Services.PushNotificationService;
using DateSwipe.Client.Services.UserPreferenceService;
using DateSwipe.Client.Services.DateProposalService;
using DateSwipe.Client.Services.PlannedDateService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<ISignalRService, SignalRService>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<IDateDecisionService, DateDecisionService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IInvitationService, InvitationService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddSingleton<LoadingService>();
builder.Services.AddScoped<IPushNotificationService, PushNotificationService>();
builder.Services.AddScoped<IUserPreferenceService, UserPreferenceService>();
builder.Services.AddScoped<IDateProposalService, DateProposalService>();
builder.Services.AddScoped<IPlannedDateService, PlannedDateService>();

builder.Services.AddTransient<AuthMessageHandler>();


builder.Services.AddScoped(sp =>
{
var localStorage = sp.GetRequiredService<ILocalStorageService>();
var navigation = sp.GetRequiredService<NavigationManager>();
var handler = new AuthMessageHandler(localStorage, navigation)
{
    InnerHandler = new HttpClientHandler()
};

return new HttpClient(handler)
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) // Updated to server IP
    };
});

builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

await builder.Build().RunAsync();
