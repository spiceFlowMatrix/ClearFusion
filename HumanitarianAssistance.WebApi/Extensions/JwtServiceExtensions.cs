using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HumanitarianAssistance.WebApi.Extensions
{
    public static class JwtServiceExtensions
    {
        private static SymmetricSecurityKey _signingKey;

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"]));

            // jwt token configuration
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            services.Configure<JwtIssuerOptions>(options =>
           {
               options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
               options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
               options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
           });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["JwtIssuerOptions:Issuer"],
                    ValidAudience = Configuration["JwtIssuerOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

    }
}