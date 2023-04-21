using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalWeb.Migrations
{
    /// <inheritdoc />
    public partial class allowdub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Country__3214EC074B60A19C", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Router",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OAuthRou__3214EC07C90747E9", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__States__3214EC07186FF386", x => x.Id);
                    table.ForeignKey(
                        name: "FK__States__CountryI__32E0915F",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebGuid = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false, defaultValueSql: "(CONVERT([varchar](255),lower(newid())))"),
                    UserName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "varchar(8000)", unicode: false, maxLength: 8000, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    OAuthRouter = table.Column<byte>(type: "tinyint", nullable: true),
                    LoginAttempt = table.Column<byte>(type: "tinyint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__3214EC071AFDC7D1", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Users__OAuthRout__2C3393D0",
                        column: x => x.OAuthRouter,
                        principalTable: "Router",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "App",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Secret = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false, defaultValueSql: "(CONVERT([varchar](255),lower(newid())))"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    TagLine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LogoUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RedirectUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__App__3214EC07333458BE", x => x.Id);
                    table.ForeignKey(
                        name: "FK__App__UserId__52593CB8",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Company = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<long>(type: "bigint", nullable: false),
                    Mobile = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__3214EC07A2DEC2A0", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Customer__Countr__36B12243",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Customer__StateI__37A5467C",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Customer__UserId__38996AB5",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuthServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppId = table.Column<int>(type: "int", nullable: false),
                    RouterId = table.Column<byte>(type: "tinyint", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AuthServ__3214EC07BE84E2DD", x => x.Id);
                    table.ForeignKey(
                        name: "FK__AuthServe__AppId__6EF57B66",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__AuthServe__Route__6FE99F9F",
                        column: x => x.RouterId,
                        principalTable: "Router",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppId = table.Column<int>(type: "int", nullable: false),
                    InnerName = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__3214EC076B821EC5", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Roles__AppId__5EBF139D",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserInApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserInAp__3214EC07FAD6493A", x => x.Id);
                    table.ForeignKey(
                        name: "FK__UserInApp__AppId__6C190EBB",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__UserInApp__UserI__6B24EA82",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserInRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserInRo__3214EC0727C60F9D", x => x.Id);
                    table.ForeignKey(
                        name: "FK__UserInRol__RoleI__68487DD7",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__UserInRol__UserI__6754599E",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_App_UserId",
                table: "App",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ__App__8F8373A15F0C10A3",
                table: "App",
                column: "Secret",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthServer_AppId",
                table: "AuthServer",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthServer_RouterId",
                table: "AuthServer",
                column: "RouterId");

            migrationBuilder.CreateIndex(
                name: "UQ__Country__A25C5AA777A1FCFD",
                table: "Country",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CountryId",
                table: "Customer",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_StateId",
                table: "Customer",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                table: "Customer",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__1788CC4D9A0572E5",
                table: "Customer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_AppId",
                table: "Roles",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__4EBBBAC948F24140",
                table: "Roles",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__737584F66775D866",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__73DA93DA7467410B",
                table: "Roles",
                column: "InnerName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "UQ__States__C3BA3B3B10ED8377",
                table: "States",
                column: "StateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInApp_AppId",
                table: "UserInApp",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInApp_UserId",
                table: "UserInApp",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInRoles_RoleId",
                table: "UserInRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInRoles_UserId",
                table: "UserInRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OAuthRouter",
                table: "Users",
                column: "OAuthRouter");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__C790C838BC861566",
                table: "Users",
                column: "WebGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__C9F28456C2BF0E2C",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthServer");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "UserInApp");

            migrationBuilder.DropTable(
                name: "UserInRoles");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "App");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Router");
        }
    }
}
