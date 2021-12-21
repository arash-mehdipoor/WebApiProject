using Domain.Users;
using System;
using System.Collections.Generic;

namespace Application.Interfaces.Users
{
    public interface IUserRepasitory
    {
        #region Get
        User GetUser(Guid userId);
        IEnumerable<User> GetAll();
        #endregion
        #region Exists
        User UserLogin(string Username, string Password);
        #endregion
    }
}
