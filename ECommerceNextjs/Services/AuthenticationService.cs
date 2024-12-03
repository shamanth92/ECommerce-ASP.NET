using FirebaseAdmin.Auth;

namespace ECommerceNextjs.Services
{
    public class AuthenticationService
    {
        public async Task<Boolean> checkAuthToken(string token)
        {
            var decode = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
            if (decode.Uid != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
