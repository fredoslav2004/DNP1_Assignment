using System;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using static Utils.Functional;

namespace BlazorApp.Services;

public class ClaimService(AuthenticationStateProvider authStateProvider)
{
    private readonly AuthenticationStateProvider _authStateProvider = authStateProvider;

    public async Task<Maybe<ClaimData>> GetClaimValues()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return Maybe<ClaimData>.None;

        var claims = user.Claims;
        if (!claims.Any())
            return Maybe<ClaimData>.None;

        var name = user.Identity.Name;

        var idClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        var userId = (idClaim != null && int.TryParse(idClaim.Value, out var r)) ? r : -1;

        return Maybe<ClaimData>.Some(new ClaimData { Name = name, UserId = userId });
    }

    public class ClaimData
    {
        public string? Name { get; set; }
        public int? UserId { get; set; }
    }
}

