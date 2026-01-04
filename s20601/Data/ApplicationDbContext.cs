using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using s20601.Data.Models;

namespace s20601.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public virtual DbSet<ActivityType> ActivityTypes { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<Crew> Crews { get; set; }
    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<Message> Messages { get; set; }
    public virtual DbSet<Movie> Movies { get; set; }
    public virtual DbSet<MovieCollection> MovieCollections { get; set; }
    public virtual DbSet<MovieCollectionMovie> MovieCollectionMovies { get; set; }
    public virtual DbSet<MovieCollectionUser> MovieCollectionUsers { get; set; }
    public virtual DbSet<MovieCrew> MovieCrews { get; set; }
    public virtual DbSet<MovieGenre> MovieGenres { get; set; }
    public virtual DbSet<MovieOfTheDay> MovieOfTheDays { get; set; }
    public virtual DbSet<MovieRate> MovieRates { get; set; }
    public virtual DbSet<MovieUpdateRequest> MovieUpdateRequests { get; set; }
    public virtual DbSet<MovieUpdateRequestCrew> MovieUpdateRequestCrews { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<ReviewRate> ReviewRates { get; set; }
    public virtual DbSet<SocialActivityLog> SocialActivityLogs { get; set; }
    public virtual DbSet<UserRelationship> UserRelationships { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<ActivityType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ActivityType_pk");

            entity.ToTable("ActivityType");

        });

        modelBuilder.Entity<Crew>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Crew_pk");

            entity.ToTable("Crew");

            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Genre_pk");

            entity.ToTable("Genre");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Message_pk");

            entity.ToTable("Message");

            entity.HasIndex(e => e.IdRecipient, "IX_Message_IdRecipient");

            entity.HasIndex(e => e.IdSender, "IX_Message_IdSender");
            
            entity.Property(e => e.Content)
                .HasMaxLength(2500)
                .IsUnicode(false);
            
            entity.HasOne(d => d.IdRecipientNavigation).WithMany(p => p.MessageIdRecipientNavigations)
                .HasForeignKey(d => d.IdRecipient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Message_Recipient");

            entity.HasOne(d => d.IdSenderNavigation).WithMany(p => p.MessageIdSenderNavigations)
                .HasForeignKey(d => d.IdSender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Message_Sender");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Movie_pk");

            entity.ToTable("Movie");

            entity.Property(e => e.OriginalTitle)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.TitleType)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MovieCollection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MovieCollection_pk");

            entity.ToTable("MovieCollection");

