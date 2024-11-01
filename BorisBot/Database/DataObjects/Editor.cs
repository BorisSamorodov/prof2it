using System.ComponentModel.DataAnnotations;

namespace BorisBot.Database.DataObjects;

public class Editor
{
    [Key]
    public Guid Id { get; set; }

    public Author Author { get; set; } = new();

    public ScientificJournal Journal { get; set; } = new();
}