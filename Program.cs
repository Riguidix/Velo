using Microsoft.EntityFrameworkCore;

using Velo.Models;
using Velo.Services;
using Velo.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<VeloDbContext>(options => options.UseInMemoryDatabase("VeloDB"));

builder.Services.AddScoped<IPDFService, PdfService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();