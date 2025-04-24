using Gestao.Residuos.Data.Repository;
using Gestao.Residuos.Data.Contexts;
using GestaoResiduos.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Gestao.Residuos.Services;
using Gestao.Residuos.Models;
using AutoMapper;
using Gestao.Residuos.ViewModel.Caminhao;
using Gestao.Residuos.ViewModel.Morador;
using Gestao.Residuos.ViewModel.AgendamentoColeta;
using Gestao.Residuos.ViewModel.Residuo;
using Gestao.Residuos.ViewModel.Rota;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Gestao.Residuos.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region INICIALIZANDO O BANCO DE DADOS
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);
#endregion

#region Repositorios
builder.Services.AddScoped<IAgendamentoColetaRepository, AgendamentoColetaRepository>();
builder.Services.AddScoped<ICaminhaoRepository, CaminhaoRepository>();
builder.Services.AddScoped<IMoradorRepository, MoradorRepository>();
builder.Services.AddScoped<IResiduoRepository, ResiduoRepository>();
builder.Services.AddScoped<IRotaRepository, RotaRepository>();
builder.Services.AddScoped<IAuthUsersRepository, AuthUsersRepository>();

#endregion

#region Services
builder.Services.AddScoped<IAgendamentoColetaService, AgendamentoColetaService>();
builder.Services.AddScoped<ICaminhaoService, CaminhaoService>();
builder.Services.AddScoped<IMoradorService, MoradorService>();
builder.Services.AddScoped<IResiduoService, ResiduoService>();
builder.Services.AddScoped<IRotaService, RotaService>();
builder.Services.AddScoped<IAuthUsersService, AuthUsersService>();
#endregion

#region AutoMapper
var mapperConfig = new AutoMapper.MapperConfiguration(c => {
    // Permite que coleções nulas sejam mapeadas
    c.AllowNullCollections = true;
    // Permite que valores de destino nulos sejam mapeados
    c.AllowNullDestinationValues = true;

    //Caminhao
    c.CreateMap<CaminhaoModel, CaminhaoViewModel>();

    c.CreateMap<CaminhaoViewModel, CaminhaoModel>();
    c.CreateMap<CaminhaoUpdateViewModel, CaminhaoModel>();
    c.CreateMap<CaminhaoCreateViewModel, CaminhaoModel>();

    //Morador
    c.CreateMap<MoradorModel, MoradorViewModel>();

    c.CreateMap<MoradorViewModel, MoradorModel>();
    c.CreateMap<MoradorUpdateViewModel, MoradorModel>();
    c.CreateMap<MoradorCreateViewModel, MoradorModel>();

    //AgendamentoColeta
    c.CreateMap<AgendamentoColetaModel, AgendamentoColetaViewModel>();

    c.CreateMap<AgendamentoColetaViewModel, AgendamentoColetaModel>();
    c.CreateMap<AgendamentoUpdateColetaViewModel, AgendamentoColetaModel>();
    c.CreateMap<AgendamentoCreateColetaViewModel, AgendamentoColetaModel>();

    //Residuo
    c.CreateMap<ResiduoModel, ResiduoViewModel>();

    c.CreateMap<ResiduoViewModel, ResiduoModel>();
    c.CreateMap<ResiduoUpdateViewModel, ResiduoModel>();
    c.CreateMap<ResiduoCreateViewModel, ResiduoModel>();

    //Morador
    c.CreateMap<RotaModel, RotaViewModel>();

    c.CreateMap<RotaViewModel, RotaModel>();
    c.CreateMap<RotaUpdateViewModel, RotaModel>();
    c.CreateMap<RotaCreateViewModel, RotaModel>();

});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion


#region Auth
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.MapControllers();

app.Run();
