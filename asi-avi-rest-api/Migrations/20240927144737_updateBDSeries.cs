using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asi_avi_rest_api.Migrations
{
    /// <inheritdoc />
    public partial class updateBDSeries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "utl_email",
                schema: "public",
                table: "t_e_utilisateur_utl",
                newName: "utl_mail");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_utilisateur_utl_utl_email",
                schema: "public",
                table: "t_e_utilisateur_utl",
                newName: "IX_t_e_utilisateur_utl_utl_mail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "utl_mail",
                schema: "public",
                table: "t_e_utilisateur_utl",
                newName: "utl_email");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_utilisateur_utl_utl_mail",
                schema: "public",
                table: "t_e_utilisateur_utl",
                newName: "IX_t_e_utilisateur_utl_utl_email");
        }
    }
}
