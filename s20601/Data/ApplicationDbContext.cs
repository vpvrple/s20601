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
    public virtual DbSet<Group> Groups { get; set; }
    public virtual DbSet<GroupMembership> GroupMemberships { get; set; }
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
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<ReviewRate> ReviewRates { get; set; }
    public virtual DbSet<SocialActivityLog> SocialActivityLogs { get; set; }
    public virtual DbSet<Status> Statuses { get; set; }
    public virtual DbSet<UserRelationship> UserRelationships { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ActivityType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ActivityType_pk");

            entity.ToTable("ActivityType");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        builder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Comment_pk");

            entity.ToTable("Comment");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasPrecision(2);
            entity.Property(e => e.LastModifiedAt).HasPrecision(2);

            entity.HasOne(d => d.IdCommentNavigation).WithMany(p => p.InverseIdCommentNavigation)
                .HasForeignKey(d => d.IdComment)
                .HasConstraintName("Comment_Comment");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Comment_Post");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Comment_User");
        });

        builder.Entity<Crew>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Crew_pk");

            entity.ToTable("Crew");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        builder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Genre_pk");

            entity.ToTable("Genre");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        builder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Group_pk");

            entity.ToTable("Group");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Image).HasMaxLength(100);
        });

        builder.Entity<GroupMembership>(entity =>
        {
            entity.HasKey(e => new { e.IdUser, e.IdGroup }).HasName("GroupMembership_pk");

            entity.ToTable("GroupMembership");

            entity.Property(e => e.JoinedAt).HasPrecision(2);

            entity.HasOne(d => d.IdGroupNavigation).WithMany(p => p.GroupMemberships)
                .HasForeignKey(d => d.IdGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GroupMembership_Group");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.GroupMemberships)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GroupMembership_User");
        });

        builder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Message_pk");

            entity.ToTable("Message");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Content)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.DeliverTime).HasPrecision(2);

            entity.HasOne(d => d.IdRecipientNavigation).WithMany(p => p.MessageIdRecipientNavigations)
                .HasForeignKey(d => d.IdRecipient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Message_Recipient");

            entity.HasOne(d => d.IdSenderNavigation).WithMany(p => p.MessageIdSenderNavigations)
                .HasForeignKey(d => d.IdSender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Message_Sender");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Message_Status");
        });

        builder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Movie_pk");

            entity.ToTable("Movie");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OriginalTitle)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TitleType)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        builder.Entity<MovieCollection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MovieCollection_pk");

            entity.ToTable("MovieCollection");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasPrecision(2);
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        builder.Entity<MovieCollectionMovie>(entity =>
        {
            entity.HasKey(e => e.IdMovieCollection).HasName("MovieCollectionMovie_pk");

            entity.ToTable("MovieCollectionMovie");

            entity.Property(e => e.IdMovieCollection).ValueGeneratedNever();
            entity.Property(e => e.AddedAt).HasPrecision(2);
            entity.Property(e => e.Movie_Id)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMovieCollectionNavigation).WithOne(p => p.MovieCollectionMovie)
                .HasForeignKey<MovieCollectionMovie>(d => d.IdMovieCollection)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieCollectionMovie_MovieCollection");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieCollectionMovies)
                .HasForeignKey(d => d.Movie_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieCollectionMovie_Movie");
        });

        builder.Entity<MovieCollectionUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MovieCollectionUser_pk");

            entity.ToTable("MovieCollectionUser");

            entity.HasIndex(e => new { e.IdUser, e.IdMovieCollection }, "MovieCollectionUser_ak_1").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdMovieCollectionNavigation).WithMany(p => p.MovieCollectionUsers)
                .HasForeignKey(d => d.IdMovieCollection)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieCollectionUsers_MovieCollection");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.MovieCollectionUsers)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieCollectionUsers_User");
        });

        builder.Entity<MovieCrew>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MovieCrew_pk");

            entity.ToTable("MovieCrew");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CharacterName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Job)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Movie_Id)
                .HasMaxLength(10)
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

        builder.Entity<MovieGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MovieGenre_pk");

            entity.ToTable("MovieGenre");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Movie_Id)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasIndex(e => new { e.Movie_Id, e.Genre_Id })
                .IsUnique(true);

            entity.HasOne(d => d.Genre).WithMany(p => p.MovieGenres)
                .HasForeignKey(d => d.Genre_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieGenre_Genre");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieGenres)
                .HasForeignKey(d => d.Movie_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieGenre_Movie");
        });

        builder.Entity<MovieOfTheDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MovieOfTheDay_pk");

            entity.ToTable("MovieOfTheDay");

            entity.Property(e => e.Movie_Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Date).HasPrecision(2);

            entity.HasOne(d => d.Movie).WithOne(p => p.MovieOfTheDay)
                .HasForeignKey<MovieOfTheDay>(d => d.Movie_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieOfTheDay_Movie");
        });

        builder.Entity<MovieRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MovieRate_pk");

            entity.ToTable("MovieRate");

            entity.HasIndex(e => new { e.IdUser, e.Movie_Id }, "UniqueRating").IsUnique();

            entity.Property(e => e.Movie_Id)
                .HasMaxLength(10)
                .IsUnicode(false);
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

        builder.Entity<MovieUpdateRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MovieUpdateRequest_pk");

            entity.ToTable("MovieUpdateRequest");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasPrecision(2);
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.Movie_Id)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.MovieUpdateRequests)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieUpdateRequest_User");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieUpdateRequests)
                .HasForeignKey(d => d.Movie_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MovieUpdateRequest_Movie");
        });



        builder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Post_pk");

            entity.ToTable("Post");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Content)
                .HasMaxLength(5000)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasPrecision(2);
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.IdGroupNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Post_Group");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Post_User");
        });

        builder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Review_pk");

            entity.HasIndex(e => new { e.IdAuthor, e.Movie_Id }, "UniqueReview").IsUnique();

            entity.ToTable("Review");

            entity.Property(e => e.IdAuthor).ValueGeneratedNever();
            entity.Property(e => e.Content)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasPrecision(2);
            entity.Property(e => e.LastModifiedAt).HasPrecision(2);
            entity.Property(e => e.Movie_Id)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAuthorNavigation).WithOne(p => p.Review)
                .HasForeignKey<Review>(d => d.IdAuthor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Review_User");

            entity.HasOne(d => d.Movie).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Movie_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Review_Movie");
        });

        builder.Entity<ReviewRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ReviewRate_pk");

            entity.ToTable("ReviewRate");

            entity.Property(e => e.IdUser).ValueGeneratedNever();
            entity.Property(e => e.RatedAt).HasPrecision(3);

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.ReviewRate)
                .HasForeignKey<ReviewRate>(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ReviewRate_User");

            entity.HasOne(d => d.Review_IdNavigation).WithMany(p => p.ReviewRates)
                .HasForeignKey(d => d.Review_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ReviewRate_Review");
        });


        builder.Entity<SocialActivityLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SocialActivityLog_pk");

            entity.ToTable("SocialActivityLog");

            entity.Property(e => e.Id).ValueGeneratedNever();
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

        builder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Status_pk");

            entity.ToTable("Status");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pk");

            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasPrecision(2);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.LastLogin).HasPrecision(2);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProfileDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.IdGroups).WithMany(p => p.IdOwners)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupOwnership",
                    r => r.HasOne<Group>().WithMany()
                        .HasForeignKey("IdGroup")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("GroupOwnership_Group"),
                    l => l.HasOne<ApplicationUser>().WithMany()
                        .HasForeignKey("IdOwner")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("GroupOwnership_User"),
                    j =>
                    {
                        j.HasKey("IdOwner", "IdGroup").HasName("GroupOwnership_pk");
                        j.ToTable("GroupOwnership");
                    });
        });

        builder.Entity<UserRelationship>(entity =>
        {
            entity.HasKey(e => new { e.IdUser, e.IdRelatedUser }).HasName("UserRelationship_pk");

            entity.ToTable("UserRelationship");

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
    }
}
