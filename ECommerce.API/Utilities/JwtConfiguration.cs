using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Entities.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Utilities
{
    public static class JwtConfiguration
    {
     //   private static JwtConfigurationModel ConfigurationModel;
        public static void AddJwtAuthentication(this IServiceCollection services, SiteSettings siteSettings)
        {
            //ConfigurationModel = new JwtConfigurationModel();

            ////configuration.GetSection("IdentitySetting")
            ////    .Bind(ConfigurationModel);

            var s = siteSettings.IdentitySetting;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretKey = Encoding.UTF8.GetBytes(siteSettings.IdentitySetting.IdentitySecretKey);
                var encryptionKey = Encoding.UTF8.GetBytes(siteSettings.IdentitySetting.EncryptKey);

                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero, // default: 5 min
                    RequireSignedTokens = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true, //default : false
                    ValidAudience = siteSettings.IdentitySetting.Audience,

                    ValidateIssuer = true, //default : false
                    ValidIssuer = siteSettings.IdentitySetting.Issuer,

                    TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = async context =>
                    {
                     
                        if (context.Exception != null)
                            throw new CustomException(HttpStatusCode.Unauthorized);
                    },
                    OnTokenValidated = async context =>
                    {
                        var claimsIdentity = context.Principal?.Identity as ClaimsIdentity;
                        if (claimsIdentity?.Claims.Any() != true)
                            context.Fail("This token has no claims.");

                        var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);

                        //Find user and token from database and perform your custom validation
                        var userId =claimsIdentity.GetUserId<string>();

                        //var mustChangePassword = claimsIdentity.MustChangePassword();


                        //if (mustChangePassword.HasValue && mustChangePassword.Value && (context.Request.Path != "/api/v1/users/change-password"
                        //                           && context.Request.Method != "PUT"))
                        //{
                        //    context.Fail(new CustomException(CustomHttpStatus.MustChangeDefaultPassword));
                        //    return;
                        //}

                        if (!claimsIdentity.IsUserActive())
                            context.Fail("User is not active.");

                        context.Success();
                    },
                    OnChallenge = async context =>
                    {

                        switch (context.AuthenticateFailure)
                        {
                            case SecurityTokenExpiredException:

                                context.Response.StatusCode = (int)CustomHttpStatus.TokenExpired;
                                await context.Response.WriteAsJsonAsync(new ApiResult()
                                {
                                    Messages = new List<string> {CustomHttpStatus.TokenExpired.GetDescription()},
                                    Status = context.Response.StatusCode
                                });
                                break;

                            case CustomException exception:

                                context.Response.StatusCode = exception.Status;
                                await context.Response.WriteAsJsonAsync(new ApiResult()
                                {
                                    Status = exception.Status,
                                    Messages = new List<string> {exception.Message}
                                });

                                break;

                            default:
                                context.Response.StatusCode = 401;
                                await context.Response.WriteAsJsonAsync(new ApiResult()
                                {
                                    Status = context.Response.StatusCode
                                });
                                break;
                        }

                        context.HandleResponse();

                    }
                };
            });
        }
    }
}
