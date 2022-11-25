
using GameEngine.Classes;
using DataLayer.DTO;
using DataLayer.Interfaces;
using DataLayer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using FrontEnd.Hubs;
using FrontEnd.Classes;
using MtG_Application.Interface;
using MtG_Infra;
using MtG_Application;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<PokerWrapper>();
builder.Services.AddTransient<IMtGCardRepository, SearchForCard>();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<GameHub>("/GameHub");
app.MapHub<ChatRoom>("/ChatRoom");
app.MapHub<PokerRoom>("/PokerRoom");
app.MapFallbackToPage("/_Host");

app.Run();
