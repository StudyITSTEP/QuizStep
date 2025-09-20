using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using QuizStep.Core.Entities;
using QuizStep.Core.Interfaces;
using QuizStep.Infrastructure.Config;

namespace QuizStep.Infrastructure.Repositories;

public class JwtProvider: IJwtProvider
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    
    public string GetJwt(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
        };
        
        var jwt = new JwtSecurityToken(
            issuer: JwtConfig.ISSUER,
            audience: JwtConfig.AUDIENCE,
                
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)),
            signingCredentials: new SigningCredentials(JwtConfig.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public string GenerateJwtToken(User user)
    {
        throw new NotImplementedException();
    }


    public bool ValidateToken(string token)
    {
        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = JwtConfig.ISSUER,
                ValidateAudience = true,
                ValidAudience = JwtConfig.AUDIENCE,
                ValidateLifetime = true,
                IssuerSigningKey = JwtConfig.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };

            ClaimsPrincipal principal =
                _jwtSecurityTokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            
            if(principal.Identity != null) return principal.Identity.IsAuthenticated;
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}