﻿using System.Security.Claims;

namespace s20601.Services;
public interface ICurrentUserService
{
    Task<string?> GetAuthenticatedUserId();
    Task<ClaimsPrincipal?> GetAuthenticatedUser();
}
