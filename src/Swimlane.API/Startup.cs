using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Swimlane.Application;
using Swimlane.Core;
using Swimlane.Infrastructure;
using System.Text;



namespace Swimlane.API
{
    public static class DependencyInject
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.InjectCore();
            builder.Services.InjectApplication();
            builder.Services.InjectInfrastructure(builder.Configuration);
           // builder.Services.InjectSwagger();

            builder.Services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
                };
            });
        }

        //public static IServiceCollection InjectSwagger(this IServiceCollection services)
        //{
        //    services.AddSwaggerGen(x =>
        //    {
        //        x.SwaggerDoc("v1", new OpenApiInfo
        //        {
        //            Title = "Swimlane API",
        //            Version = "v1",
        //            Description = "Swimlane API"
        //        });

        //        x.EnableAnnotations();

        //        x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //        {
        //            Name = "Authorization",
        //            Type = SecuritySchemeType.Http,
        //            Scheme = "Bearer",
        //            BearerFormat = "JWT",
        //            In = ParameterLocation.Header,
        //            Description = "Enter: Bearer {your JWT token}"
        //        });

        //        x.AddSecurityRequirement(new OpenApiSecurityRequirement
        //    {
        //        {
        //            new OpenApiSecurityScheme
        //            {
        //                Reference = new OpenApiReference
        //                {
        //                    Type = ReferenceType.SecurityScheme,
        //                    Id = "Bearer"
        //                }
        //            },
        //            Array.Empty<string>()
        //        }
        //    });
        //    });

        //    return services;
        //}

    }
}
