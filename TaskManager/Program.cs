using NLog.Web;
using TaskManager.Abstractions.Services;
using TaskManager.DataAccess.Extensions;
using TaskManager.Identity.Extensions;
using TaskManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseNLog();
builder.Services.AddLogging();

builder.Services.AddControllersWithViews();
builder.Services.AddIdentity(builder.Configuration);

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Task/Error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();
