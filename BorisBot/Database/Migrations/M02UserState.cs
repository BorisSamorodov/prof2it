using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BorisBot.Database.Migrations;

[DbContext(typeof(BorisBotContext))]
[Migration("M02UserState")]
public class M02UserState : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>("UserState", "Authors", nullable: true);
    }
}