using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PracticeAPI.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "Rezbirp";
        const string KEY = "aeo_hB%&>+u1/TGCyXR&0.e{7vWl%[5{";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
