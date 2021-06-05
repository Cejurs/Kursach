using Kursach.domain;
using Kursach.infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Forms;

namespace Kursach
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = new HostBuilder().ConfigureServices((hostcontext, services) =>
            {
                services.AddSingleton<Form1>();
                services.AddScoped<IRepository, Repository>();
            });
            var host = builder.Build();
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var form1 = services.GetRequiredService<Form1>();
                Application.Run(form1);
            }
        }
    }
}
