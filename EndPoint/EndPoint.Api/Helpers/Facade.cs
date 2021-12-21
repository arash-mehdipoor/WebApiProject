using Application.Interfaces.Dapper;
using Application.Interfaces.Token;
using Application.Interfaces.Users;
using Infrastructure.Context;
using Infrastructure.Services.Dapper;
using Infrastructure.Services.Token;
using Infrastructure.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace EndPoint.Api.Helpers
{
    public static class Facade
    {
        public static IServiceCollection AddServiceProject(this IServiceCollection services,
           IConfiguration configuration)
        {
            #region NewtonsoftJson
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            #endregion
            #region RegisterServices
            services.AddScoped<IUserTokenRepasitory, UserTokenRepository>();
            services.AddScoped<IUserRepasitory, UserRepository>();
            services.AddScoped<IDapperRepository, DapperRepository>();
            #endregion
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EndPoint.Api", Version = "v1" });
            });
            #endregion
            #region AuthenticationANDJwtBearer
            services.AddAuthentication(Options =>
            {
                Options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(configureOptions =>
           {
               configureOptions.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidIssuer = configuration["JWtConfig:issuer"],
                   ValidAudience = configuration["JWtConfig:audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWtConfig:Key"])),
                   ValidateIssuerSigningKey = true,
                   ValidateLifetime = true,
               };
               configureOptions.SaveToken = true;
               configureOptions.Events = new JwtBearerEvents
               {
                   OnTokenValidated = context =>
                   {
                       //log
                       var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorRepasitory>();
                       return tokenValidatorService.Execute(context);
                   },
               };

           });
            #endregion
            #region ApiVersioning
            services.AddApiVersioning(Options =>
            {
                Options.AssumeDefaultVersionWhenUnspecified = true;
                Options.DefaultApiVersion = new ApiVersion(1, 0);
                Options.ReportApiVersions = true;
            });
            #endregion
            #region ConnectionString
            string connection = "Data Source=.;Initial Catalog=WebApiProject;Integrated Security=True;MultipleActiveResultSets=true";
            services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option => option.UseSqlServer(connection));
            #endregion
            return services;
        }
    }
}
