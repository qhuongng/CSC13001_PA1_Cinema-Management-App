using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CineManagement.Models;

public partial class CinemaManagementContext : DbContext
{
    public CinemaManagementContext()
    {
    }

    public CinemaManagementContext(DbContextOptions<CinemaManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<AgeRating> AgeRatings { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }


    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ESPRESSO;Initial Catalog=cinemaManagement;Persist Security Info=True;User ID=sa;Password=123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK__Actor__8332510B95A462E4");


            entity.ToTable("Actor");

            entity.Property(e => e.ActorId).HasColumnName("actorId");
            entity.Property(e => e.ActorName)
                .HasMaxLength(50)
                .HasColumnName("actorName");
            entity.Property(e => e.Avatar).HasColumnName("avatar");
            entity.Property(e => e.ShotDes).HasColumnName("shotDes");
        });

        modelBuilder.Entity<AgeRating>(entity =>
        {
            entity.HasKey(e => e.AgeId).HasName("PK__AgeRatin__D68D4907BD7F610F");
            entity.ToTable("AgeRating");

            entity.Property(e => e.AgeId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ageId");
            entity.Property(e => e.AgeDef)
                .HasMaxLength(50)
                .HasColumnName("ageDef");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.DirectorId).HasName("PK__Director__418D834E7EECE0E2");

            entity.ToTable("Director");

            entity.Property(e => e.DirectorId).HasColumnName("directorId");
            entity.Property(e => e.Avatar).HasColumnName("avatar");
            entity.Property(e => e.DirectorName)
                .HasMaxLength(50)
                .HasColumnName("directorName");
            entity.Property(e => e.ShotDes).HasColumnName("shotDes");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genre__3C5476824AEE7F53");

            entity.ToTable("Genre");

            entity.Property(e => e.GenreId).HasColumnName("genreId");
            entity.Property(e => e.GenreName)
                .HasMaxLength(30)
                .HasColumnName("genreName");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movie__42EB374E215B9B3E");
            entity.ToTable("Movie");

            entity.Property(e => e.MovieId).HasColumnName("movieId");
            entity.Property(e => e.Certification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("certification");
            entity.Property(e => e.DirectorId).HasColumnName("directorId");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.ImdbRating).HasColumnName("IMDbRating");
            entity.Property(e => e.MovieName)
                .HasMaxLength(50)
                .HasColumnName("movieName");
            entity.Property(e => e.Poster).HasColumnName("poster");
            entity.Property(e => e.ReleaseYear).HasColumnName("releaseYear");

            entity.HasOne(d => d.CertificationNavigation).WithMany(p => p.Movies)
                .HasForeignKey(d => d.Certification)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movie__certifica__4222D4EF");
            entity.HasOne(d => d.Director).WithMany(p => p.Movies)
                .HasForeignKey(d => d.DirectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movie__directorI__412EB0B6");
            entity.HasMany(d => d.Actors).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieActor",
                    r => r.HasOne<Actor>().WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MovieActo__actor__49C3F6B7"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MovieActo__movie__48CFD27E"),
                    j =>
                    {
                        j.HasKey("MovieId", "ActorId").HasName("PK__MovieAct__EAD8125E2F50A075");
                        j.ToTable("MovieActors");
                        j.IndexerProperty<int>("MovieId").HasColumnName("movieId");
                        j.IndexerProperty<int>("ActorId").HasColumnName("actorId");
                    });

            entity.HasMany(d => d.Genres).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MovieGenr__genre__45F365D3"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MovieGenr__movie__44FF419A"),
                    j =>
                    {
                        j.HasKey("MovieId", "GenreId").HasName("PK__MovieGen__712E7026702A4CC3");
                        j.ToTable("MovieGenres");
                        j.IndexerProperty<int>("MovieId").HasColumnName("movieId");
                        j.IndexerProperty<int>("GenreId").HasColumnName("genreId");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__Users__66DCF95DB171F7BB");

            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userName");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");
            entity.Property(e => e.Password)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("userId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
