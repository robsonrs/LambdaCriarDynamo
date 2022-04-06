using Amazon.DynamoDBv2;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System.Net;
using LambdaCriarDynamo.Services;
using Amazon.S3;
using Amazon;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaCriarDynamo;

public class Functions
{
    AmazonDynamoDBClient client;
    /// <summary>
    /// Default constructor that Lambda will invoke.
    /// </summary>
    public Functions()
    {
    }


    /// <summary>
    /// A Lambda function to respond to HTTP Get methods from API Gateway
    /// </summary>
    /// <param name="request"></param>
    /// <returns>The API Gateway response.</returns>
    public APIGatewayProxyResponse Get()
    {
        var response = new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            Body = "Hello AWS Serverless",
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
        };

        return response;
    }

    public bool CreateDatabaseDynamo()
    {
        
        AmazonDynamoDBConfig config = new AmazonDynamoDBConfig();
        config.ServiceURL = "http://localhost:8000";
        try
        {
            client = new AmazonDynamoDBClient(config);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Falha ao criar conexão Dynamo. \nMensagem de Erro: {ex.Message} \nStack Trace: {ex.StackTrace}");
            return false;
        }

        return true;
    }

    public static async void CreateTableFromFmt()
    {
        IAmazonS3 client = new AmazonS3Client(RegionEndpoint.USEast1);
        await ServiceS3.GetObjectFromBucketAsync(client, "formato-tabela-usuario", "usuarios.xml");
    }
}