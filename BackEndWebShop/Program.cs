using Microsoft.EntityFrameworkCore;
using BackEndWebShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BackEndWebShop.Repository;
using BackEndWebShop.Helper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    /*options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = true;*/
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<BookShopContext>()
.AddDefaultTokenProviders();


builder.Services.AddDbContext<BookShopContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyStore")));


//Setting auto mapper
builder.Services.AddAutoMapper(typeof(Program));

//Setting sessions
builder.Services.AddMvc()
        .AddSessionStateTempDataProvider();
builder.Services.AddSession();

//Đăng ký repository
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

//Setting Authentication JWT
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

// Cấu hình phân quyền
builder.Services.AddAuthorization(options =>
{
    
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
/*app.UseMvcWithDefaultRoute();
*/app.MapControllers();

app.Run();
