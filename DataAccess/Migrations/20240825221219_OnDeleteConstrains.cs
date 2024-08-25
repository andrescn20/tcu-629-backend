using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class OnDeleteConstrains : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Devices_DeviceId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Boards_BoardId",
                table: "Sensors");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_SensorTypes_SensorTypeId",
                table: "Sensors");

            migrationBuilder.DropForeignKey(
                name: "FK_TemperatureData_Sensors_SensorId",
                table: "TemperatureData");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Devices_DeviceId",
                table: "Boards",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Boards_BoardId",
                table: "Sensors",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "BoardId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_SensorTypes_SensorTypeId",
                table: "Sensors",
                column: "SensorTypeId",
                principalTable: "SensorTypes",
                principalColumn: "SensorTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TemperatureData_Sensors_SensorId",
                table: "TemperatureData",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "SensorId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Devices_DeviceId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Boards_BoardId",
                table: "Sensors");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_SensorTypes_SensorTypeId",
                table: "Sensors");

            migrationBuilder.DropForeignKey(
                name: "FK_TemperatureData_Sensors_SensorId",
                table: "TemperatureData");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Devices_DeviceId",
                table: "Boards",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Boards_BoardId",
                table: "Sensors",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_SensorTypes_SensorTypeId",
                table: "Sensors",
                column: "SensorTypeId",
                principalTable: "SensorTypes",
                principalColumn: "SensorTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TemperatureData_Sensors_SensorId",
                table: "TemperatureData",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "SensorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
