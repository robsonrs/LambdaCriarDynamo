using Amazon.S3;
using Amazon.S3.Model;
using LambdaCriarDynamo.Models;
using System.Xml;

namespace LambdaCriarDynamo.Services
{
    public static class ServiceS3
    {
        public static async Task GetObjectFromBucketAsync(IAmazonS3 client,
            string bucketName,
            string objectName)
        {
            try
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = objectName,
                };

                using GetObjectResponse response = await client.GetObjectAsync(request);
                using Stream responseStream = response.ResponseStream;
                using StreamReader reader = new StreamReader(responseStream);
                // snippet-start:[S3.dotnetv3.GetObjectExample]
                string responseBody = reader.ReadToEnd();
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(responseBody);

                XmlNodeList elemList = xmlDocument.GetElementsByTagName("COLUMN");

                List<ColumnDefinition> listcolumnDefinition = new List<ColumnDefinition>();
                foreach (XmlNode node in elemList)
                {
                    ColumnDefinition columnDefinition = new()
                    {
                    };
                }
            }
            catch (AmazonS3Exception e)
            {
                // If the bucket or the object do not exist
                Console.WriteLine($"Error: '{e.Message}'");
            }
        }
    }
}
