using Microsoft.AspNetCore.Identity;
using WineTime.ModelBinders;
using WineTime.Core.Constants;
using WineTime.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDbContexts(builder.Configuration);

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
       {
           options.SignIn.RequireConfirmedAccount = false;
           options.Password.RequireDigit = false;
           options.Password.RequireLowercase = false;
           options.Password.RequireNonAlphanumeric = false;
           options.Password.RequireUppercase = false;
        })
       .AddRoles<IdentityRole>()
       .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
        options.ModelBinderProviders.Insert(1, new DateTimeModelBinderProvider(FormatingConstant.NormalDateFormat));
        options.ModelBinderProviders.Insert(2, new DoubleModelBinderProvider());
        options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
    });

builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
