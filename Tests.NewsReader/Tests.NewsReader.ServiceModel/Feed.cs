using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel.Syndication;

namespace Tests.NewsReader.ServiceModel
{
    [DataContract(Namespace = "http://Tests.NewsReader.ServiceModel")]
    public class Feed
    {
        /// <summary>
        /// News feed title
        /// </summary>
        [DataMember]
        public string Title { get; private set; }

        /// <summary>
        /// Absolute Url of the feed image
        /// </summary>
        [DataMember]
        public string ImageUrl { get; private set; }

        /// <summary>
        /// List of the feed's news items
        /// </summary>
        [DataMember]
        public List<NewsItem> Items { get; private set; }

        /// <summary>
        /// Represent details for client when service fail
        /// </summary>
        [DataMember]
        public ServiceFault Fault { get; private set; }

        /// <summary>
        /// Default ctor
        /// </summary>
        public Feed()
        {
            Items = new List<NewsItem>();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="feed">SyndicationFeed object</param>
        public Feed (SyndicationFeed feed)
        {
            Title = feed.Title.Text;
            ImageUrl = feed.ImageUrl.AbsoluteUri;
            Items = new List<NewsItem>();

            foreach (SyndicationItem syndicationItem in feed.Items)
                Items.Add((NewsItem)syndicationItem);
        }

        /// <summary>
        /// Ctor using when service failed
        /// </summary>
        /// <param name="fault">Represent details for client when service fail</param>
        public Feed(ServiceFault fault)
        {
            Fault = fault;
        }

        /// <summary>
        /// Overriding to show feed title instad default class string representation
        /// </summary>
        /// <returns>Title string</returns>
        public override string ToString()
        {
            return Title;
        }
    }
}
