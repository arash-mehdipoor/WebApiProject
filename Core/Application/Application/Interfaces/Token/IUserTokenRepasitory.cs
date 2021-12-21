using Domain.Users;
using System;

namespace Application.Interfaces.Token
{
    public interface IUserTokenRepasitory
    {
        #region Add
        void AddToken(UserToken userToken);
        #endregion
        #region Get
        UserToken FindUserToken(string userToken);
        UserToken GetUserTokenByUserId(Guid userId);
        #endregion
    }
}
