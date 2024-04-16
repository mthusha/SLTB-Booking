using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusTicketBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatemt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agency",
                columns: table => new
                {
                    PartnerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    APIKey = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agency", x => x.PartnerID);
                });

            migrationBuilder.CreateTable(
                name: "bus",
                columns: table => new
                {
                    BusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bus", x => x.BusID);
                });

            migrationBuilder.CreateTable(
                name: "passengers",
                columns: table => new
                {
                    PassengerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NIC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passengers", x => x.PassengerID);
                });

            migrationBuilder.CreateTable(
                name: "routes",
                columns: table => new
                {
                    RouteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartStation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndStation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_routes", x => x.RouteID);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "seats",
                columns: table => new
                {
                    SeatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seats", x => x.SeatID);
                    table.ForeignKey(
                        name: "FK_seats_bus_BusID",
                        column: x => x.BusID,
                        principalTable: "bus",
                        principalColumn: "BusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteID = table.Column<int>(type: "int", nullable: false),
                    BusID = table.Column<int>(type: "int", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedules", x => x.ScheduleID);
                    table.ForeignKey(
                        name: "FK_schedules_bus_BusID",
                        column: x => x.BusID,
                        principalTable: "bus",
                        principalColumn: "BusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schedules_routes_RouteID",
                        column: x => x.RouteID,
                        principalTable: "routes",
                        principalColumn: "RouteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerID = table.Column<int>(type: "int", nullable: false),
                    ScheduleID = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_bookings_passengers_PassengerID",
                        column: x => x.PassengerID,
                        principalTable: "passengers",
                        principalColumn: "PassengerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookings_schedules_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "schedules",
                        principalColumn: "ScheduleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "seatselection",
                columns: table => new
                {
                    SeatSelectionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    SeatID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seatselection", x => x.SeatSelectionID);
                    table.ForeignKey(
                        name: "FK_seatselection_bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "bookings",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_seatselection_seats_SeatID",
                        column: x => x.SeatID,
                        principalTable: "seats",
                        principalColumn: "SeatID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookings_PassengerID",
                table: "bookings",
                column: "PassengerID");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_ScheduleID",
                table: "bookings",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_schedules_BusID",
                table: "schedules",
                column: "BusID");

            migrationBuilder.CreateIndex(
                name: "IX_schedules_RouteID",
                table: "schedules",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_seats_BusID",
                table: "seats",
                column: "BusID");

            migrationBuilder.CreateIndex(
                name: "IX_seatselection_BookingID",
                table: "seatselection",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_seatselection_SeatID",
                table: "seatselection",
                column: "SeatID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agency");

            migrationBuilder.DropTable(
                name: "seatselection");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "seats");

            migrationBuilder.DropTable(
                name: "passengers");

            migrationBuilder.DropTable(
                name: "schedules");

            migrationBuilder.DropTable(
                name: "bus");

            migrationBuilder.DropTable(
                name: "routes");
        }
    }
}
