using Application.Interfaces.Token;
using Application.Interfaces.Users;
using Domain.Users;
using EndPoint.Api.Models.Dto;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EndPoint.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepasitory userRepository;
        private readonly IUserTokenRepasitory userTokenRepository;

        public AccountController(
            IUserRepasitory userRepository,
            IUserTokenRepasitory userTokenRepository,
            IConfiguration configuration)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
            this.userTokenRepository = userTokenRepository;
        }

        [HttpPost]
        public IActionResult Post(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return BadRequest();
            User user = userRepository.UserLogin(userName, password);
            if (user != null)
            {
                UserToken userToken = userTokenRepository.GetUserTokenByUserId(user.Id);
                if (userToken != null && userToken.RefreshTokenExp > DateTime.Now)
                {
                    return Ok(new { userToken.Token, userToken.RefreshToken });
                }
                return Ok(CreateToken(user));
            }
            return Unauthorized();
        }



        private LoginResultDto CreateToken(User user)
        {
            var claims = new List<Claim>
                {
                    new Claim ("UserId", user.Id.ToString()),
                    new Claim ("UserName",  user.UserName),
                };
            string key = configuration["JWtConfig:Key"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenexp = DateTime.Now.AddMinutes(int.Parse(configuration["JWtConfig:expires"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWtConfig:issuer"],
                audience: configuration["JWtConfig:audience"],
                expires: tokenexp,
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: credentials
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = Guid.NewGuid();

            userTokenRepository.AddToken(new UserToken()
            {
                TokenExp = tokenexp,
                Token = jwtToken,
                UserId = user.Id,
                RefreshToken = refreshToken.ToString(),
                RefreshTokenExp = DateTime.Now.AddDays(30)
            });

            return new LoginResultDto()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.ToString()
            };
        }
    }
}
