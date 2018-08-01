using System;

namespace Tests.NewsReader.SelfHostService
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomain_UnhandledException;

            using (SelfHost host = new SelfHost())
            {
                host.Open();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ServiceModel.LogWriter.InitLogger();
            ServiceModel.LogWriter.Log(ServiceModel.LogWriter.LogLevelEnum.Error, ((Exception)e.ExceptionObject).Message, true, (Exception)e.ExceptionObject);
        }
    }
}
