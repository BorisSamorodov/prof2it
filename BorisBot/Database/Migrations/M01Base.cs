using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BorisBot.Database.Migrations;

[DbContext(typeof(BorisBotContext))]
[Migration("M01Base")]
public class M01Base : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable("Authors", cb => new
            {
                Id = cb.Column<long>(),
                UserName = cb.Column<string>(),
                RealName = cb.Column<string>()
            },
            constraints: ctb =>
            {
                ctb.PrimaryKey("PK_authors", x => x.Id);
            });

        migrationBuilder.CreateTable("ScientificJournals", cb => new
            {
                Id = cb.Column<Guid>(),
                Name = cb.Column<string>()
            },
            constraints: ctb =>
            {
                ctb.PrimaryKey("PK_scientificJournals", x => x.Id);
            });
        
        migrationBuilder.CreateTable("JournalIssues", cb => new
            {
                Id = cb.Column<Guid>(),
                ScientificJournalId = cb.Column<Guid>(),
                Date = cb.Column<DateTime>()
            },
            constraints: ctb =>
            {
                ctb.PrimaryKey("PK_journalIssues", x => x.Id);
                ctb.ForeignKey("FK_journalIssues_scientificJournals", x => x.ScientificJournalId, "ScientificJournals", "Id");
            });
        
        migrationBuilder.CreateTable("Articles", cb => new
            {
                Id = cb.Column<Guid>(),
                JournalIssueId = cb.Column<Guid>(),
                Title = cb.Column<string>(),
                Contents = cb.Column<byte[]>()
            },
            constraints: ctb =>
            {
                ctb.PrimaryKey("PK_articles", x => x.Id);
                ctb.ForeignKey("FK_articles_journalIssues", x => x.JournalIssueId, "JournalIssues", "Id");
            });
        
        migrationBuilder.CreateTable("ArticleAuthors", cb => new
            {
                Id = cb.Column<Guid>(),
                ArticleId = cb.Column<Guid>(),
                AuthorId = cb.Column<long>(),
            },
            constraints: ctb =>
            {
                ctb.PrimaryKey("PK_articleAuthors", x => x.Id);
                ctb.ForeignKey("FK_articleAuthors_articles", x => x.ArticleId, "Articles", "Id");
                ctb.ForeignKey("FK_articleAuthors_authors", x => x.AuthorId, "Authors", "Id");
            });
        
        migrationBuilder.CreateTable("Editors", cb => new
            {
                Id = cb.Column<Guid>(),
                AuthorId = cb.Column<long>(),
                ScientificJournalId = cb.Column<Guid>()
            },
            constraints: ctb =>
            {
                ctb.PrimaryKey("PK_editors", x => x.Id);
                ctb.ForeignKey("FK_editors_authors", x => x.AuthorId, "Authors", "Id");
                ctb.ForeignKey("FK_editors_scientificJournals", x => x.ScientificJournalId, "ScientificJournals", "Id");
            });
        }
}