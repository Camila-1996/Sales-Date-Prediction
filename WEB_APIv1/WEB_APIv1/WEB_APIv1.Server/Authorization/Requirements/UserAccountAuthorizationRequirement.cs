﻿// ---------------------------------------
// ---
// ---
// ---
// ---------------------------------------

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WEB_APIv1.Core.Services.Account;
using WEB_APIv1.Server.Services;

namespace WEB_APIv1.Server.Authorization.Requirements
{
    public class UserAccountAuthorizationRequirement(string operationName) : IAuthorizationRequirement
    {
        public string OperationName { get; private set; } = operationName;
    }

    public class ViewUserAuthorizationHandler : AuthorizationHandler<UserAccountAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, UserAccountAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null || requirement.OperationName != UserAccountManagementOperations.ReadOperationName)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaims.Permission, ApplicationPermissions.ViewUsers)
                || GetIsSameUser(context.User, targetUserId))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

        private static bool GetIsSameUser(ClaimsPrincipal user, string targetUserId)
        {
            if (string.IsNullOrWhiteSpace(targetUserId))
                return false;

            return Utilities.GetUserId(user) == targetUserId;
        }
    }

    public class ManageUserAuthorizationHandler : AuthorizationHandler<UserAccountAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, UserAccountAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null ||
                (requirement.OperationName != UserAccountManagementOperations.CreateOperationName &&
                 requirement.OperationName != UserAccountManagementOperations.UpdateOperationName &&
                 requirement.OperationName != UserAccountManagementOperations.DeleteOperationName))
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaims.Permission, ApplicationPermissions.ManageUsers)
                || GetIsSameUser(context.User, targetUserId))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

        private static bool GetIsSameUser(ClaimsPrincipal user, string targetUserId)
        {
            if (string.IsNullOrWhiteSpace(targetUserId))
                return false;

            return Utilities.GetUserId(user) == targetUserId;
        }
    }
}