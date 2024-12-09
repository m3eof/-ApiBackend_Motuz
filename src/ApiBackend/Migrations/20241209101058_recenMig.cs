using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBackend.Migrations
{
    public partial class recenMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    bio = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    genre_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_of_genre = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.genre_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    users_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    user_password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    follower_number = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    following_number = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    created_at = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__EAA7D14B7F0B63D1", x => x.users_id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    author_id = table.Column<int>(type: "int", nullable: false),
                    published_year = table.Column<int>(type: "int", nullable: false),
                    genre_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.book_id);
                    table.ForeignKey(
                        name: "FK__Books__author_id__4316F928",
                        column: x => x.author_id,
                        principalTable: "Authors",
                        principalColumn: "author_id");
                    table.ForeignKey(
                        name: "FK__Books__genre_id__440B1D61",
                        column: x => x.genre_id,
                        principalTable: "Genres",
                        principalColumn: "genre_id");
                });

            migrationBuilder.CreateTable(
                name: "Followers",
                columns: table => new
                {
                    users_id = table.Column<int>(type: "int", nullable: false),
                    follower_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Follower__0EE3326971ED1E65", x => new { x.users_id, x.follower_id });
                    table.ForeignKey(
                        name: "FK__Followers__follo__60A75C0F",
                        column: x => x.follower_id,
                        principalTable: "Users",
                        principalColumn: "users_id");
                    table.ForeignKey(
                        name: "FK__Followers__users__619B8048",
                        column: x => x.users_id,
                        principalTable: "Users",
                        principalColumn: "users_id");
                });

            migrationBuilder.CreateTable(
                name: "Following",
                columns: table => new
                {
                    users_id = table.Column<int>(type: "int", nullable: false),
                    following_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Followin__642865C3CDABB615", x => new { x.users_id, x.following_id });
                    table.ForeignKey(
                        name: "FK__Following__follo__6477ECF3",
                        column: x => x.following_id,
                        principalTable: "Users",
                        principalColumn: "users_id");
                    table.ForeignKey(
                        name: "FK__Following__users__656C112C",
                        column: x => x.users_id,
                        principalTable: "Users",
                        principalColumn: "users_id");
                });

            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    pref_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    users_id = table.Column<int>(type: "int", nullable: false),
                    genre_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Preferen__F94C4B3D76066907", x => x.pref_id);
                    table.ForeignKey(
                        name: "FK__Preferenc__genre__1332DBDC",
                        column: x => x.genre_id,
                        principalTable: "Genres",
                        principalColumn: "genre_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Preferenc__users__14270015",
                        column: x => x.users_id,
                        principalTable: "Users",
                        principalColumn: "users_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    exchange_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    owner_id = table.Column<int>(type: "int", nullable: false),
                    seeker_id = table.Column<int>(type: "int", nullable: false),
                    status_of_exchange = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    created_at = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.exchange_id);
                    table.ForeignKey(
                        name: "FK__Exchanges__book___7D439ABD",
                        column: x => x.book_id,
                        principalTable: "Books",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Exchanges__owner__7E37BEF6",
                        column: x => x.owner_id,
                        principalTable: "Users",
                        principalColumn: "users_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Exchanges__seeke__7F2BE32F",
                        column: x => x.seeker_id,
                        principalTable: "Users",
                        principalColumn: "users_id");
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    fav_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    users_id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Favorite__37AAF6FE22FED5FB", x => x.fav_id);
                    table.ForeignKey(
                        name: "FK__Favorites__book___0E6E26BF",
                        column: x => x.book_id,
                        principalTable: "Books",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Favorites__users__0F624AF8",
                        column: x => x.users_id,
                        principalTable: "Users",
                        principalColumn: "users_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    users_id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    review_text = table.Column<string>(type: "text", nullable: true),
                    likes_count = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    created_at = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.review_id);
                    table.ForeignKey(
                        name: "FK__Reviews__book_id__6E01572D",
                        column: x => x.book_id,
                        principalTable: "Books",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Reviews__users_i__6D0D32F4",
                        column: x => x.users_id,
                        principalTable: "Users",
                        principalColumn: "users_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    wishlist_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    users_id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlist", x => x.wishlist_id);
                    table.ForeignKey(
                        name: "FK__Wishlist__book_i__09A971A2",
                        column: x => x.book_id,
                        principalTable: "Books",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Wishlist__users___0A9D95DB",
                        column: x => x.users_id,
                        principalTable: "Users",
                        principalColumn: "users_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    review_id = table.Column<int>(type: "int", nullable: false),
                    users_id = table.Column<int>(type: "int", nullable: false),
                    text_comment = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK__Comments__review__05D8E0BE",
                        column: x => x.review_id,
                        principalTable: "Reviews",
                        principalColumn: "review_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Comments__users___06CD04F7",
                        column: x => x.users_id,
                        principalTable: "Users",
                        principalColumn: "users_id");
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    users_id = table.Column<int>(type: "int", nullable: false),
                    review_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Likes__FCAF5292CBEAC829", x => new { x.users_id, x.review_id });
                    table.ForeignKey(
                        name: "FK__Likes__review_id__74AE54BC",
                        column: x => x.review_id,
                        principalTable: "Reviews",
                        principalColumn: "review_id");
                    table.ForeignKey(
                        name: "FK__Likes__users_id__75A278F5",
                        column: x => x.users_id,
                        principalTable: "Users",
                        principalColumn: "users_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Saved",
                columns: table => new
                {
                    saved_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    review_id = table.Column<int>(type: "int", nullable: false),
                    users_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saved", x => x.saved_id);
                    table.ForeignKey(
                        name: "FK__Saved__review_id__1AD3FDA4",
                        column: x => x.review_id,
                        principalTable: "Reviews",
                        principalColumn: "review_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Saved__users_id__1BC821DD",
                        column: x => x.users_id,
                        principalTable: "Users",
                        principalColumn: "users_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_author_id",
                table: "Books",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_genre_id",
                table: "Books",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_review_id",
                table: "Comments",
                column: "review_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_users_id",
                table: "Comments",
                column: "users_id");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_book_id",
                table: "Exchanges",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_owner_id",
                table: "Exchanges",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_seeker_id",
                table: "Exchanges",
                column: "seeker_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_book_id",
                table: "Favorites",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Favorite__EE3700E469374E21",
                table: "Favorites",
                columns: new[] { "users_id", "book_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Followers_follower_id",
                table: "Followers",
                column: "follower_id");

            migrationBuilder.CreateIndex(
                name: "IX_Following_following_id",
                table: "Following",
                column: "following_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Genres__BF6C47E6195CE769",
                table: "Genres",
                column: "name_of_genre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_review_id",
                table: "Likes",
                column: "review_id");

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_genre_id",
                table: "Preferences",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Preferen__DB23F99E0A7E1747",
                table: "Preferences",
                columns: new[] { "users_id", "genre_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_book_id",
                table: "Reviews",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_users_id",
                table: "Reviews",
                column: "users_id");

            migrationBuilder.CreateIndex(
                name: "IX_Saved_review_id",
                table: "Saved",
                column: "review_id");

            migrationBuilder.CreateIndex(
                name: "IX_Saved_users_id",
                table: "Saved",
                column: "users_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__AB6E61640EFAC5EA",
                table: "Users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__F3DBC57206F61104",
                table: "Users",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_book_id",
                table: "Wishlist",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_users_id",
                table: "Wishlist",
                column: "users_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Exchanges");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Followers");

            migrationBuilder.DropTable(
                name: "Following");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Preferences");

            migrationBuilder.DropTable(
                name: "Saved");

            migrationBuilder.DropTable(
                name: "Wishlist");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
