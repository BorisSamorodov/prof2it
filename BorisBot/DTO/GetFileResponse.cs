namespace BorisBot.DTO;

public class GetFileResponse
{
    public bool Ok { get; set; }
    public GetFileResponseResult Result { get; set; } = new();
}

