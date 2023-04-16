using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSet.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillLevelConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Skill_Level",
                table: "Skills",
                sql: "[Level] > 0 AND [Level] <= 10");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Skill_Level",
                table: "Skills");
        }
    }
}
