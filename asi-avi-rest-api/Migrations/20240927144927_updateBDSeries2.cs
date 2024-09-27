using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asi_avi_rest_api.Migrations
{
    /// <inheritdoc />
    public partial class updateBDSeries2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "utl_rue",
                schema: "public",
                table: "t_e_utilisateur_utl",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "utl_rue",
                schema: "public",
                table: "t_e_utilisateur_utl");
        }
    }
}
