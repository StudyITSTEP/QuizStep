using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace QuizStep.Infrastructure.Config;

public class JwtConfig
{
    public static string ISSUER =  "Issuer";
    public static string AUDIENCE =  "Audience";

    private static string KEY = "this_is_a_very_long_super_secret_key_1234567890";

    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}