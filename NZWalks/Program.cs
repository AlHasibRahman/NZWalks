using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Mappings;
using NZWalks.Repository;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Register services
// ----------------------------

builder.Services.AddControllers();

// ✅ Swagger/OpenAPI configuration (standard)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Register EF Core with SQL Server
builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalkConnectionString")));

// ✅ Register repository
builder.Services.AddScoped<IRegionRepository, SQLRegionRepositories>();

// ✅ Register AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperFrofiles));

var app = builder.Build();

// ----------------------------
// Configure middleware pipeline
// ----------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
