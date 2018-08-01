using System;
using System.Runtime.Serialization;
using System.ServiceModel.Syndication;

namespace Tests.NewsReader.ServiceModel
{
    /// <summary>
    /// Represent news feed item object
    /// </summary>
    [DataContract(Namespace = "http://Tests.NewsReader.ServiceModel")]
    public class NewsItem
    {
        /// <summary>
        /// News Title
        /// </summary>
        [DataMember]
        public string Title { get; private set; }

        /// <summary>
        /// News Summary
        /// </summary>
        [DataMember]
        public string Summary { get; private set; }

        /// <summary>
        /// News publication date and time
        /// </summary>
        [DataMember]
        public DateTime PublishDate { get; private set; }

        /// <summary>
        /// News publication Url
        /// </summary>
        [DataMember]
        public string SourceUrl { get; private set; }

        /// <summary>
        /// Converter for explicit convertion from SyndicationItem type to NewsItem type
        /// </summary>
        /// <param name="syndicationItem">SyndicationItem object</param>
        public static explicit operator NewsItem (SyndicationItem syndicationItem)
        {
            NewsItem newsItem = new NewsItem();
            newsItem.Title = syndicationItem.Title.Text;
            newsItem.Summary = syndicationItem.Summary.Text;
            newsItem.Summary = newsItem.Summary.Split('<')[0];
            newsItem.PublishDate = syndicationItem.PublishDate.DateTime;
            newsItem.SourceUrl = syndicationItem.Id;
            return newsItem;
        }

        /// <summary>
        /// Overriding to show news title instad default class string representation
        /// </summary>
        /// <returns>Title string</returns>
        public override string ToString()
        {
            return Title;
        }
    }
}
