using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProEventos.API.Helpers;
using ProEventos.Application;
using ProEventos.Application.Contratos;
using ProEventos.Domain.Identity;
using ProEventos.Persistence;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ProEventosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ctx"),
    b => b.MigrationsAssembly("ProEventos.Persistence")));

builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
})
           .AddRoles<Role>()
           .AddRoleManager<RoleManager<Role>>()
           .AddSignInManager<SignInManager<User>>()
           .AddRoleValidator<RoleValidator<Role>>()
           .AddEntityFrameworkStores<ProEventosContext>()
           .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

builder.Services.AddControllers()
        .AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
        )
        .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<ILoteService, LoteService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPalestranteService, PalestranteService>();
builder.Services.AddScoped<IRedeSocialService, RedeSocialService>();
builder.Services.AddScoped<IUtil, Util>();

builder.Services.AddScoped<IGeralPersist, GeralPersist>();
builder.Services.AddScoped<IEventoPersist, EventoPersist>();
builder.Services.AddScoped<ILotePersist, LotePersist>();
builder.Services.AddScoped<IUserPersist, UserPersist>();
builder.Services.AddScoped<IPalestrantePersist, PalestrantePersist>();
builder.Services.AddScoped<IRedeSocialPersist, RedeSocialPersist>();

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapControllers();

app.Run();
