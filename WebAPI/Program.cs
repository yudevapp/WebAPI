using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using WebAPI.Contracts;
using WebAPI.Data;
using WebAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebAPI.Handlers;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add Database
var connectionString = builder.Configuration.GetConnectionString("HotelListingDbContext");
builder.Services.AddDbContext<HotelListingDbContext>(options => options.UseSqlServer(connectionString));

// Add IndentityCore
builder.Services.AddIdentityApiEndpoints <ApplicationUser>()
    .AddEntityFrameworkStores<HotelListingDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; })
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["JWTSettings.Issuer"],
           ValidAudience = builder.Configuration["JWTSettings.Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Key"])),
           ClockSkew = TimeSpan.Zero

       };
   })
   .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(AuthenticationDefaults.BasicScheme, _ => { })
   .AddScheme<AuthenticationSchemeOptions, ApiAuthenticationHandler>(AuthenticationDefaults.ApiKeyScheme, _ => { });
builder.Services.AddAuthorization();

// Add services to the IoC container.
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IHotelsService, HotelsService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IApiKeyValidatorService, ApiKeyValidatorService>();

// Add Controller
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<ApiKeyValidatorService>();

var app = builder.Build();
app.MapIdentityApi<ApplicationUser>();

app.MapGroup("/api/defaultauth").MapIdentityApi<ApplicationUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
