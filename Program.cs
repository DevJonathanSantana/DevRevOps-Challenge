using REVOPS.DevChallenge;
using REVOPS.DevChallenge.Clients;
using REVOPS.DevChallenge.Components;
using REVOPS.DevChallenge.Context;
using REVOPS.DevChallenge.Services; 
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers; 

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
builder.Services.AddControllers();


builder.Services.AddDbContext<ChallengeContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")!);
    options.EnableSensitiveDataLogging();
});




var talkToken = builder.Configuration["AppSettings:Talk2ApiToken"];


builder.Services.AddHttpClient<ITalkClient, TalkClient>(client =>
{
    
    client.BaseAddress = new Uri("https://app-utalk.umbler.com/api/"); 
    
    
    if (!string.IsNullOrEmpty(talkToken))
    {
        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", talkToken);
    }


    client.DefaultRequestHeaders.Add("OrganizationId", "YT-h9PtKxKdBtkrR");
});


builder.Services.AddScoped<IChatService, ChatService>();

// -------------------------------------

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

app.Run();