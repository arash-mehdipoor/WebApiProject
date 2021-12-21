using Application.Interfaces.Dapper;
using Application.Interfaces.Users;
using Dapper;
using Domain.Users;
using Infrastructure.Context;
using Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Data;

namespace Infrastructure.Services.Users
{
    public class UserRepository : IUserRepasitory
    {
        private readonly DataBaseContext context;
        private readonly IDapperRepository dapper;

        public UserRepository(DataBaseContext context, IDapperRepository dapper)
        {
            this.context = context;
            this.dapper = dapper;
        }

        public IEnumerable<User> GetAll()
        {
            var db = dapper.GetDbconnection();
            var result = db.Query<User>("GetAllUser");
            return result;

            //return context.Users.ToList();
        }

        public User GetUser(Guid userId)
        {
            var db = dapper.GetDbconnection();
            var user = db.QuerySingleOrDefault<User>("GetUser",
                param: new { @userId = userId }, commandType: CommandType.StoredProcedure);

            //var user = context.Users.SingleOrDefault(p => p.Id == userId);
            return user;
        }

        public User UserLogin(string username, string password)
        {
            SecurityHelper securityHelper = new SecurityHelper();
            var passwordHash = securityHelper.Getsha256Hash(password);

            var db = dapper.GetDbconnection();
            var user = db.QuerySingleOrDefault<User>("ExistsUserLogin",
                param: new { @username = username, @password = passwordHash }, commandType: CommandType.StoredProcedure);

            //var user = context.Users.SingleOrDefault(u => u.Name == username && u.Password == password);
            return user;
        }




    }
}
