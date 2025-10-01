using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizStep.Core.Entities;
using QuizStep.Core.Errors.UserErrors;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;
using QuizStep.Infrastructure.Data;
using QuizStep.Infrastructure.Services;

namespace QuizStep.Infrastructure.Repositories;

public class UserRepository : IUser
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationContext _applicationContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtProvider _jwtProvider;

    public UserRepository(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IJwtProvider jwt,
        ApplicationContext applicationContext)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _jwtProvider = jwt;
        _applicationContext = applicationContext;
    }

    public Task<User?> GetUserByEmailAsync(string email) => _userManager.FindByEmailAsync(email);

    public Task<User?> GetUserByIdAsync(string id) => _userManager.FindByIdAsync(id);

    public Task<bool> IsLockedOutAsync(User user) => _userManager.IsLockedOutAsync(user);

    public Task<bool> CheckPasswordAsync(User user, string password) => _userManager.CheckPasswordAsync(user, password);

    public Task<string> GetAccessTokenAsync(User user) => Task.Run(() => _jwtProvider.GetJwt(user));

    public Task<User?> GetUserAsync() => _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

    public Task<IdentityResult> CreateUserAsync(User user, string password) => _userManager.CreateAsync(user, password);

    public Task<IdentityResult> AddToRoleAsync(User user, string role) => _userManager.AddToRoleAsync(user, role);

    public Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles) =>
        _userManager.AddToRolesAsync(user, roles);

    public Task<IList<string>> GetRolesAsync(User user) => _userManager.GetRolesAsync(user);

    public async Task<Result> SignInAsync(User user, string password)
    {
        var result = await _userManager.CheckPasswordAsync(user, password);
        return Result.Success();
    }

    public Task<string> GenerateEmailConfirmationTokenAsync(User user) =>
        _userManager.GenerateEmailConfirmationTokenAsync(user);

    public async Task<Result> ConfirmEmailAsync(User user, string token)
    {
        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            return Result.Success();
        }

        return Error.Failed;
    }

    public async Task<string?> GenerateRefreshTokenAsync(string userId, TimeSpan? expiration = null)
    {
        var salt = TokenHasher.GenerateToken(16);
        var token = TokenHasher.GenerateToken(64);
        var hashToken = TokenHasher.HashToken(token, salt);

        var refreshToken = new RefreshToken
        {
            UserId = userId,
            Salt = salt,
            Hash = hashToken,
            Expires = DateTime.UtcNow.Add(expiration ?? TimeSpan.FromHours(1))
        };

        _applicationContext.RefreshTokens.Add(refreshToken);
        await _applicationContext.SaveChangesAsync();
        return token;
    }

    public async Task RevokeRefreshTokensAsync(string userId)
    {
        var token = await _applicationContext.RefreshTokens.FirstOrDefaultAsync(t => t.UserId == userId);
        if (token == null) return;
        _applicationContext.RefreshTokens.Remove(token);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task RevokeRefreshTokenAsync(int tokenId)
    {
        var token = await _applicationContext.RefreshTokens.FirstOrDefaultAsync(t => t.Id == tokenId);
        if (token == null) return;
        _applicationContext.RefreshTokens.Remove(token);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task<string?> RenewRefreshTokenAsync(string userId, string refreshToken, TimeSpan? expiration = null)
    {
        var token = await _applicationContext.RefreshTokens
            .Where(t => t.UserId == userId)
            .ToListAsync();

        if (token.Count <= 0)
            return null;
        bool validToken = false;

        foreach (var r in token)
        {
            if (r.Expires > DateTime.UtcNow)
            {
                if (TokenHasher.VerifyToken(refreshToken, r.Salt, r.Hash))
                {
                    validToken = true;
                    try
                    {
                        await RevokeRefreshTokenAsync(r.Id);

                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                }
            }
        }

        if (!validToken) return null;

        return await GenerateRefreshTokenAsync(userId, expiration);
    }
}