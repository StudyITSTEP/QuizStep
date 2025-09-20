using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuizStep.Core.Entities;
using QuizStep.Core.Primitives;

namespace QuizStep.Core.Interfaces;

public interface IUser
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(string id);
    Task<bool> IsLockedOutAsync(User user);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<string> GetAccessTokenAsync(User user);
    Task<User?> GetUserAsync();
    Task<IdentityResult> CreateUserAsync(User user, string password);
    Task<IdentityResult> AddToRoleAsync(User user, string role);
    Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles);
    Task<IList<string>> GetRolesAsync(User user);
    Task<Result> SignInAsync(User user, string password);
    Task<string> GenerateEmailConfirmationTokenAsync(User user);
    Task<Result> ConfirmEmailAsync(User user, string token);
}