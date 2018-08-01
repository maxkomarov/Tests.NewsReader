using System;
using System.ServiceModel.Syndication;
using System.Xml;
using Tests.NewsReader.ServiceModel;

namespace Tests.NewsReader.SelfHostService
{
    /// <summary>
    /// Implementation of INewsReader Contract
    /// </summary>
    public class NewsReader : INewsReader
    {
        /// <summary>
        /// Implementation of INewsReader.Load() method
        /// </summary>
        /// <param name="feedAddress">Url of requested RSS-feed for loading</param>
        /// <returns></returns>
        public Feed Load(string feedAddress)
        {
            LogWriter.Log(LogWriter.LogLevelEnum.Info, $"Loading news from rss-feed {feedAddress} requested...", true);
            Feed feed = null;
            try
            {
                LogWriter.Log(LogWriter.LogLevelEnum.Debug,"SysndicationFeed.Load() calling...");

                using (XmlReader xmlReader = XmlReader.Create(feedAddress))
                {
                    SyndicationFeed synchFeed = SyndicationFeed.Load(xmlReader);
                    feed = new Feed(synchFeed);
                }

                LogWriter.Log(LogWriter.LogLevelEnum.Info, $"{feed.Items.Count} news items loaded.", true);
            }
            catch (Exception e)
            {
                LogWriter.Log(LogWriter.LogLevelEnum.Error, $"Service fail: {e.Message}", true, e);

                feed = new Feed(new ServiceFault("News Reader Service fail!", e));
            }
            LogWriter.Log(LogWriter.LogLevelEnum.Debug, $"NewsReader.Load() return value: {feed.ToString()}");
            return feed;
        }
    }
}
