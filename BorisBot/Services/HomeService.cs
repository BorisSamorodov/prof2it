using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BorisBot.Database.DataObjects;
using BorisBot.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

namespace BorisBot.Services;

public class HomeService : IHomeService
{
    private readonly BorisBotConfig _config;
    private readonly IContextFactory _contextFactory;

    public HomeService(
        IOptions<BorisBotConfig> config,
        IContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
        _config = config.Value;
    }

    public bool IsHashCorrect(Dictionary<string, string> request, string telegramHash)
    {
        var sortedRequest = request
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);

        var message = string.Join("\n", sortedRequest.Select(x => $"{x.Key}={x.Value}"));
        
        using var hasher = SHA256.Create();
        var keyBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(_config.TelegramToken));

        var messageBytes = Encoding.UTF8.GetBytes(message);
        var hash = new HMACSHA256(keyBytes);
        var computedHash = hash.ComputeHash(messageBytes);
        var computedHex = Convert.ToHexString(computedHash);

        return computedHex.Equals(telegramHash, StringComparison.InvariantCultureIgnoreCase);
    }
    
    public async Task AuthorizeUser(HttpContext httpContext, string userName, long id)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, userName),
            new(ClaimTypes.Role, "User") 
        };
        
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true, 
            ExpiresUtc = DateTime.UtcNow.AddDays(1) 
        };
        
        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        var context = _contextFactory.GetContext();
        if (context.Authors.Any(x => x.Id == id && x.UserName == userName))
        {
            return;
        }

        context.Authors.Add(new Author
        {
            Id = id,
            UserName = userName
        });

        await context.SaveChangesAsync();
    }

    public async Task AuthorizeLocalAdmin(HttpContext httpContext)
    {
        var context = _contextFactory.GetContext();
        var admin = context.Authors.Single(x => x.Id == _config.AdminUserId);
        await AuthorizeUser(httpContext, admin.UserName, admin.Id);
    }
}