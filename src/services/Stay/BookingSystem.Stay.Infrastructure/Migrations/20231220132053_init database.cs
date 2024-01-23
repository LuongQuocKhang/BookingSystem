using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSystem.Stay.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Stay");

            migrationBuilder.CreateTable(
                name: "Amenities",
                schema: "Stay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Host",
                schema: "Stay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPlace = table.Column<int>(type: "int", nullable: false),
                    ResponeRate = table.Column<float>(type: "real", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Host", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StayWishLists",
                schema: "Stay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StayId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StayWishLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stay",
                schema: "Stay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfBeds = table.Column<int>(type: "int", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "int", nullable: false),
                    NumberOfBathrooms = table.Column<int>(type: "int", nullable: false),
                    NumberOfBedrooms = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    NumberOfRating = table.Column<double>(type: "float", nullable: false),
                    PricePerNight = table.Column<double>(type: "float", nullable: false),
                    StayInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceCharge = table.Column<double>(type: "float", nullable: false),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    CancellationPolicy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckInTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckOutTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stay_Host_HostId",
                        column: x => x.HostId,
                        principalSchema: "Stay",
                        principalTable: "Host",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomRates",
                schema: "Stay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StayId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomRates_Stay_StayId",
                        column: x => x.StayId,
                        principalSchema: "Stay",
                        principalTable: "Stay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StayAmenities",
                schema: "Stay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmenityId = table.Column<int>(type: "int", nullable: false),
                    StayId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StayAmenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StayAmenities_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalSchema: "Stay",
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StayAmenities_Stay_StayId",
                        column: x => x.StayId,
                        principalSchema: "Stay",
                        principalTable: "Stay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StayImages",
                schema: "Stay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StayId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StayImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StayImages_Stay_StayId",
                        column: x => x.StayId,
                        principalSchema: "Stay",
                        principalTable: "Stay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StayReviews",
                schema: "Stay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StayId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StayReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StayReviews_Stay_StayId",
                        column: x => x.StayId,
                        principalSchema: "Stay",
                        principalTable: "Stay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StayShares",
                schema: "Stay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StayId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StayShares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StayShares_Stay_StayId",
                        column: x => x.StayId,
                        principalSchema: "Stay",
                        principalTable: "Stay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StayTags",
                schema: "Stay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StayId = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StayTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StayTags_Stay_StayId",
                        column: x => x.StayId,
                        principalSchema: "Stay",
                        principalTable: "Stay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StayUnAvailability",
                schema: "Stay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnAvaiableDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StayId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StayUnAvailability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StayUnAvailability_Stay_StayId",
                        column: x => x.StayId,
                        principalSchema: "Stay",
                        principalTable: "Stay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomRates_StayId",
                schema: "Stay",
                table: "RoomRates",
                column: "StayId");

            migrationBuilder.CreateIndex(
                name: "IX_Stay_HostId",
                schema: "Stay",
                table: "Stay",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_StayAmenities_AmenityId",
                schema: "Stay",
                table: "StayAmenities",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_StayAmenities_StayId",
                schema: "Stay",
                table: "StayAmenities",
                column: "StayId");

            migrationBuilder.CreateIndex(
                name: "IX_StayImages_StayId",
                schema: "Stay",
                table: "StayImages",
                column: "StayId");

            migrationBuilder.CreateIndex(
                name: "IX_StayReviews_StayId",
                schema: "Stay",
                table: "StayReviews",
                column: "StayId");

            migrationBuilder.CreateIndex(
                name: "IX_StayShares_StayId",
                schema: "Stay",
                table: "StayShares",
                column: "StayId");

            migrationBuilder.CreateIndex(
                name: "IX_StayTags_StayId",
                schema: "Stay",
                table: "StayTags",
                column: "StayId");

            migrationBuilder.CreateIndex(
                name: "IX_StayUnAvailability_StayId",
                schema: "Stay",
                table: "StayUnAvailability",
                column: "StayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomRates",
                schema: "Stay");

            migrationBuilder.DropTable(
                name: "StayAmenities",
                schema: "Stay");

            migrationBuilder.DropTable(
                name: "StayImages",
                schema: "Stay");

            migrationBuilder.DropTable(
                name: "StayReviews",
                schema: "Stay");

            migrationBuilder.DropTable(
                name: "StayShares",
                schema: "Stay");

            migrationBuilder.DropTable(
                name: "StayTags",
                schema: "Stay");

            migrationBuilder.DropTable(
                name: "StayUnAvailability",
                schema: "Stay");

            migrationBuilder.DropTable(
                name: "StayWishLists",
                schema: "Stay");

            migrationBuilder.DropTable(
                name: "Amenities",
                schema: "Stay");

            migrationBuilder.DropTable(
                name: "Stay",
                schema: "Stay");

            migrationBuilder.DropTable(
                name: "Host",
                schema: "Stay");
        }
    }
}
