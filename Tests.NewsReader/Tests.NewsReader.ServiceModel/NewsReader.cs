using System.ServiceModel.Syndication;
using System.Xml;

namespace Tests.NewsReader.ServiceModel
{
    public class NewsReader : INewsReader
    {
        public Feed Load(string feedAddress)
        {
            XmlReader xmlReader = XmlReader.Create(feedAddress);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
            return new Feed(feed);
        }
    }
}
