using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductionBackEnd.Data;
using ProductionBackEnd.Data.AutoMapperProfiles;
using ProductionBackEnd.Interfaces.Repositories.Admin;
using ProductionBackEnd.Interfaces.Repositories.PlatformNews;
using ProductionBackEnd.Interfaces.Repositories.Products;
using ProductionBackEnd.Interfaces.Repositories.User;
using ProductionBackEnd.Interfaces.Utils;
using ProductionBackEnd.Models.User;
using ProductionBackEnd.Repositories.Admin;
using ProductionBackEnd.Repositories.News;
using ProductionBackEnd.Repositories.Products;
using ProductionBackEnd.Repositories.User;
using ProductionBackEnd.Utils;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace ProductionBackEnd
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
            services.AddDbContext<AppDbContext>(options =>
            {
                string mySqlConnectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString));
            });

            #region 註冊服務
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPlatformNewsRepository, PlatformNewsRepository>();
            services.AddScoped<ITokenHelper, TokenHelper>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            #endregion

            services.AddAutoMapper(typeof(AdminProfile).Assembly);
            services.AddAutoMapper(typeof(UserProfile).Assembly);
            services.AddControllers();
            services.AddCors();

            #region 設定Identity
            // Identity設定
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            })
            // 這邊是使用Core的話要設定要引用的功能
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddRoleValidator<RoleValidator<AppRole>>()
            // AddIdentity 或 AddIdentityCore都要加
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            #endregion

            #region 設定JWT驗證
            // JWT設定
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // 驗證發行者
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JwtSettings:Issuer"],
                        // 驗證受眾，通常不需要
                        ValidateAudience = false,

                        // 需要過期時間(看需求)
                        //RequireExpirationTime = true,

                        // 一般我們都會驗證 Token 的有效期間
                        ValidateLifetime = true,
                        // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
                        ValidateIssuerSigningKey = true,
                        // 驗證key
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:TokenKey"]))

                    };
                });
            #endregion

            #region 加入Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ProductionBackEnd",
                    Version = "v1",
                    Contact = new OpenApiContact { Name = "Our Web WebSite", Email = "b0972834939@gmail.com" },
                    Description = "Our API",
                });

                // 加入xml檔案到swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);

                #region 加入授權鎖
                var openApiSecurity = new OpenApiSecurityScheme
                {
                    Description = "JWT Authentication，Please enter the \"Bearer {token}\"，token value get from login api ",
                    Scheme = "Bearer",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };
                c.AddSecurityDefinition("oauth2", openApiSecurity);
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                #endregion
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebSite v1"));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("*"));
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
