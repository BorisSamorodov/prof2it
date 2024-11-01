namespace BorisBot.Extensions;

public static class StringExtensions
{
    public static DateTime? AsUtc(this string data)
    {
        try
        {
            var split = data.Split('.');
            return new DateTime(int.Parse(split[2]), int.Parse(split[1]), int.Parse(split[0]), 0, 0, 0, DateTimeKind.Utc);

        }
        catch (Exception)
        {
            return null;
        }
    }
}