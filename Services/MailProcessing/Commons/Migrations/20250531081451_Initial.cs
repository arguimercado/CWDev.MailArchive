using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MailProcessing.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorMailLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mailbox = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ErrorDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MailId = table.Column<long>(type: "bigint", nullable: false),
                    ErrorDetails = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorMailLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MailLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mailbox = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    LatestMailDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MailId = table.Column<long>(type: "bigint", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorMailLogs");

            migrationBuilder.DropTable(
                name: "MailLogs");
        }
    }
}
