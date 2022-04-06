using Amazon.Lambda.APIGatewayEvents;
using Xunit;


namespace LambdaCriarDynamo.Tests;

public class FunctionTest
{
    public FunctionTest()
    {
    }

    [Fact]
    public void TestGetMethod()
    {
        APIGatewayProxyResponse response;

        Functions functions = new Functions();

        response = functions.Get();
        Assert.Equal(200, response.StatusCode);
        Assert.Equal("Hello AWS Serverless", response.Body);
    }

    [Fact]
    public void TestCreateDatabaseDynamo()
    {
        Functions functions = new Functions();
        Assert.True(functions.CreateDatabaseDynamo());
    }

    [Fact]
    public void TestCreateTableFromFmt()
    {
        Functions functions = new Functions();
        Functions.CreateTableFromFmt();
        Assert.True(true);
    }
}