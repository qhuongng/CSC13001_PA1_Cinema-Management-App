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

    public virtual DbSet<MovieInfo> MovieInfos { get; set; }

    public virtual DbSet<Projector> Projectors { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SAMMM/NTDSAMMM;Initial Catalog=cinemaManagement;User ID=sa;Password=123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK__Actor__8332510B58D301B9");

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
            entity.HasKey(e => e.AgeId).HasName("PK__AgeRatin__D68D4907F2C8C098");

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
            entity.HasKey(e => e.DirectorId).HasName("PK__Director__418D834E3FD52A80");

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
            entity.HasKey(e => e.GenreId).HasName("PK__Genre__3C54768254FF8C96");

            entity.ToTable("Genre");

            entity.Property(e => e.GenreId).HasColumnName("genreId");
            entity.Property(e => e.GenreName)
                .HasMaxLength(30)
                .HasColumnName("genreName");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movie__42EB374E9A1F97E6");

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
                .HasConstraintName("FK__Movie__certifica__45F365D3");

            entity.HasOne(d => d.Director).WithMany(p => p.Movies)
                .HasForeignKey(d => d.DirectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movie__directorI__44FF419A");

            entity.HasMany(d => d.Actors).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieActor",
                    r => r.HasOne<Actor>().WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MovieActo__actor__4D94879B"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MovieActo__movie__4CA06362"),
                    j =>
                    {
                        j.HasKey("MovieId", "ActorId").HasName("PK__MovieAct__EAD8125E14CA9CE4");
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
                        .HasConstraintName("FK__MovieGenr__genre__49C3F6B7"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MovieGenr__movie__48CFD27E"),
                    j =>
                    {
                        j.HasKey("MovieId", "GenreId").HasName("PK__MovieGen__712E702605475C59");
                        j.ToTable("MovieGenres");
                        j.IndexerProperty<int>("MovieId").HasColumnName("movieId");
                        j.IndexerProperty<int>("GenreId").HasColumnName("genreId");
                    });
        });

        modelBuilder.Entity<MovieInfo>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__MovieInf__42EB374E577B3823");

            entity.Property(e => e.MovieId)
                .ValueGeneratedNever()
                .HasColumnName("movieId");
            entity.Property(e => e.DailyShowtime).HasColumnName("dailyShowtime");
            entity.Property(e => e.IsSelling).HasColumnName("isSelling");
            entity.Property(e => e.MonthlyShowtime).HasColumnName("monthlyShowtime");
            entity.Property(e => e.SoldTicket).HasColumnName("soldTicket");
            entity.Property(e => e.TicketRevenue).HasColumnName("ticketRevenue");
            entity.Property(e => e.WeeklyShowtime).HasColumnName("weeklyShowtime");

            entity.HasOne(d => d.Movie).WithOne(p => p.MovieInfo)
                .HasForeignKey<MovieInfo>(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MovieInfo__movie__6FE99F9F");
        });

        modelBuilder.Entity<Projector>(entity =>
        {
            entity.HasKey(e => e.ProjectorId).HasName("PK__Projecto__836CBC8084051EEC");

            entity.ToTable("Projector");

            entity.Property(e => e.ProjectorId).HasColumnName("projectorId");
            entity.Property(e => e.ProjectorInfo)
                .HasColumnType("datetime")
                .HasColumnName("projectorInfo");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__3333C610B4E8E13F");

            entity.ToTable("Ticket");

            entity.Property(e => e.TicketId).HasColumnName("ticketId");
            entity.Property(e => e.MovieId).HasColumnName("movieId");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProjectorId).HasColumnName("projectorId");
            entity.Property(e => e.SeatId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("seatId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Movie).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ticket__movieId__52593CB8");

            entity.HasOne(d => d.Projector).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ProjectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ticket__projecto__534D60F1");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ticket__userId__5165187F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__CB9A1CFFCA2C0792");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");
            entity.Property(e => e.Password)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__Voucher__F53389E96B8EE827");

            entity.ToTable("Voucher");

            entity.Property(e => e.VoucherId).HasColumnName("voucherId");
            entity.Property(e => e.DiscountPercent).HasColumnName("discountPercent");
            entity.Property(e => e.IsUsed).HasColumnName("isUsed");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Voucher__userId__2EDAF651");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
