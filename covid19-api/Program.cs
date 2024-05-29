global using Newtonsoft.Json;
global using covid19_api.Services;
global using covid19_api.Data;
global using covid19_api.Models;
using Microsoft.EntityFrameworkCore;
using covid19_api.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;
using covid19_api.Services.Role;
using Microsoft.AspNetCore.Authorization;
using covid19_api.Services.UserClaims;
using covid19_api.Services.Permission;
using covid19_api.Services.JwtToken;
using covid19_api.Services.Covid19Data;
using covid19_api.Providers;
using covid19_api.Handlers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = """Standard authorization header using Bearer schema. Example: "bearer {token}" """,
        In = ParameterLocation.Header,
        Name= "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICovid19DataService, Covid19DataService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJWTTokenService, JWTTokenService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();

builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
builder.Services.AddScoped<IUserClaimsService, UserClaimsService>();


//Register Custom Authorize Attribute
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
           
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection("Appsettings:Token").Value!)),
            ValidateLifetime = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew=TimeSpan.Zero
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//Test Middleware Implementation
app.Use(async (ctx, next) => {
    app.Logger.LogInformation($"Middleware Calles.... requestPath : {ctx.Request.Path}");
        await next.Invoke(ctx);
});

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{area=Home}/{controller=Home}/{action=Index}/{id?}");

app.Run();
