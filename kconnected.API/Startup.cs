using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using kconnected.API.Data;
using kconnected.API.Entities;
using kconnected.API.Repositories;
using kconnected.API.Services;
using kconnected.API.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace kconnected.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers(o => o.Filters.Add(new AuthorizeFilter()));
            //Database context
            services.AddDbContext<kconnectedAPIDbContext>();

            services.AddTransient<DbContext>(options => {
                return new kconnectedAPIDbContext();
            });

            //Repository layer dependencies
            services.AddScoped<IUserRepository,InMemoryUserRepository>();
            services.AddScoped<ISkillRepository,InMemorySkillRepository>();
            //Service layer dependencies
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<ISkillService,SkillService>();

            services.AddScoped<IAuthenticationManager,JWTAuthenticationManager>();


            services.AddControllers(options => {
                 options.SuppressAsyncSuffixInActionNames = false;
                 options.Filters.Add(new AuthorizeFilter());
                 
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "kconnected.API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "kconnected",    
                    ValidAudience = "User",    
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("C1CF4B7DC4C4175B6618DE4F55CA4")) 
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Users", new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireClaim(ClaimTypes.Role, "User")
                    .Build());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DisplayRequestDuration();
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "kconnected.API v1");

                    
                } );


            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
