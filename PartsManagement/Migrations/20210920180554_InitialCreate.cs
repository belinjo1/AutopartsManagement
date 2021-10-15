using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PartsManagement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Markat",
                columns: table => new
                {
                    MarkaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markat", x => x.MarkaId);
                });

            migrationBuilder.CreateTable(
                name: "Pyetjet",
                columns: table => new
                {
                    ContactId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(nullable: true),
                    Mbiemri = table.Column<string>(nullable: true),
                    Pyetja = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pyetjet", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "Shtetet",
                columns: table => new
                {
                    ShtetiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shtetet", x => x.ShtetiId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modelet",
                columns: table => new
                {
                    ModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(nullable: true),
                    MarkaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelet", x => x.ModelId);
                    table.ForeignKey(
                        name: "FK_Modelet_Markat_MarkaId",
                        column: x => x.MarkaId,
                        principalTable: "Markat",
                        principalColumn: "MarkaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Qytetet",
                columns: table => new
                {
                    QytetiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(nullable: true),
                    ShtetiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qytetet", x => x.QytetiId);
                    table.ForeignKey(
                        name: "FK_Qytetet_Shtetet_ShtetiId",
                        column: x => x.ShtetiId,
                        principalTable: "Shtetet",
                        principalColumn: "ShtetiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Emri = table.Column<string>(nullable: true),
                    Mbiemri = table.Column<string>(nullable: true),
                    Kompania = table.Column<string>(nullable: true),
                    QytetiId = table.Column<int>(nullable: false),
                    ShefiId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Qytetet_QytetiId",
                        column: x => x.QytetiId,
                        principalTable: "Qytetet",
                        principalColumn: "QytetiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_ShefiId",
                        column: x => x.ShefiId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Furnitoret",
                columns: table => new
                {
                    FurnitoriId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(nullable: true),
                    Mbiemri = table.Column<string>(nullable: true),
                    Lokacioni = table.Column<string>(nullable: true),
                    Telefoni = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furnitoret", x => x.FurnitoriId);
                    table.ForeignKey(
                        name: "FK_Furnitoret_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Komentet",
                columns: table => new
                {
                    KomentiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulli = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    emriKomentuesit = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    PuntoriId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentet", x => x.KomentiId);
                    table.ForeignKey(
                        name: "FK_Komentet_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Porosite",
                columns: table => new
                {
                    PorosiaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulli = table.Column<string>(nullable: true),
                    Sasia = table.Column<int>(nullable: false),
                    Klienti = table.Column<string>(nullable: true),
                    Telefoni = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porosite", x => x.PorosiaId);
                    table.ForeignKey(
                        name: "FK_Porosite_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sektoret",
                columns: table => new
                {
                    SektoriId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sektoret", x => x.SektoriId);
                    table.ForeignKey(
                        name: "FK_Sektoret_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produktet",
                columns: table => new
                {
                    ProduktiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Sasia = table.Column<int>(nullable: false),
                    SektoriId = table.Column<int>(nullable: false),
                    MarkaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produktet", x => x.ProduktiId);
                    table.ForeignKey(
                        name: "FK_Produktet_Markat_MarkaId",
                        column: x => x.MarkaId,
                        principalTable: "Markat",
                        principalColumn: "MarkaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produktet_Sektoret_SektoriId",
                        column: x => x.SektoriId,
                        principalTable: "Sektoret",
                        principalColumn: "SektoriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FaturatIN",
                columns: table => new
                {
                    FaturaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sasia = table.Column<int>(nullable: false),
                    Qmimi = table.Column<double>(nullable: false),
                    CreatedAt = table.Column<string>(nullable: true),
                    ProduktiId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaturatIN", x => x.FaturaId);
                    table.ForeignKey(
                        name: "FK_FaturatIN_Produktet_ProduktiId",
                        column: x => x.ProduktiId,
                        principalTable: "Produktet",
                        principalColumn: "ProduktiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FaturatOUT",
                columns: table => new
                {
                    FaturaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sasia = table.Column<int>(nullable: false),
                    Qmimi = table.Column<double>(nullable: false),
                    Totali = table.Column<double>(nullable: false),
                    Shitesi = table.Column<string>(nullable: true),
                    ProduktiId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaturatOUT", x => x.FaturaId);
                    table.ForeignKey(
                        name: "FK_FaturatOUT_Produktet_ProduktiId",
                        column: x => x.ProduktiId,
                        principalTable: "Produktet",
                        principalColumn: "ProduktiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaturatOUT_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16d512ef-3cb6-45ad-88cc-55d702734957", "c8be18bc-4cf8-42b3-ad78-92526326c98d", "User", "USER" },
                    { "7537f523-9b4f-4b69-b883-afca470b3776", "0a92de0f-c2ce-42f6-95a6-9af652528779", "Admin", "ADMIN" },
                    { "2fab0faa-57fa-4522-a1ff-55c79ea315c4", "03761285-7e13-457e-bc0f-5c0b3c4770b0", "Puntor", "PUNTOR" }
                });

            migrationBuilder.InsertData(
                table: "Markat",
                columns: new[] { "MarkaId", "Emri" },
                values: new object[,]
                {
                    { 1, "Ferrari" },
                    { 2, "Audi" },
                    { 3, "BMW" },
                    { 4, "VolksWagen" },
                    { 5, "Mercedes" },
                    { 6, "Skoda" },
                    { 7, "Volvo" },
                    { 8, "Toyota" },
                    { 9, "Mitsubishi" },
                    { 10, "Porsche" }
                });

            migrationBuilder.InsertData(
                table: "Shtetet",
                columns: new[] { "ShtetiId", "Emri" },
                values: new object[,]
                {
                    { 1, "Kosovë" },
                    { 2, "Shqipëri" },
                    { 3, "Maqedoni" }
                });

            migrationBuilder.InsertData(
                table: "Modelet",
                columns: new[] { "ModelId", "Emri", "MarkaId" },
                values: new object[,]
                {
                    { 1, "Ferrari Ri", 1 },
                    { 2, "Audi A3", 2 },
                    { 3, "BMW 5qe", 3 },
                    { 4, "Golf 4shi Bajramit", 4 },
                    { 5, "Mercedes e class", 5 },
                    { 6, "Skoda octavia", 6 },
                    { 7, "Volvo 3.0 tdi", 7 },
                    { 8, "Toyota off-road", 8 },
                    { 9, "Mitsubishi modeli 2t", 9 },
                    { 10, "Porsche panamera", 10 }
                });

            migrationBuilder.InsertData(
                table: "Qytetet",
                columns: new[] { "QytetiId", "Emri", "ShtetiId" },
                values: new object[,]
                {
                    { 47, "Kukës", 2 },
                    { 46, "Kucovë", 2 },
                    { 45, "Krujë", 2 },
                    { 44, "Korcë", 2 },
                    { 43, "Kolonjë", 2 },
                    { 42, "Kavajë", 2 },
                    { 37, "Elbasan", 2 },
                    { 40, "Gjirokastër", 2 },
                    { 39, "Gramsh", 2 },
                    { 38, "Fier", 2 },
                    { 48, "Kurbin", 2 },
                    { 36, "Durrës", 2 },
                    { 35, "Dibër", 2 },
                    { 41, "Has", 2 },
                    { 49, "Lezhë", 2 },
                    { 54, "Mat", 2 },
                    { 51, "Lushnjë", 2 },
                    { 52, "Malësi e madhe", 2 },
                    { 53, "Mallakastër", 2 },
                    { 34, "Devoll", 2 },
                    { 55, "Mirditë", 2 },
                    { 56, "Peqin", 2 },
                    { 57, "Përmet", 2 },
                    { 58, "Pogradec", 2 },
                    { 59, "Pukë", 2 },
                    { 60, "Sarandë", 2 },
                    { 61, "Skrapar", 2 },
                    { 62, "Shkodër", 2 },
                    { 63, "Tepelenë", 2 },
                    { 64, "Tiranë", 2 },
                    { 50, "Librazhd", 2 },
                    { 33, "Delvinë", 2 },
                    { 28, "Vushtrri", 1 },
                    { 31, "Berat", 2 },
                    { 1, "Artanë", 1 },
                    { 2, "Besianë", 1 },
                    { 3, "Burim", 1 },
                    { 4, "Dardanë", 1 },
                    { 5, "Decan", 1 },
                    { 6, "Dragash", 1 },
                    { 7, "Drenas", 1 },
                    { 8, "Ferizaj", 1 },
                    { 9, "Fushë Kosovë", 1 },
                    { 10, "Gjakovë", 1 },
                    { 11, "Gjilan", 1 },
                    { 12, "Kastriot", 1 },
                    { 13, "Kaqanik", 1 },
                    { 14, "Klinë", 1 },
                    { 32, "Bulqizë", 2 },
                    { 15, "Leposaviq", 1 },
                    { 17, "Malishevë", 1 },
                    { 18, "Mitrovicë", 1 },
                    { 19, "Pejë", 1 },
                    { 20, "Prishtinë", 1 },
                    { 21, "Prizren", 1 },
                    { 22, "Rahovec", 1 },
                    { 23, "Skënderaj", 1 },
                    { 24, "Shtërpcë", 1 },
                    { 25, "Shtime", 1 },
                    { 26, "Therandë", 1 },
                    { 27, "Viti", 1 },
                    { 65, "Tropojë", 2 },
                    { 29, "Zubin Potok", 1 },
                    { 30, "Zveqan", 1 },
                    { 16, "Lipjan", 1 },
                    { 66, "Vlorë", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_QytetiId",
                table: "AspNetUsers",
                column: "QytetiId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShefiId",
                table: "AspNetUsers",
                column: "ShefiId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturatIN_ProduktiId",
                table: "FaturatIN",
                column: "ProduktiId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturatOUT_ProduktiId",
                table: "FaturatOUT",
                column: "ProduktiId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturatOUT_UserId",
                table: "FaturatOUT",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Furnitoret_UserId",
                table: "Furnitoret",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentet_UserId",
                table: "Komentet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelet_MarkaId",
                table: "Modelet",
                column: "MarkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Porosite_UserId",
                table: "Porosite",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Produktet_MarkaId",
                table: "Produktet",
                column: "MarkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produktet_SektoriId",
                table: "Produktet",
                column: "SektoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Qytetet_ShtetiId",
                table: "Qytetet",
                column: "ShtetiId");

            migrationBuilder.CreateIndex(
                name: "IX_Sektoret_UserId",
                table: "Sektoret",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FaturatIN");

            migrationBuilder.DropTable(
                name: "FaturatOUT");

            migrationBuilder.DropTable(
                name: "Furnitoret");

            migrationBuilder.DropTable(
                name: "Komentet");

            migrationBuilder.DropTable(
                name: "Modelet");

            migrationBuilder.DropTable(
                name: "Porosite");

            migrationBuilder.DropTable(
                name: "Pyetjet");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Produktet");

            migrationBuilder.DropTable(
                name: "Markat");

            migrationBuilder.DropTable(
                name: "Sektoret");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Qytetet");

            migrationBuilder.DropTable(
                name: "Shtetet");
        }
    }
}
