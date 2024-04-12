using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shield_Tech.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    // Here we ensure the /Admin area is protected and requires an Admin role.
    options.Conventions.AuthorizeFolder("/Admin", "Admin");
});

builder.Services.AddSession();

builder.Services.AddDbContext<AppDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDataContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// Initialize the database and create the default admin user and role
CreateAdminUserAndRole(app.Services).Wait();

app.Run();

// Method to create a default admin user and role
static async Task CreateAdminUserAndRole(IServiceProvider serviceProvider)
{
    using (var scope = serviceProvider.CreateScope())
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Ensure the Admin role exists
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // Check if the admin user exists
        var adminUser = await userManager.FindByEmailAsync("admin@shieldtech.com");
        if (adminUser == null)
        {
            // Create a new admin user
            adminUser = new AppUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
            };
            await userManager.CreateAsync(adminUser, "AdminPassword123!");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
