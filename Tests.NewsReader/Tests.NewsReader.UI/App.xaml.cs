using System;
using System.Windows;

namespace Tests.NewsReader.UI
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        Client.NewsReaderClient client;

        public App()
        {
            client = Client.NewsReaderClient.GetInstance();

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Unhalted exception occured!\r\n{((Exception)e.ExceptionObject).Message}", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            client.Close();
            base.OnExit(e);
        }
    }
}
