using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuizStep.Core.Entities;
using QuizStep.Core.Interfaces;

namespace QuizStep.Infrastructure.Repositories;

public class UserRepository: IUser
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtProvider _jwtProvider;
    public UserRepository(UserManager<User> userManager,  IHttpContextAccessor httpContextAccessor, IJwtProvider jwt)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _jwtProvider = jwt;
    }
    
    public Task<User?> GetUserByEmailAsync(string email) => _userManager.FindByEmailAsync(email);

    public Task<User?> GetUserByIdAsync(string id) => _userManager.FindByIdAsync(id);

    public Task<bool> IsLockedOutAsync(User user) => _userManager.IsLockedOutAsync(user);
    
    public Task<bool> CheckPasswordAsync(User user, string password) => _userManager.CheckPasswordAsync(user, password);

    public Task<string> GetAccessTokenAsync(User user) => Task.Run(() => _jwtProvider.GetJwt(user));

    public Task<User?> GetUserAsync() => _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

    public Task<IdentityResult> CreateUserAsync(User user, string password) => _userManager.CreateAsync(user, password);

    public Task<IdentityResult> AddToRoleAsync(User user, string role) => _userManager.AddToRoleAsync(user, role);

    public Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles) => _userManager.AddToRolesAsync(user, roles);

    public Task<IList<string>> GetRolesAsync(User user) => _userManager.GetRolesAsync(user);
    
    public async Task<bool> SignInAsync(User user, string password)
    {
        var  result = await _userManager.CheckPasswordAsync(user, password);
        return result;
    }
}