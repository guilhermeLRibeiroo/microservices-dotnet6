using Microsoft.AspNetCore.Authentication;
using Shopping.Web.Services;
using Shopping.Web.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IProductService, ProductService>(
        config => config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceUrls:ProductAPI"))
    );

builder.Services.AddHttpClient<ICartService, CartService>(
        config => config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceUrls:CartAPI"))
    );

//builder.Services.AddHttpClient<ICouponService, CouponService>(
//        config => config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceUrls:CouponAPI"))
//    );

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies", options => options.ExpireTimeSpan = TimeSpan.FromMinutes(10))
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = builder.Configuration.GetValue<string>("ServiceUrls:IdentityServer");
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = "shopping";
        options.ClientSecret = "secret_secret";
        options.ResponseType = "code";

        options.ClaimActions.MapJsonKey("role", "role", "role");
        options.ClaimActions.MapJsonKey("sub", "sub", "sub");

        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";

        options.Scope.Add("shopping");

        options.SaveTokens = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
