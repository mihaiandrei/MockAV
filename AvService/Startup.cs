using AvService.Domain;
using AvService.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AvService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
                                                                .AllowAnyHeader()
                                                                .AllowAnyMethod());
            });

            services.AddSingleton<IScannerService, ScannerService>();
            services.AddSingleton<IConnectedClientManager, ConnectedClientManager>();
            services.AddSingleton<INotificationRepository, NotificationRepository>();
            services.AddSingleton<INotifier, Notifier>();
            services.AddSingleton<IScanner, Scanner>();
            services.AddSingleton<IScanHub, ContextHolder>();
            services.AddScoped<DatabaseContext>();

            services.AddSignalR();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.WithOrigins("http://localhost:3000")
                                             .AllowAnyHeader()
                                             .AllowAnyMethod()
                                             .AllowCredentials());

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapHub<ScanHub>("/scanhub");
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
