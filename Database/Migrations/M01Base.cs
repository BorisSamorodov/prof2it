using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BorisBot.Database.Migrations;

[DbContext(typeof(BorisBotContext))]
[Migration("M01Base")]
public class M01Base : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable("authors", cb => new
            {
                id = cb.Column<long>(),
                userName = cb.Column<string>(),
                realName = cb.Column<string>()
            },
            constraints: ctb =>
            {
                ctb.PrimaryKey("PK_authors", x => x.id);
            });

        migrationBuilder.CreateTable("scientificJournals", cb => new
            {
                id = cb.Column<Guid>(),
                name = cb.Column<string>()
            },
            constraints: ctb =>
            {
                ctb.PrimaryKey("PK_scientificJournals", x => x.id);
            });
        
        migrationBuilder.CreateTable("journalIssues", cb => new
            {
                id = cb.Column<Guid>(),
                scientificJournalId = cb.Column<Guid>(),
                date = cb.Column<DateTime>()
            },
            constraints: ctb =>
            {
                ctb.PrimaryKey("PK_journalIssues", x => x.id);
                ctb.ForeignKey("FK_journalIssues_scientificJournals", x => x.scientificJournalId, "scientificJournals", "id");
            });
        
        migrationBuilder.CreateTable("articles", cb => new
            {
                id = cb.Column<Guid>(),
                journalIssueId = cb.Column<Guid>(),
                title = cb.Column<string>(),
                contents = cb.Column<byte[]>()
            },
            constraints: ctb =>
            {
                ctb.PrimaryKey("PK_articles", x => x.id);
                ctb.ForeignKey("FK_articles_journalIssues", x => x.journalIssueId, "journalIssues", "id");
            });
        
        migrationBuilder.CreateTable("articleAuthors", cb => new
            {
                id = cb.Column<Guid>(),
                articleId = cb.Column<Guid>(),
                authorId = cb.Column<long>(),
            },
            constraints: ctb =>
            {
                ctb.PrimaryKey("PK_articleAuthors", x => x.id);
                ctb.ForeignKey("FK_articleAuthors_articles", x => x.articleId, "articles", "id");
                ctb.ForeignKey("FK_articleAuthors_authors", x => x.authorId, "authors", "id");
            });
        
        migrationBuilder.CreateTable("editors", cb => new
            {
                id = cb.Column<Guid>(),
                authorId = cb.Column<long>(),
                scientificJournalId = cb.Column<Guid>()
            },
            constraints: ctb =>
            {
                ctb.PrimaryKey("PK_editors", x => x.id);
                ctb.ForeignKey("FK_editors_authors", x => x.authorId, "authors", "id");
                ctb.ForeignKey("FK_editors_scientificJournals", x => x.scientificJournalId, "scientificJournals", "id");
            });
        }
}