            entity.Property(e => e.CreatedAt).HasPrecision(2);
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MovieCollectionMovie>(entity =>
        {
            entity.HasKey(e => e.IdMovieCollection).HasName("MovieCollectionMovie_pk");

            entity.ToTable("MovieCollectionMovie");

            entity.HasIndex(e => e.Movie_Id, "IX_MovieCollectionMovie_Movie_Id");

            entity.Property(e => e.AddedAt).HasPrecision(2);

            entity.HasOne(d => d.IdMovieCollectionNavigation).WithOne(p => p.MovieCollectionMovie)
                .HasForeignKey<MovieCollectionMovie>(d => d.IdMovieCollection)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieCollectionMovie_MovieCollection");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieCollectionMovies)
                .HasForeignKey(d => d.Movie_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieCollectionMovie_Movie");
        });

        modelBuilder.Entity<MovieCollectionUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MovieCollectionUser_pk");

            entity.ToTable("MovieCollectionUser");

            entity.HasIndex(e => e.IdMovieCollection, "IX_MovieCollectionUser_IdMovieCollection");

            entity.HasIndex(e => new { e.IdUser, e.IdMovieCollection }, "MovieCollectionUser_ak_1").IsUnique();

            entity.HasOne(d => d.IdMovieCollectionNavigation).WithMany(p => p.MovieCollectionUsers)
                .HasForeignKey(d => d.IdMovieCollection)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieCollectionUsers_MovieCollection");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.MovieCollectionUsers)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieCollectionUsers_User");
        });

        modelBuilder.Entity<MovieCrew>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MovieCrew_pk");

            entity.ToTable("MovieCrew");

            entity.HasIndex(e => e.IdCrew, "IX_MovieCrew_IdCrew");

            entity.HasIndex(e => e.Movie_Id, "IX_MovieCrew_Movie_Id");

            entity.Property(e => e.CharacterName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Job)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCrewNavigation).WithMany(p => p.MovieCrews)
                .HasForeignKey(d => d.IdCrew)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieCrew_Crew");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieCrews)
                .HasForeignKey(d => d.Movie_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieCrew_Movie");
        });

        modelBuilder.Entity<MovieGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MovieGenre_pk");

            entity.ToTable("MovieGenre");

            entity.HasIndex(e => e.Genre_Id, "IX_MovieGenre_Genre_Id");

            entity.HasIndex(e => new { e.Movie_Id, e.Genre_Id }, "IX_MovieGenre_Movie_Id_Genre_Id").IsUnique();

            entity.HasOne(d => d.Genre).WithMany(p => p.MovieGenres)
                .HasForeignKey(d => d.Genre_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieGenre_Genre");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieGenres)
                .HasForeignKey(d => d.Movie_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieGenre_Movie");
        });

        modelBuilder.Entity<MovieOfTheDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MovieOfTheDay_pk");

            entity.ToTable("MovieOfTheDay");

            entity.HasIndex(e => e.Movie_Id, "IX_MovieOfTheDay_Movie_Id").IsUnique();

            entity.Property(e => e.Date).HasPrecision(2);

            entity.HasOne(d => d.Movie).WithOne(p => p.MovieOfTheDay)
                .HasForeignKey<MovieOfTheDay>(d => d.Movie_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieOfTheDay_Movie");
        });

        modelBuilder.Entity<MovieRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MovieRate_pk");

            entity.ToTable("MovieRate");

            entity.HasIndex(e => e.Movie_Id, "IX_MovieRate_Movie_Id");

            entity.HasIndex(e => new { e.IdUser, e.Movie_Id }, "UniqueRating").IsUnique();

            entity.Property(e => e.RatedAt).HasPrecision(2);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.MovieRates)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieRate_User");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieRates)
                .HasForeignKey(d => d.Movie_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieRate_Movie");
        });

        modelBuilder.Entity<MovieUpdateRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MovieUpdateRequest_pk");

            entity.ToTable("MovieUpdateRequest");

            entity.HasIndex(e => e.IdUser, "IX_MovieUpdateRequest_IdUser");

            entity.HasIndex(e => e.Movie_Id, "IX_MovieUpdateRequest_Movie_Id");

            entity.Property(e => e.CreatedAt).HasPrecision(2);
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.MovieUpdateRequests)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieUpdateRequest_User");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieUpdateRequests)
                .HasForeignKey(d => d.Movie_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieUpdateRequest_Movie");

            entity.HasMany(d => d.NewGenres).WithMany(p => p.MovieUpdateRequests)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieUpdateRequestGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("MovieUpdateRequestGenre_Genre"),
                    l => l.HasOne<MovieUpdateRequest>().WithMany()
                        .HasForeignKey("MovieUpdateRequestId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("MovieUpdateRequestGenre_MovieUpdateRequest"),
                    j =>
                    {
                        j.HasKey("MovieUpdateRequestId", "GenreId").HasName("MovieUpdateRequestGenre_pk");
                        j.ToTable("MovieUpdateRequestGenre");
                        j.HasIndex(new[] { "GenreId" }, "IX_MovieUpdateRequestGenre_GenreId");
                    });
        });
        
        modelBuilder.Entity<MovieUpdateRequestCrew>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MovieUpdateRequestCrew_pk");
            entity.ToTable("MovieUpdateRequestCrew");
            
            entity.Property(e => e.FirstName).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.LastName).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Job).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.CharacterName).HasMaxLength(100).IsUnicode(false);

            entity.HasOne(d => d.MovieUpdateRequest)
                .WithMany(p => p.NewCrew)
                .HasForeignKey(d => d.MovieUpdateRequestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("MovieUpdateRequestCrew_MovieUpdateRequest");
        });
        

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Review_pk");

            entity.ToTable("Review");

            entity.HasIndex(e => e.Movie_Id, "IX_Review_Movie_Id");

            entity.HasIndex(e => new { e.IdAuthor, e.Movie_Id }, "UniqueReview").IsUnique();

            entity.Property(e => e.Content)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasPrecision(2);
            entity.Property(e => e.LastModifiedAt).HasPrecision(2);

            entity.HasOne(d => d.IdAuthorNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.IdAuthor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Review_User");

            entity.HasOne(d => d.Movie).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Movie_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Review_Movie");
        });

        modelBuilder.Entity<ReviewRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ReviewRate_pk");

            entity.ToTable("ReviewRate");

            entity.HasIndex(e => e.Review_Id, "IX_ReviewRate_Review_Id");

            entity.Property(e => e.IdUser).HasMaxLength(450);
            entity.Property(e => e.RatedAt).HasPrecision(3);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.ReviewRates)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ReviewRate_User");

            entity.HasOne(d => d.Review).WithMany(p => p.ReviewRates)
                .HasForeignKey(d => d.Review_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ReviewRate_Review");
        });

        modelBuilder.Entity<SocialActivityLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SocialActivityLog_pk");

            entity.ToTable("SocialActivityLog");

            entity.HasIndex(e => e.IdActivityType, "IX_SocialActivityLog_IdActivityType");

            entity.HasIndex(e => e.IdUser, "IX_SocialActivityLog_IdUser");

            entity.Property(e => e.ActivityAt).HasPrecision(2);

            entity.HasOne(d => d.IdActivityTypeNavigation).WithMany(p => p.SocialActivityLogs)
                .HasForeignKey(d => d.IdActivityType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ActivityLog_ActivityType");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.SocialActivityLogs)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ActivityLog_User");
        });

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pk");

            entity.ToTable("User");

            entity.Property(e => e.CreatedAt).HasPrecision(2);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.LastLogin).HasPrecision(2);
            entity.Property(e => e.LastDailyLogin).HasPrecision(2);
            entity.Property(e => e.ProfileDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserRelationship>(entity =>
        {
            entity.HasKey(e => new { e.IdUser, e.IdRelatedUser }).HasName("UserRelationship_pk");

            entity.ToTable("UserRelationship");

            entity.HasIndex(e => e.IdRelatedUser, "IX_UserRelationship_IdRelatedUser");

            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRelatedUserNavigation).WithMany(p => p.UserRelationshipIdRelatedUserNavigations)
                .HasForeignKey(d => d.IdRelatedUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserRelationship_User2");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserRelationshipIdUserNavigations)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserRelationship_User1");
        });

        base.OnModelCreating(modelBuilder);
    }


}