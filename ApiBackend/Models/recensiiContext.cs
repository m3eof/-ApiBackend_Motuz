using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiBackend.Models
{
    public partial class recensiiContext : DbContext
    {
        public recensiiContext()
        {
        }

        public recensiiContext(DbContextOptions<recensiiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Exchange> Exchanges { get; set; } = null!;
        public virtual DbSet<Favorite> Favorites { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Preference> Preferences { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Saved> Saveds { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Wishlist> Wishlists { get; set; } = null!;

     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.Bio)
                    .HasColumnType("text")
                    .HasColumnName("bio");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("full_name");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.PublishedYear).HasColumnName("published_year");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Books__author_id__4316F928");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Books__genre_id__440B1D61");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.TextComment)
                    .HasColumnType("text")
                    .HasColumnName("text_comment");

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ReviewId)
                    .HasConstraintName("FK__Comments__review__05D8E0BE");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comments__users___06CD04F7");
            });

            modelBuilder.Entity<Exchange>(entity =>
            {
                entity.Property(e => e.ExchangeId).HasColumnName("exchange_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.SeekerId).HasColumnName("seeker_id");

                entity.Property(e => e.StatusOfExchange)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("status_of_exchange");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Exchanges)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Exchanges__book___7D439ABD");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.ExchangeOwners)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK__Exchanges__owner__7E37BEF6");

                entity.HasOne(d => d.Seeker)
                    .WithMany(p => p.ExchangeSeekers)
                    .HasForeignKey(d => d.SeekerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Exchanges__seeke__7F2BE32F");
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(e => e.FavId)
                    .HasName("PK__Favorite__37AAF6FE22FED5FB");

                entity.HasIndex(e => new { e.UsersId, e.BookId }, "UQ__Favorite__EE3700E469374E21")
                    .IsUnique();

                entity.Property(e => e.FavId).HasColumnName("fav_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Favorites__book___0E6E26BF");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK__Favorites__users__0F624AF8");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasIndex(e => e.NameOfGenre, "UQ__Genres__BF6C47E6195CE769")
                    .IsUnique();

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.NameOfGenre)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("name_of_genre");
            });

            modelBuilder.Entity<Preference>(entity =>
            {
                entity.HasKey(e => e.PrefId)
                    .HasName("PK__Preferen__F94C4B3D76066907");

                entity.HasIndex(e => new { e.UsersId, e.GenreId }, "UQ__Preferen__DB23F99E0A7E1747")
                    .IsUnique();

                entity.Property(e => e.PrefId).HasColumnName("pref_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Preferences)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK__Preferenc__genre__1332DBDC");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Preferences)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK__Preferenc__users__14270015");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.LikesCount)
                    .HasColumnName("likes_count")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.ReviewText)
                    .HasColumnType("text")
                    .HasColumnName("review_text");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Reviews__book_id__6E01572D");

                entity.HasOne(d => d.UsersNavigation)
                    .WithMany(p => p.ReviewsNavigation)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK__Reviews__users_i__6D0D32F4");
            });

            modelBuilder.Entity<Saved>(entity =>
            {
                entity.ToTable("Saved");

                entity.Property(e => e.SavedId).HasColumnName("saved_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.Saveds)
                    .HasForeignKey(d => d.ReviewId)
                    .HasConstraintName("FK__Saved__review_id__1AD3FDA4");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Saveds)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Saved__users_id__1BC821DD");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UsersId)
                    .HasName("PK__Users__EAA7D14B7F0B63D1");

                entity.HasIndex(e => e.Email, "UQ__Users__AB6E61640EFAC5EA")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__Users__F3DBC57206F61104")
                    .IsUnique();

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FollowerNumber)
                    .HasColumnName("follower_number")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FollowingNumber)
                    .HasColumnName("following_number")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_password");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasMany(d => d.Followers)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "Follower",
                        l => l.HasOne<User>().WithMany().HasForeignKey("FollowerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Followers__follo__60A75C0F"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UsersId").HasConstraintName("FK__Followers__users__619B8048"),
                        j =>
                        {
                            j.HasKey("UsersId", "FollowerId").HasName("PK__Follower__0EE3326971ED1E65");

                            j.ToTable("Followers");

                            j.IndexerProperty<int>("UsersId").HasColumnName("users_id");

                            j.IndexerProperty<int>("FollowerId").HasColumnName("follower_id");
                        });

                entity.HasMany(d => d.Followings)
                    .WithMany(p => p.UsersNavigation)
                    .UsingEntity<Dictionary<string, object>>(
                        "Following",
                        l => l.HasOne<User>().WithMany().HasForeignKey("FollowingId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Following__follo__6477ECF3"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UsersId").HasConstraintName("FK__Following__users__656C112C"),
                        j =>
                        {
                            j.HasKey("UsersId", "FollowingId").HasName("PK__Followin__642865C3CDABB615");

                            j.ToTable("Following");

                            j.IndexerProperty<int>("UsersId").HasColumnName("users_id");

                            j.IndexerProperty<int>("FollowingId").HasColumnName("following_id");
                        });

                entity.HasMany(d => d.Reviews)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "Like",
                        l => l.HasOne<Review>().WithMany().HasForeignKey("ReviewId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Likes__review_id__74AE54BC"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UsersId").HasConstraintName("FK__Likes__users_id__75A278F5"),
                        j =>
                        {
                            j.HasKey("UsersId", "ReviewId").HasName("PK__Likes__FCAF5292CBEAC829");

                            j.ToTable("Likes");

                            j.IndexerProperty<int>("UsersId").HasColumnName("users_id");

                            j.IndexerProperty<int>("ReviewId").HasColumnName("review_id");
                        });

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.Followers)
                    .UsingEntity<Dictionary<string, object>>(
                        "Follower",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UsersId").HasConstraintName("FK__Followers__users__619B8048"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("FollowerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Followers__follo__60A75C0F"),
                        j =>
                        {
                            j.HasKey("UsersId", "FollowerId").HasName("PK__Follower__0EE3326971ED1E65");

                            j.ToTable("Followers");

                            j.IndexerProperty<int>("UsersId").HasColumnName("users_id");

                            j.IndexerProperty<int>("FollowerId").HasColumnName("follower_id");
                        });

                entity.HasMany(d => d.UsersNavigation)
                    .WithMany(p => p.Followings)
                    .UsingEntity<Dictionary<string, object>>(
                        "Following",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UsersId").HasConstraintName("FK__Following__users__656C112C"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("FollowingId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Following__follo__6477ECF3"),
                        j =>
                        {
                            j.HasKey("UsersId", "FollowingId").HasName("PK__Followin__642865C3CDABB615");

                            j.ToTable("Following");

                            j.IndexerProperty<int>("UsersId").HasColumnName("users_id");

                            j.IndexerProperty<int>("FollowingId").HasColumnName("following_id");
                        });
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.ToTable("Wishlist");

                entity.Property(e => e.WishlistId).HasColumnName("wishlist_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Wishlist__book_i__09A971A2");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK__Wishlist__users___0A9D95DB");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
