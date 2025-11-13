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
        var claimsMaybe = await GetAuth();

        if (!claimsMaybe.HasValue())
            return Maybe<ClaimData>.None;

        var claims = claimsMaybe.GetValue();

        var name = (string?)GetClaimValue<string>(claims, ClaimTypes.Name);
        var userId = (int?)GetClaimValue<int?>(claims, ClaimTypes.NameIdentifier);

        return Maybe<ClaimData>.Some(new ClaimData { Name = name, UserId = userId });
    }

    public async Task<Maybe<IEnumerable<Claim>>> GetAuth()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return Maybe<IEnumerable<Claim>>.None;

        var claims = user.Claims;
        if (!claims.Any())
            return Maybe<IEnumerable<Claim>>.None;

        return Maybe<IEnumerable<Claim>>.Some(claims);
    }

    public object? GetClaimValue(IEnumerable<Claim> claims, string claimType)
    {
        var claim = claims.FirstOrDefault(c => c.Type == claimType);
        if (claim == null)
            return default;

        return claim.Value;
    }

    [Obsolete("Use GetClaimValue without generic parameter")]
    public object? GetClaimValue<T>(IEnumerable<Claim> claims, string claimType)
    {
        return GetClaimValue(claims, claimType);
    }

    public class ClaimData
    {
        public string? Name { get; set; }
        public int? UserId { get; set; }
    }

    public async Task<int?> GetUserId()
    {
        var maybeClaims = await GetAuth();
        if (!maybeClaims.HasValue())
        {
            return null;
        }
        // TODO: naming
        var vv = GetClaimValue(maybeClaims.GetValue(), ClaimTypes.NameIdentifier);
        var sv = vv?.ToString();
        int? userId = int.Parse(sv!);
        return userId;
    }
}

