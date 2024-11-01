using System.ComponentModel.DataAnnotations;

namespace BorisBot.Database.DataObjects;

public class ArticleAuthor
{
    [Key]
    public Guid Id { get; set; }

    public Article Article { get; set; } = new();

    public Author Author { get; set; } = new();
}