using System;
using System.Windows;
using DataAccess.Models;
using WPFApp.UI.Public.Commands.Pages;
using WPFApp.UI.Public.ViewModels.Pages;
using WPFApp.UI.Public.Views;
using WPFApp.UI.Public.Views.Pages;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Repositories.Authen;
using Services;
using Services.Authen;
using Microsoft.Extensions.Hosting;
using System.Windows.Threading;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                if (e.Exception != null) throw new Exception();
                {
                  
                }
                e.Handled = true;
            }
            catch (Exception)
            {
                MessageBox.Show($"An error occurred: {e.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"An unhandled error occurred: {(e.ExceptionObject as Exception)?.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public App()
        {
        }

    
    }
}
