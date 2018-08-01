using System.ServiceModel;

namespace Tests.NewsReader.ServiceModel
{
    [ServiceContract(Namespace = "http://Tests.NewsReader.ServiceModel")]
    public interface INewsReader
    {
        [OperationContract]
        Feed Load(string feedAddress);
    }
}
