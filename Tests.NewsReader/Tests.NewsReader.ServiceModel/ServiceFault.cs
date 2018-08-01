using System;
using System.Runtime.Serialization;

namespace Tests.NewsReader.ServiceModel
{
    /// <summary>
    /// Represent details for client when service fail
    /// </summary>
    [DataContract]
    public class ServiceFault
    {
        /// <summary>
        /// User-friendly description of the fault
        /// </summary>
        [DataMember]
        public string Issue { get; set; }

        /// <summary>
        /// Details of the fault
        /// </summary>
        [DataMember]
        public string Details { get; set; }

        /// <summary>
        /// Exception rising this fault
        /// </summary>
        [DataMember]
        public Exception Exception { get; set; }

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="issue">User-friendly description of the fault</param>
        /// <param name="details">Details of the fault</param>
        /// <param name="e">Exception rising this fault</param>
        public ServiceFault(string issue, Exception e, string details = "")
        {
            Issue = issue;
            Details = details;
            Exception = e;
        }
    }
}
