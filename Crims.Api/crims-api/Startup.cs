using Crims.Authentication;
using Crims.Authentication.Middlewares;
using Crims.Core;
using Crims.Data;
using Crims.Data.Persistence;
using crims_api.Middlewares;
using crims_api.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace crims_api
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHealthChecks("/health");

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<JwtMiddleware>();

            if (!env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{GetType().Name} API");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(configure => configure.MapControllers());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            //services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
            //services.AddFluentValidationClientsideAdapters();

            services.AddCore(Configuration);
            services.AddInfrastructure(Configuration);
            services.AddDomain(Configuration);
            services.AddCrimsAuthorization(Configuration);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });
            services.AddEndpointsApiExplorer();
            services.ConfigureSwagger();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // services.AddHangfire(
            //     config =>
            //     {
            //         config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
            //         config.UseSimpleAssemblyNameTypeSerializer();
            //         config.UseRecommendedSerializerSettings();
            //         config.UsePostgreSqlStorage(c => c.UseNpgsqlConnection(Environment.GetEnvironmentVariable("DATABASE_CONNECTION")));
            //     });
            // services.AddHangfireServer();
        }

    }
}
