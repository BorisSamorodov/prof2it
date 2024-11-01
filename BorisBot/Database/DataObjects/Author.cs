using System.ComponentModel.DataAnnotations;

namespace BorisBot.Database.DataObjects;

public class Author
{
    [Key]
    public long Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string RealName { get; set; } = string.Empty;

    public string? UserState { get; set; }
}