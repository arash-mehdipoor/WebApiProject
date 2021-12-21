
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

namespace Application.Interfaces.Token
{
    public interface ITokenValidatorRepasitory
    {
        #region TokenValidator
        Task Execute(TokenValidatedContext context);
        #endregion
    }
}
