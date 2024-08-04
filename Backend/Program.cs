using BackendRestAPI;
using BackendRestAPI.Data;
using BackendRestAPI.Middleware;
using BackendRestAPI.Repositories.Contacts;
using BackendRestAPI.Repositories.Dictionaries;
using BackendRestAPI.Repositories.Users;
using BackendRestAPI.Services.Contacts;
using BackendRestAPI.Services.Contacts.ContactsRequestMapper;
using BackendRestAPI.Services.Dictionaries;
using BackendRestAPI.Services.Dictionaries.DictionaryRequestMapper;
using BackendRestAPI.Services.Token;
using BackendRestAPI.Services.Users;
using BackendRestAPI.Services.Users.UserRequestMapper;
using BackendRestAPI.Services.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// add db context
builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var serverVersion = new MySqlServerVersion(new Version(9, 0, 1));
    options.UseMySql(connectionString, serverVersion);
});

// add services
builder.Services.AddScoped<IContactsService, ContactsService>();
builder.Services.AddScoped<IDictionariesService, DictionariesService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<ITokenService, TokenService>();

// add repositories
builder.Services.AddScoped<IContactsRepository, ContactsRepository>();
builder.Services.AddScoped<IDictionaryRepository, DictionaryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// add mappers
builder.Services.AddScoped<IContactsRequestMapper, ContactsRequestMapper>();
builder.Services.AddSingleton<IUserRequestMapper, UserRequestMapper>();
builder.Services.AddSingleton<IDictionaryRequestMapper, DictionaryRequestMapper>();

// add validators
builder.Services.AddScoped<IStringValidator, DefaultStringValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add jwt authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
    options.Events = new JwtBearerEvents
    {
        // extract token from cookie
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey("token"))
            {
                context.Token = context.Request.Cookies["token"]; 
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddCors();

var app = builder.Build();

app.UseExceptionHandler("/errors");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options =>
    options
    .WithOrigins(new[] {builder.Configuration.GetConnectionString("Frontend")}) 
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<SlidingExpirationMiddleware>();
app.MapControllers();

app.Run();
