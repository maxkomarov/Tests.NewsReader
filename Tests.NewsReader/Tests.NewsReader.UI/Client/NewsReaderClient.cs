using System;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading;
using Tests.NewsReader.ServiceModel;

namespace Tests.NewsReader.UI.Client
{
    public class NewsReaderClient 
    {
        private Process svcProcess;
        private ChannelFactory<INewsReader> channelFactory;
        private static NewsReaderClient instance;
        private BackgroundWorker loader;

        public event EventHandler LoadAsyncStarted;
        public event EventHandler<Feed> LoadAsyncCompleted;

        public static NewsReaderClient GetInstance()
        {
            return instance = instance ?? new NewsReaderClient();
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        private NewsReaderClient()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endPoint = new EndpointAddress("http://localhost:8000/NewsReader");
            channelFactory = new ChannelFactory<INewsReader>(binding, endPoint);

            if (Process.GetProcessesByName("Tests.NewsReader.SelfHostService").Length == 0)
            {
                svcProcess = Process.Start("Tests.NewsReader.SelfHostService.exe");
                Thread.Sleep(100);
            }

            loader = new BackgroundWorker();
            loader.WorkerReportsProgress = true;
            loader.DoWork += (sender, e) => { Load(e.Argument.ToString()); };
            loader.ProgressChanged += (sender, e) => { OnLoadAsyncCompleted((Feed)e.UserState); };
        }

        /// <summary>
        /// Close child service proccess
        /// </summary>
        public void Close()
        {
            if (svcProcess != null)
                if (!svcProcess.HasExited)
                {
                    svcProcess.Refresh();
                    svcProcess.CloseMainWindow();
                    svcProcess.Close();
                }
        }

        public void LoadAsync(string rssAddress)
        {
            loader.RunWorkerAsync(rssAddress);
        }

        private void OnLoadAsyncCompleted(Feed feed)
        {
            LoadAsyncCompleted?.Invoke(this, feed);
        }

        private void OnLoadAsyncStarted()
        {
            LoadAsyncStarted?.Invoke(this, EventArgs.Empty); 
        }

        private void Load(string rssAddress)
        {
            OnLoadAsyncStarted();
            Feed feed = new Feed();
            INewsReader client = null;
            try
            {
                client = channelFactory.CreateChannel();
                feed = client.Load(rssAddress);

                if (feed.Fault != null)
                {

                }
            }
            catch (EndpointNotFoundException e)
            {
                Process.Start("Tests.NewsReader.SelfHostService.exe");
                Thread.Sleep(100);
            }
            finally
            {
                if (client != null)
                {
                    ((IClientChannel)client).Close();
                }
            }
            loader.ReportProgress(100, feed);
        }
    }
}
