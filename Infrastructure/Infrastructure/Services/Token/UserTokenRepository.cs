using Application.Interfaces.Dapper;
using Application.Interfaces.Token;
using Dapper;
using Domain.Users;
using Infrastructure.Context;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;

namespace Infrastructure.Services.Token
{
    public class UserTokenRepository : IUserTokenRepasitory
    {

        private readonly IDapperRepository dapper;
        private readonly DataBaseContext context;
        public UserTokenRepository(DataBaseContext context, IDapperRepository dapper)
        {
            this.context = context;
            this.dapper = dapper;
        }
        public void AddToken(UserToken userToken)
        {
            var db = dapper.GetDbconnection();
            var userId = Guid.Parse(userToken.UserId.ToString());
            db.Execute("AddUserToken", param:
                new
                {
                    @userId = userId,
                    @token = userToken.Token,
                    @tokenExp = userToken.TokenExp,
                    @refreshToken = userToken.RefreshToken,
                    @refreshTokenExp = userToken.RefreshTokenExp
                }, commandType: CommandType.StoredProcedure);

            //context.UserTokens.Add(userToken);
            //context.SaveChanges();
        }

        public UserToken FindUserToken(string userToken)
        {

            var db = dapper.GetDbconnection();
            var result = db.QuerySingleOrDefault<UserToken>("GetUserToken",
                param: new { @userToken = userToken }, commandType: CommandType.StoredProcedure);


            //var usertoken = context.UserTokens.Include(p => p.User)
            //    .SingleOrDefault(p => p.TokenHash == userToken);
            return result;
        }

        public UserToken GetUserTokenByUserId(Guid userId)
        {
            var db = dapper.GetDbconnection();
            var result = db.QuerySingleOrDefault<UserToken>("GetUser",
                param: new
                {
                    @userId = userId
                }, commandType: CommandType.StoredProcedure);
            return result;
            //return context.UserTokens.SingleOrDefault(t => t.UserId == userId);
        }
    }
}
