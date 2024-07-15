using System;
using System.Windows;
using DataAccess.Models;
using FinancialWPFApp.UI.Public.Commands.Pages;
using FinancialWPFApp.UI.Public.ViewModels.Pages;
using FinancialWPFApp.UI.Public.Views;
using FinancialWPFApp.UI.Public.Views.Pages;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Repositories.Authen;
using Services;
using Services.Authen;
using Microsoft.Extensions.Hosting;

namespace FinancialWPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        ConfigureServices(services);
                    }).Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CourseManagementDBContext>();

            services.AddTransient<IAuthenRepository, AuthenRepository>();
            services.AddTransient<IAuthenService, AuthenService>();

            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudentService, StudentService>();

            services.AddScoped<MainWindowView>();
            services.AddScoped<LoginViewModel>();
            services.AddScoped<LoginCommand>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }

            base.OnExit(e);
        }
    }
}
