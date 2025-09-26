using System.Security.Claims;
using lms_test1.Auhtorization.Requirements;
using lms_test1.Models;
using Microsoft.AspNetCore.Authorization;

namespace lms_test1.Auhtorization.Handler;

public class SameOwnerOfTeacherSubjectHandler : AuthorizationHandler<SameOwnerRequirement, TeacherSubject>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        SameOwnerRequirement requirement,
        TeacherSubject resource)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (resource.TeacherId == userId || context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
