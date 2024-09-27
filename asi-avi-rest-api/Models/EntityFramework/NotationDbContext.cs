using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace TP2Console.Models.EntityFramework;

public partial class NotationDbContext : DbContext
{
    public NotationDbContext()
    {
    }

    public NotationDbContext(DbContextOptions<NotationDbContext> options)
    : base(options)
    {
    }

    public virtual DbSet<Notation> Notations { get; set; }

    public virtual DbSet<Serie> Series { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseLoggerFactory(MyLoggerFactory)
                            .EnableSensitiveDataLogging();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        modelBuilder.Entity<Notation>(entity =>
        {
            entity.HasOne(d => d.SerieNotee).WithMany(p => p.NotesSerie)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_notation_serie");

            entity.HasOne(d => d.UtilisateurNotant).WithMany(p => p.NotesUtilisateur)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_notations_utilisateur");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        { 
            entity.HasIndex(u => u.Mail).IsUnique();
            entity.Property(u => u.Pays).HasDefaultValue("France");
            entity.Property(u => u.DateCreation).HasDefaultValueSql("now()");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}