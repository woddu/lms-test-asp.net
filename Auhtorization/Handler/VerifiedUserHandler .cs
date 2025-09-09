using lms_test1.Areas.Identity.Data;
using lms_test1.Auhtorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace lms_test1.Auhtorization.Handler;

public class VerifiedUserHandler : AuthorizationHandler<VerifiedUserRequirement>
{
    private readonly UserManager<LMSUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public VerifiedUserHandler(UserManager<LMSUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, VerifiedUserRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var user = await _userManager.GetUserAsync(context.User);

        if (user != null && await _userManager.IsEmailConfirmedAsync(user))
        {
            context.Succeed(requirement);
        }
    }
}
