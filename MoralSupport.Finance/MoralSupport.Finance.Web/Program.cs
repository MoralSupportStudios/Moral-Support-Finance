using Microsoft.EntityFrameworkCore;
using MoralSupport.Authentication.Application.Interfaces;
using MoralSupport.Authentication.Infrastructure.Auth;
using MoralSupport.Authentication.Infrastructure.Persistence;
using MoralSupport.Finance.Infrastructure.Persistence;
using MoralSupport.Finance.Web.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication("MyCookieSchema")
    .AddCookie("MyCookieSchema", options =>
    {
        options.LoginPath = "/signin";
        options.LogoutPath = "/signout";
    });
builder.Services.AddDbContext<AuthenticationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnection")));
builder.Services.AddScoped<IAuthService, GoogleAuthService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICurrentUserService, StubCurrentUserService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

//    // Only seed if empty
//    if (!db.Organizations.Any())
//    {
//        var org = new Organization { Name = "Test Org" };
//        db.Organizations.Add(org);

//        var user = new User {Name = "Joey", Email = "Joey@FakeEmail.com" };
//        db.Users.Add(user);

//        db.SaveChanges();
//    }
//}

app.Run();
