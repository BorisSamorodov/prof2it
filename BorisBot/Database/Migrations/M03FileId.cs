using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BorisBot.Database.Migrations;

[DbContext(typeof(BorisBotContext))]
[Migration("M03FileId")]
public class M03FileId : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>("TelegramFileId", "Articles");
    }
}