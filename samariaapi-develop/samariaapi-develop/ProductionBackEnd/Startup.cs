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

            #region ���U�A��
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

            #region �]�wIdentity
            // Identity�]�w
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            })
            // �o��O�ϥ�Core���ܭn�]�w�n�ޥΪ��\��
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddRoleValidator<RoleValidator<AppRole>>()
            // AddIdentity �� AddIdentityCore���n�[
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            #endregion

            #region �]�wJWT����
            // JWT�]�w
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // ���ҵo���
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JwtSettings:Issuer"],
                        // ���Ҩ����A�q�`���ݭn
                        ValidateAudience = false,

                        // �ݭn�L���ɶ�(�ݻݨD)
                        //RequireExpirationTime = true,

                        // �@��ڭ̳��|���� Token �����Ĵ���
                        ValidateLifetime = true,
                        // �p�G Token ���]�t key �~�ݭn���ҡA�@�볣�u��ñ���Ӥw
                        ValidateIssuerSigningKey = true,
                        // ����key
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:TokenKey"]))

                    };
                });
            #endregion

            #region �[�JSwagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ProductionBackEnd",
                    Version = "v1",
                    Contact = new OpenApiContact { Name = "Our Web WebSite", Email = "b0972834939@gmail.com" },
                    Description = "Our API",
                });

                // �[�Jxml�ɮר�swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);

                #region �[�J���v��
                var openApiSecurity = new OpenApiSecurityScheme
                {
                    Description = "JWT Authentication�APlease enter the \"Bearer {token}\"�Atoken value get from login api ",
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
