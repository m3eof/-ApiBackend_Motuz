﻿// <auto-generated />
using System;
using ApiBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiBackend.Migrations
{
    [DbContext(typeof(recensiiContext))]
    [Migration("20241218093101_Test3")]
    partial class Test3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.36")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ApiBackend.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccountUsersId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReasonRevoked")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountUsersId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("ApiBackend.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("author_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorId"), 1L, 1);

                    b.Property<string>("Bio")
                        .HasColumnType("text")
                        .HasColumnName("bio");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("full_name");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("ApiBackend.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("author_id");

                    b.Property<int>("GenreId")
                        .HasColumnType("int")
                        .HasColumnName("genre_id");

                    b.Property<int>("PublishedYear")
                        .HasColumnType("int")
                        .HasColumnName("published_year");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("title");

                    b.HasKey("BookId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("GenreId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("ApiBackend.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("comment_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"), 1L, 1);

                    b.Property<byte[]>("CreatedAt")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("created_at");

                    b.Property<int>("ReviewId")
                        .HasColumnType("int")
                        .HasColumnName("review_id");

                    b.Property<string>("TextComment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("text_comment");

                    b.Property<int>("UsersId")
                        .HasColumnType("int")
                        .HasColumnName("users_id");

                    b.HasKey("CommentId");

                    b.HasIndex("ReviewId");

                    b.HasIndex("UsersId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ApiBackend.Models.Exchange", b =>
                {
                    b.Property<int>("ExchangeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("exchange_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExchangeId"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<byte[]>("CreatedAt")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("created_at");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int")
                        .HasColumnName("owner_id");

                    b.Property<int>("SeekerId")
                        .HasColumnType("int")
                        .HasColumnName("seeker_id");

                    b.Property<string>("StatusOfExchange")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("status_of_exchange");

                    b.HasKey("ExchangeId");

                    b.HasIndex("BookId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("SeekerId");

                    b.ToTable("Exchanges");
                });

            modelBuilder.Entity("ApiBackend.Models.Favorite", b =>
                {
                    b.Property<int>("FavId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("fav_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FavId"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<int>("UsersId")
                        .HasColumnType("int")
                        .HasColumnName("users_id");

                    b.HasKey("FavId")
                        .HasName("PK__Favorite__37AAF6FE22FED5FB");

                    b.HasIndex("BookId");

                    b.HasIndex(new[] { "UsersId", "BookId" }, "UQ__Favorite__EE3700E469374E21")
                        .IsUnique();

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("ApiBackend.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("genre_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"), 1L, 1);

                    b.Property<string>("NameOfGenre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("name_of_genre");

                    b.HasKey("GenreId");

                    b.HasIndex(new[] { "NameOfGenre" }, "UQ__Genres__BF6C47E6195CE769")
                        .IsUnique();

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("ApiBackend.Models.Preference", b =>
                {
                    b.Property<int>("PrefId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pref_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrefId"), 1L, 1);

                    b.Property<int>("GenreId")
                        .HasColumnType("int")
                        .HasColumnName("genre_id");

                    b.Property<int>("UsersId")
                        .HasColumnType("int")
                        .HasColumnName("users_id");

                    b.HasKey("PrefId")
                        .HasName("PK__Preferen__F94C4B3D76066907");

                    b.HasIndex("GenreId");

                    b.HasIndex(new[] { "UsersId", "GenreId" }, "UQ__Preferen__DB23F99E0A7E1747")
                        .IsUnique();

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("ApiBackend.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("review_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<byte[]>("CreatedAt")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("created_at");

                    b.Property<int?>("LikesCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("likes_count")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("rating");

                    b.Property<string>("ReviewText")
                        .HasColumnType("text")
                        .HasColumnName("review_text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("title");

                    b.Property<int>("UsersId")
                        .HasColumnType("int")
                        .HasColumnName("users_id");

                    b.HasKey("ReviewId");

                    b.HasIndex("BookId");

                    b.HasIndex("UsersId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("ApiBackend.Models.Saved", b =>
                {
                    b.Property<int>("SavedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("saved_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SavedId"), 1L, 1);

                    b.Property<byte[]>("CreatedAt")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("created_at");

                    b.Property<int>("ReviewId")
                        .HasColumnType("int")
                        .HasColumnName("review_id");

                    b.Property<int>("UsersId")
                        .HasColumnType("int")
                        .HasColumnName("users_id");

                    b.HasKey("SavedId");

                    b.HasIndex("ReviewId");

                    b.HasIndex("UsersId");

                    b.ToTable("Saved", (string)null);
                });

            modelBuilder.Entity("ApiBackend.Models.User", b =>
                {
                    b.Property<int>("UsersId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("users_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsersId"), 1L, 1);

                    b.Property<bool>("AcceptTerms")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("CreatedAt")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<int?>("FollowerNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("follower_number")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("FollowingNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("following_number")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime?>("PasswordReset")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("username");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Verified")
                        .HasColumnType("datetime2");

                    b.HasKey("UsersId")
                        .HasName("PK__Users__EAA7D14B7F0B63D1");

                    b.HasIndex(new[] { "Email" }, "UQ__Users__AB6E61640EFAC5EA")
                        .IsUnique();

                    b.HasIndex(new[] { "Username" }, "UQ__Users__F3DBC57206F61104")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ApiBackend.Models.Wishlist", b =>
                {
                    b.Property<int>("WishlistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("wishlist_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WishlistId"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<byte[]>("CreatedAt")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("created_at");

                    b.Property<int>("UsersId")
                        .HasColumnType("int")
                        .HasColumnName("users_id");

                    b.HasKey("WishlistId");

                    b.HasIndex("BookId");

                    b.HasIndex("UsersId");

                    b.ToTable("Wishlist", (string)null);
                });

            modelBuilder.Entity("Follower", b =>
                {
                    b.Property<int>("UsersId")
                        .HasColumnType("int")
                        .HasColumnName("users_id");

                    b.Property<int>("FollowerId")
                        .HasColumnType("int")
                        .HasColumnName("follower_id");

                    b.HasKey("UsersId", "FollowerId")
                        .HasName("PK__Follower__0EE3326971ED1E65");

                    b.HasIndex("FollowerId");

                    b.ToTable("Followers", (string)null);
                });

            modelBuilder.Entity("Following", b =>
                {
                    b.Property<int>("UsersId")
                        .HasColumnType("int")
                        .HasColumnName("users_id");

                    b.Property<int>("FollowingId")
                        .HasColumnType("int")
                        .HasColumnName("following_id");

                    b.HasKey("UsersId", "FollowingId")
                        .HasName("PK__Followin__642865C3CDABB615");

                    b.HasIndex("FollowingId");

                    b.ToTable("Following", (string)null);
                });

            modelBuilder.Entity("Like", b =>
                {
                    b.Property<int>("UsersId")
                        .HasColumnType("int")
                        .HasColumnName("users_id");

                    b.Property<int>("ReviewId")
                        .HasColumnType("int")
                        .HasColumnName("review_id");

                    b.HasKey("UsersId", "ReviewId")
                        .HasName("PK__Likes__FCAF5292CBEAC829");

                    b.HasIndex("ReviewId");

                    b.ToTable("Likes", (string)null);
                });

            modelBuilder.Entity("ApiBackend.Entities.RefreshToken", b =>
                {
                    b.HasOne("ApiBackend.Models.User", "Account")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ApiBackend.Models.Book", b =>
                {
                    b.HasOne("ApiBackend.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .IsRequired()
                        .HasConstraintName("FK__Books__author_id__4316F928");

                    b.HasOne("ApiBackend.Models.Genre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreId")
                        .IsRequired()
                        .HasConstraintName("FK__Books__genre_id__440B1D61");

                    b.Navigation("Author");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("ApiBackend.Models.Comment", b =>
                {
                    b.HasOne("ApiBackend.Models.Review", "Review")
                        .WithMany("Comments")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Comments__review__05D8E0BE");

                    b.HasOne("ApiBackend.Models.User", "Users")
                        .WithMany("Comments")
                        .HasForeignKey("UsersId")
                        .IsRequired()
                        .HasConstraintName("FK__Comments__users___06CD04F7");

                    b.Navigation("Review");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ApiBackend.Models.Exchange", b =>
                {
                    b.HasOne("ApiBackend.Models.Book", "Book")
                        .WithMany("Exchanges")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Exchanges__book___7D439ABD");

                    b.HasOne("ApiBackend.Models.User", "Owner")
                        .WithMany("ExchangeOwners")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Exchanges__owner__7E37BEF6");

                    b.HasOne("ApiBackend.Models.User", "Seeker")
                        .WithMany("ExchangeSeekers")
                        .HasForeignKey("SeekerId")
                        .IsRequired()
                        .HasConstraintName("FK__Exchanges__seeke__7F2BE32F");

                    b.Navigation("Book");

                    b.Navigation("Owner");

                    b.Navigation("Seeker");
                });

            modelBuilder.Entity("ApiBackend.Models.Favorite", b =>
                {
                    b.HasOne("ApiBackend.Models.Book", "Book")
                        .WithMany("Favorites")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Favorites__book___0E6E26BF");

                    b.HasOne("ApiBackend.Models.User", "Users")
                        .WithMany("Favorites")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Favorites__users__0F624AF8");

                    b.Navigation("Book");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ApiBackend.Models.Preference", b =>
                {
                    b.HasOne("ApiBackend.Models.Genre", "Genre")
                        .WithMany("Preferences")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Preferenc__genre__1332DBDC");

                    b.HasOne("ApiBackend.Models.User", "Users")
                        .WithMany("Preferences")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Preferenc__users__14270015");

                    b.Navigation("Genre");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ApiBackend.Models.Review", b =>
                {
                    b.HasOne("ApiBackend.Models.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Reviews__book_id__6E01572D");

                    b.HasOne("ApiBackend.Models.User", "UsersNavigation")
                        .WithMany("ReviewsNavigation")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Reviews__users_i__6D0D32F4");

                    b.Navigation("Book");

                    b.Navigation("UsersNavigation");
                });

            modelBuilder.Entity("ApiBackend.Models.Saved", b =>
                {
                    b.HasOne("ApiBackend.Models.Review", "Review")
                        .WithMany("Saveds")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Saved__review_id__1AD3FDA4");

                    b.HasOne("ApiBackend.Models.User", "Users")
                        .WithMany("Saveds")
                        .HasForeignKey("UsersId")
                        .IsRequired()
                        .HasConstraintName("FK__Saved__users_id__1BC821DD");

                    b.Navigation("Review");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ApiBackend.Models.Wishlist", b =>
                {
                    b.HasOne("ApiBackend.Models.Book", "Book")
                        .WithMany("Wishlists")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Wishlist__book_i__09A971A2");

                    b.HasOne("ApiBackend.Models.User", "Users")
                        .WithMany("Wishlists")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Wishlist__users___0A9D95DB");

                    b.Navigation("Book");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Follower", b =>
                {
                    b.HasOne("ApiBackend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("FollowerId")
                        .IsRequired()
                        .HasConstraintName("FK__Followers__follo__60A75C0F");

                    b.HasOne("ApiBackend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("FK__Followers__users__619B8048");
                });

            modelBuilder.Entity("Following", b =>
                {
                    b.HasOne("ApiBackend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("FollowingId")
                        .IsRequired()
                        .HasConstraintName("FK__Following__follo__6477ECF3");

                    b.HasOne("ApiBackend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("FK__Following__users__656C112C");
                });

            modelBuilder.Entity("Like", b =>
                {
                    b.HasOne("ApiBackend.Models.Review", null)
                        .WithMany()
                        .HasForeignKey("ReviewId")
                        .IsRequired()
                        .HasConstraintName("FK__Likes__review_id__74AE54BC");

                    b.HasOne("ApiBackend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Likes__users_id__75A278F5");
                });

            modelBuilder.Entity("ApiBackend.Models.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("ApiBackend.Models.Book", b =>
                {
                    b.Navigation("Exchanges");

                    b.Navigation("Favorites");

                    b.Navigation("Reviews");

                    b.Navigation("Wishlists");
                });

            modelBuilder.Entity("ApiBackend.Models.Genre", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("Preferences");
                });

            modelBuilder.Entity("ApiBackend.Models.Review", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Saveds");
                });

            modelBuilder.Entity("ApiBackend.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ExchangeOwners");

                    b.Navigation("ExchangeSeekers");

                    b.Navigation("Favorites");

                    b.Navigation("Preferences");

                    b.Navigation("RefreshTokens");

                    b.Navigation("ReviewsNavigation");

                    b.Navigation("Saveds");

                    b.Navigation("Wishlists");
                });
#pragma warning restore 612, 618
        }
    }
}