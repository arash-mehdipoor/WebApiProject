using Application.Interfaces.Dapper;
using Application.Interfaces.Token;
using Application.Interfaces.Users;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
namespace EndPoint.Api.Controllers
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserTokenRepasitory userTokenRepository;
        private readonly IUserRepasitory userServices;


        public UserController(
            IUserTokenRepasitory userTokenRepository,
            IUserRepasitory userServices)
        {
            this.userTokenRepository = userTokenRepository;
            this.userServices = userServices;
        }

        [HttpPost]
        [Route("GetUsers")]
        public IActionResult GetUsers([FromHeader] string token)
        {
            var userToken = userTokenRepository.FindUserToken(token);
            if (userToken == null)
                return Unauthorized();
            if (userToken.RefreshTokenExp < DateTime.Now)
                return Unauthorized("Token Expire");
            IEnumerable<User> users = userServices.GetAll().Select(u => new User
            {
                Id = u.Id,
                UserName = u.UserName
            });
            if (users.Any())
                return Ok(users);
            return Ok("کاربری پیدا نشد");
        }
    }
}
