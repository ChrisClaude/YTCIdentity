using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Models;
using YTCIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace YTCIdentity;
public class CustomProfileService : ProfileService<ApplicationUser>
{
    public CustomProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory) : base(userManager, claimsFactory)
    {
    }

    protected override async Task GetProfileDataAsync(ProfileDataRequestContext context, ApplicationUser user)
    {
        var principal = await GetUserClaimsAsync(user);
        var id = (ClaimsIdentity)principal.Identity;
        if (!string.IsNullOrEmpty(user.ProfileImage))
        {
            id.AddClaim(new Claim("profile_image", user.ProfileImage));
        }

        context.AddRequestedClaims(principal.Claims);
    }
}

