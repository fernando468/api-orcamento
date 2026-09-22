using System.Text.Json.Serialization;
using Backend.Contexts;
using Backend.Exceptions;
using Backend.Interfaces;
using Backend.Interfaces.Repostiories;
using Backend.Interfaces.Services;
using Backend.Repostiories;
using Backend.Services;
using Backend.Services.StateStatus;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("LocalConnection");

// builder.Services.AddDbContext<AppDbContext>(opts =>
//     opts.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddScoped<IOrcamentoService, OrcamentoService>();
builder.Services.AddScoped<IOrcamentoRepository, OrcamentoRepository>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddTransient<AguardandoAvaliacaoState>();
builder.Services.AddTransient<AvaliandoState>();
builder.Services.AddTransient<CanceladoState>();
builder.Services.AddTransient<ConcluidoState>();
builder.Services.AddTransient<State>();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Converte enums para string nas requisições/respostas JSON
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
