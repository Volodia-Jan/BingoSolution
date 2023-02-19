using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BingoAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Friendship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGameInfoPrivate",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FriendId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => new { x.UserId, x.FriendId });
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_FriendId",
                        column: x => x.FriendId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1e9deac9-7e9d-46d6-997a-32efdf6d5f6c"),
                columns: new[] { "ConcurrencyStamp", "IsGameInfoPrivate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9e8cc6b-86fc-4c58-bc48-bd6a0dc3637a", false, "AQAAAAIAAYagAAAAEArvcPFqBuFiFHI+rhAftFt8o0jE2GmyKctMugfd7od5lmKyKncNhOt7h2/HtDjUDg==", "8921235b-855d-420d-a94a-de0a141c7fca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d8fde7-99da-4a9c-9cfc-64bfc84d7196"),
                columns: new[] { "ConcurrencyStamp", "IsGameInfoPrivate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7f253aa-60e8-42d0-9d76-c26d6fd4e8dc", false, "AQAAAAIAAYagAAAAEMugrvYSKPLYtLOhAnQl/xt+JeOET+r/H//GSKw56bmgGXqVjhfCS7hGOnf6A8BKLQ==", "cc1bb412-e3b7-4cee-a421-1183ea8fac45" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("af690bfe-f82e-4856-b8dc-1075a5f5c6b9"),
                columns: new[] { "ConcurrencyStamp", "IsGameInfoPrivate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "12e046c0-65e4-4f76-91aa-24a65498fdf9", false, "AQAAAAIAAYagAAAAEDFRF4z9NSbNyrGMfcfdibDwdxwDqHcspnb6nZdmH9dPG9zVHnkrqTAy5wzvHJnr0Q==", "4360b99a-6c0f-4ad2-8656-c4472f322951" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4a4a0ca-d7cb-4e8f-8c58-34535c9eab5b"),
                columns: new[] { "ConcurrencyStamp", "IsGameInfoPrivate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "526a57d1-e43c-4177-a855-4e1112967b39", false, "AQAAAAIAAYagAAAAEAFHvKO+NKzd3j4FDYZeo3oSIqmY1iRSyqbs5z1rNSrt1o45+6xVCCbCr0ZhlwVh3w==", "6b502d73-8603-4d86-8e72-8bbb63aec4d0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e563aa88-86b1-4c69-a0f8-496b53c9ac26"),
                columns: new[] { "ConcurrencyStamp", "IsGameInfoPrivate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cbdc741f-0d4b-4269-a0c7-949227ddabf9", false, "AQAAAAIAAYagAAAAEICocn2+Z+rJS/2nfv+J0hUIwGw2zqtBZPH8VFxiAAO41k7WHZCUsr8NFCqyb5kU/A==", "4e03e46a-fa62-4ba7-a6dc-a2e21dbe59b2" });

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_FriendId",
                table: "Friendships",
                column: "FriendId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropColumn(
                name: "IsGameInfoPrivate",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1e9deac9-7e9d-46d6-997a-32efdf6d5f6c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "325f9195-0c89-465f-88cc-fff9488587b7", "AQAAAAIAAYagAAAAEJNYmRkfaIPoU2VDViRd3WYPstzZVpiJQX1+9hde+T1vfVB0HeGCWZBZQOIX20AiHA==", "698fbdc4-841a-4205-9222-5ab0ab2b5d83" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d8fde7-99da-4a9c-9cfc-64bfc84d7196"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c64cb83a-a40b-4cc7-87d3-a3ddcfaff9fb", "AQAAAAIAAYagAAAAECTHYN5dD6fL0/ZuZYwkO04d6XkkXXsxaOOTO/fx6R1TmRdx8ELEUrNVoxZRINqrBA==", "27d26e7a-6246-41ae-bfd4-1c147bb8ee35" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("af690bfe-f82e-4856-b8dc-1075a5f5c6b9"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4db62c9d-211e-4f34-8bc0-cfb560a7f104", "AQAAAAIAAYagAAAAEO2xCFM7gD9p/FjX0vh3oVJK//IVHoh9z7pJNgKmEg/kIQq4b+xITmxRP2jFq6lHAg==", "ad6713b0-a929-4409-b54f-7c7dbb64ab06" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4a4a0ca-d7cb-4e8f-8c58-34535c9eab5b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17d546c4-9b1a-481c-95e5-d6c9ecf456b8", "AQAAAAIAAYagAAAAEOTZSD+95WRj4hvzWj8mRb1vz44mO5kMGm6zulNDaTERHmUHc+vsuk+lYCJKEP5EvA==", "8922a778-b75a-46a2-a6c7-502fce0458f9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e563aa88-86b1-4c69-a0f8-496b53c9ac26"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d42b5b9-6f03-441d-94eb-fd20246c446c", "AQAAAAIAAYagAAAAEC4fHWEN91YTuHQLNL6EqyHfPjH7t/L7G6YjvRU+5EbnzBv4BUWMlEiSxfbz+O+bBw==", "4510146f-dd3d-4d6a-85dc-4d34fd90c3a7" });
        }
    }
}
