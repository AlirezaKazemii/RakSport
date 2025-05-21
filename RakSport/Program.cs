using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RakSport.Application.Commands.Auth.SendOtp;
using RakSport.Application.Commands.Auth.VerifyOtp;
using RakSport.Application.Interfaces.Auth;
using RakSport.Application.Interfaces.UserPanel;
using RakSport.Application.Mapper.User;
using RakSport.Application.Services.Auth;
using RakSport.Application.Services.UserPanel;
using RakSport.Domain.Interfaces.Auth.OtpCodeInterface;
using RakSport.Domain.Interfaces.Auth.RefreshToken;
using RakSport.Domain.Interfaces.GenericInterface;
using RakSport.Domain.Interfaces.UnitOfWorkInterface;
using RakSport.Domain.Interfaces.User.Reservation;
using RakSport.Domain.Interfaces.UserPanel.ReservationItem;
using RakSport.Domain.Interfaces.UserPanel.Ticket;
using RakSport.Domain.Models.User;
using RakSport.Infrastructure.DbContext;
using RakSport.Infrastructure.jwtsettings;
using RakSport.Infrastructure.Repositories.Auth.otpcodeRepository;
using RakSport.Infrastructure.Repositories.Auth.refreshtokenRepository;
using RakSport.Infrastructure.Repositories.Genericrepository;
using RakSport.Infrastructure.Repositories.UserPanel.reservationItemRepository;
using RakSport.Infrastructure.Repositories.UserPanel.reservationRepository;
using RakSport.Infrastructure.Repositories.UserPanel.Ticket;
using RakSport.Infrastructure.Repositories.UserPanel.timeSlotRepostiory;
using RakSport.Infrastructure.Repositories.UserPanel.TimeSlotRepostiory;
using RakSport.Infrastructure.Seeder;
using RakSport.Infrastructure.Smsserviceoptions;
using RakSport.Infrastructure.Unitofwork;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Database Context
builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RakSport")));

// Identity Configuration
builder.Services.AddIdentity<UserEntity, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<DataBaseContext>()
.AddDefaultTokenProviders();

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<SendOtpCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<VerifyOtpCommandValidator>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(UserAutoMapper));

// Application Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMeliPayamak, MeliPayamakService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITicketService, TicketService>();
//builder.Services.AddSingleton(new MeliPayamakService("USERNAME", "PASSWORD"));

// Repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IOtpCodeRepository, OtpCodeRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationItemRepository, ReservationItemRepository>();
builder.Services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Seed Roles 
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
    await IdentitySeeder.SeedInformations(userManager, roleManager);
}

// sms 
builder.Services.Configure<SmsServiceOptions>(builder.Configuration.GetSection("MeliPayamk"));

// JWT Authentication
var jwtSettingsSection = builder.Configuration.GetSection("Jwt"); 
builder.Services.Configure<JwtSettings>(jwtSettingsSection);

var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RakSport",
        Version = "v1"
    });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Please Just Enter Token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    };

    options.AddSecurityDefinition("Bearer", securityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Build & Run
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
