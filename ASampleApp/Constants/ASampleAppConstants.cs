using System;
namespace ASampleApp.Constants
{
	static public class ASampleAppConstants
	{
		public static String MobileCenterIOS { get; } = "\"ios=294968e2-d712-43fc-a8bd-1492ed9ea29c;\"";
	}
}

namespace ASampleApp.BlobStorage
{
	public static class AzureBlobConstants
	{
		public static String BlobUrlAndKey = "DefaultEndpointsProtocol=https;AccountName=asampleappfive;AccountKey=Ka7kuZyouSKyl4Gbumc0eJkPdXlpYMl5YJmuShv8qIi+bl9HcN74eGP2qzGQRvrchTznRqpwr5zUO+iQgcomcQ==";
	}
}

namespace ASampleApp.CosmosDB
{
	public static class CosmosDBStrings
    {

	//#error SIGN UP FOR AZURE - THEN SET THE BELOW KEYS AND THEN COMMENT OUT THIS LINE
	//static readonly String myEndPoint = "https://YOUR_AZURE_COSMODB_INSTANCE.documents.azure.com:44339283123/";
	//static readonly String myKey = "YOUR_SECRET_KEY_23j9fj932jrh23hr93r29jrj3r2j3rjjdf==";

	//PUT THIS IN ASampleApp.CosmosDB - CosmosDBStrings

	public static readonly String myEndPoint = "https://asampleapp.documents.azure.com:443/";// "https://azurecosmosdbxamfb.documents.azure.com:443/";
	public static readonly String myKey = "e2FWNoi18D78r90x1Zv1MIKWFsNB6nDq0sCJNTOGCijeRYOAFxgJCj0hjym7EA9igSRWTnyw4AwRP1PeFU4HMQ=="; //"ZnxlLkJfoYBEsSUmQNEPOPi6Van37erVHUrFubpRM9Lr6nv0aY6xY5AZpbGoe3ibg2xPLo6Gj2y6u0BHtQm6pg==";

	//#error Make sure to add this Nuget package to all your platform projects Microsoft.Azure.DocumentDb.Core
    }
}

//using System;
//namespace ASampleApp.Constants
//{
//	static public class ASampleAppConstants
//	{
//		public static String MobileCenterIOS { get; } = "\"ios=asdfasdffddfsdfsf;\"";

//	}
//}
