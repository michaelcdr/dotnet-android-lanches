using AndroidLanches.API.Domain.Repositories;
using AndroidLanches.Domain.Infra;
using AndroidLanches.Domain.Repositories;
using AndroidLanches.Infra.DBConfiguration;
using AndroidLanches.Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;


namespace AndroidLanches.API
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
            services.AddControllers();
            
            //services.AddScoped<IDatabaseFactory, MySqlDatabaseFactory>();         // usando mysql
            services.AddScoped<IDatabaseFactory, SqlServerDatabaseFactory>();       // usando sql Server
            services.AddTransient<ICriadorBancoDeDados, CriadorBancoDeDadosSqlServer>();
            services.AddTransient<IPedidos,Pedidos>();
            services.AddTransient<IMesas, Mesas>();
            services.AddTransient<IProdutos, Produtos>();
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Android Lanches API",
                    Version = "v1",
                    Description = "API REST criada com o ASP.NET Core para o Sistema Android Lanches",
                    Contact = new OpenApiContact
                    {
                        Name = "Michael Costa dos Reis",
                        Url = new Uri("https://github.com/michaelcdr")
                    }
                });

                //config.OperationFilter<ApiKeyHeaderFilter>();
                //config.SchemaFilter<NullableTypeSchemaFilter>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
                //config.OperationFilter<FormFileSwaggerFilter>();
            });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())  app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json", "Android Lanches V1"
                );
            });
        }
    }
}
