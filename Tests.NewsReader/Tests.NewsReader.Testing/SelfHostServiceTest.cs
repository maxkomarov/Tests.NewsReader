using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.NewsReader.Testing
{
    [TestClass]
    public class SelfHostServiceTest
    {
        [TestMethod]
        public void OpenHost()
        {
            SelfHostService.SelfHost host = new SelfHostService.SelfHost();
            int rc = host.Open(true);
            Assert.AreEqual(0, rc);
        }

        [TestMethod]
        public void LoadFeed()
        {
            SelfHostService.NewsReader reader = new SelfHostService.NewsReader();
            ServiceModel.Feed feed = reader.Load(Properties.Settings.Default.RssAddressUrl);
            Assert.AreEqual(null, feed.Fault);
        }
    }
}
