using Infrastructure.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Services.Token
{
    public class TokenValidateRepository
    {
        private UserRepository userRepository;

        public TokenValidateRepository(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task Execute(TokenValidatedContext context)
        {
            var claimsidentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsidentity?.Claims == null || !claimsidentity.Claims.Any())
            {
                context.Fail("claims not found....");
                return;
            }

            var userId = claimsidentity.FindFirst("UserId").Value;
            if (!Guid.TryParse(userId, out Guid userGuid))
            {
                context.Fail("claims not found....");
                return;
            }

            var user = userRepository.GetUser(userGuid);
            if (user.IsActive == false)
            {
                context.Fail("User not Active");
                return;
            }
        }
    }
}
