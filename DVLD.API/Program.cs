using DVLD.Application.Interfaces;
using DVLD.Application.Services;
using DVLD.Infrastructure.Data;
using DVLD.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =========================
// Services
// =========================

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =========================
// Database
// =========================

builder.Services.AddDbContext<DvldDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// =========================
// Dependency Injection
// =========================

builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
builder.Services.AddScoped<IPeopleService, PeopleService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUserService, UsersService>();
builder.Services.AddScoped<IDriversRepository, DriversRepository>();
builder.Services.AddScoped<IDriverService, DriversService>();
builder.Services.AddScoped<
    ICountriesRepository,
    CountriesRepository>();
builder.Services.AddScoped<
    ICountryService,
    CountriesService>();
builder.Services.AddScoped<
    IDetainedLicensesRepository,
    DetainedLicensesRepository>();

builder.Services.AddScoped<
    IDetainedLicenseService,
    DetainedLicensesService>();
builder.Services.AddScoped<
    IApplicationTypesRepository,
    ApplicationTypesRepository>();

builder.Services.AddScoped<
    IApplicationTypeService,
    ApplicationTypesService>();
builder.Services.AddScoped<
    IDvldApplicationsRepository,
    DvldApplicationsRepository>();

builder.Services.AddScoped<
    IDvldApplicationService,
    DvldApplicationsService>();
// =========================
// CORS
// =========================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// =========================
// HTTP Pipeline
// =========================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "DVLD API"
        );
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();