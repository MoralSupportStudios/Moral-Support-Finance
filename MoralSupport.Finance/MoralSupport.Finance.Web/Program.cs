using Microsoft.EntityFrameworkCore;
using MoralSupport.Finance.Domain.Entities;
using MoralSupport.Finance.Infrastructure.Persistence;
using MoralSupport.Finance.Web.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICurrentUserService, StubCurrentUserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Only seed if empty
    if (!db.Organizations.Any())
    {
        var org = new Organization { Name = "Test Org" };
        db.Organizations.Add(org);

        var user = new User {Name = "Joey", Email = "Joey@FakeEmail.com" };
        db.Users.Add(user);

        db.SaveChanges();
    }
}

app.Run();
