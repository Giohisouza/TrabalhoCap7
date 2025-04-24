using Gestao.Residuos.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Residuos.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<CaminhaoModel> Caminhoes { get; set; }
        public virtual DbSet<RotaModel> Rotas { get; set; }
        public virtual DbSet<AgendamentoColetaModel> AgendamentosColetas { get; set; }
        public virtual DbSet<ResiduoModel> Residuos { get; set; }
        public virtual DbSet<MoradorModel> Moradores { get; set; }
        public virtual DbSet<AuthModel> Auth { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração para Caminhao
            modelBuilder.Entity<CaminhaoModel>(entity =>
            {
                entity.ToTable("CAMINHOES");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Placa).IsRequired().HasColumnType("VARCHAR2(20)");
                entity.Property(e => e.Modelo).IsRequired().HasColumnType("VARCHAR2(50)");
                entity.Property(e => e.CapacidadeMaxima).HasColumnType("NUMBER(10,2)");
            });

            // Configuração para Rota
            modelBuilder.Entity<RotaModel>(entity =>
            {
                entity.ToTable("ROTAS");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Caminhao)
                      .WithMany()
                      .HasForeignKey(e => e.CaminhaoId);
                entity.Property(e => e.DataHoraInicio).HasColumnType("DATE");
                entity.Property(e => e.DataHoraFim).HasColumnType("DATE");
            });

            // Configuração para AgendamentoColeta
            modelBuilder.Entity<AgendamentoColetaModel>(entity =>
            {
                entity.ToTable("AGENDAMENTOS_COLETAS");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Morador)
                      .WithMany()
                      .HasForeignKey(e => e.MoradorId);
                entity.Property(e => e.DataColeta).HasColumnType("DATE");
                entity.Property(e => e.TipoResiduo).IsRequired().HasColumnType("VARCHAR2(50)");
                entity.Property(e => e.CapacidadeAtualRecipiente).HasColumnType("NUMBER(10,2)");
            });

            // Configuração para Residuo
            modelBuilder.Entity<ResiduoModel>(entity =>
            {
                entity.ToTable("RESIDUOS");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Tipo).IsRequired().HasColumnType("VARCHAR2(50)");
                entity.Property(e => e.InstrucoesDescarte).HasColumnType("VARCHAR2(500)");
            });

            // Configuração para Morador
            modelBuilder.Entity<MoradorModel>(entity =>
            {
                entity.ToTable("MORADORES");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasColumnType("VARCHAR2(100)");
                entity.Property(e => e.Endereco).HasColumnType("VARCHAR2(200)");
                entity.Property(e => e.Email).IsRequired().HasColumnType("VARCHAR2(100)");
                entity.Property(e => e.Telefone).HasColumnType("VARCHAR2(15)");
            });

            modelBuilder.Entity<AuthModel>(entity =>
            {
                entity.ToTable("AuthUser");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.User).IsRequired().HasColumnType("VARCHAR2(100)");
                entity.Property(e => e.Password).IsRequired().HasColumnType("VARCHAR2(100)");
                entity.Property(e => e.Role).IsRequired().HasColumnType("VARCHAR2(100)");
            });
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }
    }
}
