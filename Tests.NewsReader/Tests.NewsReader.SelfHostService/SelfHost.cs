using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Tests.NewsReader.ServiceModel;

namespace Tests.NewsReader.SelfHostService
{
    public class SelfHost : IDisposable
    {
        Uri baseAddress = new Uri(Properties.Settings.Default.ServiceUrl);
        ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
        ServiceHost host = null;

        /// <summary>
        /// Default ctor
        /// </summary>
        public SelfHost()
        {
            LogWriter.InitLogger();
            
            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
            LogWriter.Log(LogWriter.LogLevelEnum.Info, $"The NewsReader service is ready at {baseAddress}", true);
            Console.WriteLine("Press <Enter> to close service application");
        }

        /// <summary>
        /// Start ServiceHost
        /// </summary>
        /// <param name="closeImmediately">Set to true for testing purposes</param>
        public int Open(bool closeImmediately = false)
        {
            int rc = 0;
            LogWriter.Log(LogWriter.LogLevelEnum.Debug, "SericeHost creating...");
            try
            {

                using (host = new ServiceHost(typeof(NewsReader), baseAddress))
                {
                    host.Description.Behaviors.Add(smb);
                    LogWriter.Log(LogWriter.LogLevelEnum.Debug, "SericeHost opening...");
                    host.Open();
                    LogWriter.Log(LogWriter.LogLevelEnum.Debug, "SericeHost opened");
                    if (!closeImmediately)
                        Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                LogWriter.Log(LogWriter.LogLevelEnum.Error, e.Message, true, e);
                rc = -1;
            }
            finally
            {
                LogWriter.Log(LogWriter.LogLevelEnum.Debug, "SericeHost closing on finally...");
                host?.Close();
                LogWriter.Log(LogWriter.LogLevelEnum.Debug, "SericeHost closed");
            }
            LogWriter.Log(LogWriter.LogLevelEnum.Debug, $"SericeHost.Load() return value: {rc}");
            return rc;
        }

        void IDisposable.Dispose()
        {
            LogWriter.Log(LogWriter.LogLevelEnum.Debug, "SericeHost disposing...");
            if (host?.State != CommunicationState.Closed)
            {
                LogWriter.Log(LogWriter.LogLevelEnum.Debug, "SericeHost closing on dispose...");
                host.Close();
                LogWriter.Log(LogWriter.LogLevelEnum.Debug, "SericeHost closed");
            }
        }
    }
}
