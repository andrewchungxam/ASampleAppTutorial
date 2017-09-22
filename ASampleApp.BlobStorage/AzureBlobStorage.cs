using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using System;
using System.IO;
using Xamarin.Forms;
using System.Reflection;
using Microsoft.WindowsAzure.Storage.Auth;
using Plugin.Media.Abstractions;

namespace ASampleApp.BlobStorage
{
	public class AzureBlobStorage
	{
        public String FileNameForBlob { get; set; }



		public AzureBlobStorage()
		{
        


        }

        public CloudBlobClient CreateCloudBlobClient()
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AzureBlobConstants.BlobUrlAndKey);
			CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            return blobClient;
        }



		/* Unmerged change from project 'ASampleApp.iOS'
        Before:
                public CloudBlobContainer CreateCloudBlobClientAndContainer(String containerName)
                {

                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AzureBlobConstants.BlobUrlAndKey);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer container = blobClient.GetContainerReference(containerName);
                    await container.CreateIfNotExistsAsync();


                }



        public static async Task performBlobOperation ()
        After:
                public async Task<CloudBlobContainer> CreateCloudBlobClientAndContainerAsync(String containerName)
                {

                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AzureBlobConstants.BlobUrlAndKey);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer container = blobClient.GetContainerReference(containerName);
                    await container.CreateIfNotExistsAsync();


                }



                public static async Task performBlobOperation ()
        */
		public async Task<CloudBlobContainer> CreateCloudBlobClientAndContainerAsync(String containerName)
		{

			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AzureBlobConstants.BlobUrlAndKey);
			CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

			CloudBlobContainer container = blobClient.GetContainerReference(containerName);
			await container.CreateIfNotExistsAsync();

			var perm = new BlobContainerPermissions();
			perm.PublicAccess = BlobContainerPublicAccessType.Blob;
			await container.SetPermissionsAsync(perm);

            return container;

		}


		public CloudBlobContainer CreateCloudBlobClientAndContainer(String containerName)
		{

			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AzureBlobConstants.BlobUrlAndKey);
			CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

			CloudBlobContainer container = blobClient.GetContainerReference(containerName);
			container.CreateIfNotExistsAsync();

			var perm = new BlobContainerPermissions();
			perm.PublicAccess = BlobContainerPublicAccessType.Blob;
			container.SetPermissionsAsync(perm);

			return container;

		}

		private const string ContainerPrefix = "ASampleAppFive-";

        public async Task UploadToBlobViaStream(CloudBlobContainer myCloudBlobContainer, MediaFile myMediaFile)
		{


			//NAME THE FILE
			//CREATE THE BLOCKBLOB
			// THEN TO THE THE BLOCKBLOB -> UPLOAD FROM STREAM


			//NAME THE FILE
			string imageName = ContainerPrefix + Guid.NewGuid();
            this.FileNameForBlob = imageName;

			//CREATE THE BLOCKBLOB
			CloudBlockBlob blockBlob = myCloudBlobContainer.GetBlockBlobReference(imageName);

			//STREAMIFY THE IMAGE
			var stream = myMediaFile.GetStream();

            //UPLOAD TO BLOB
            await blockBlob.UploadFromStreamAsync(stream);



//			//UPLOAD IMAGE
//			const string ImageToUpload = "HelloWorld.png";
//			CloudBlockBlob blockBlob = myCloudBlobContainer.GetBlockBlobReference(ImageToUpload);


//			string imagePath = "ASampleApp.BlobStorage.HelloWorld.png";
//			Assembly assembly = typeof(AzureBlobStorage).GetTypeInfo().Assembly;


//#if __IOS__
//			var stream = assembly.GetManifestResourceStream("ASampleApp.iOS.HelloWorld.png");
//#endif

//#if __ANDROID__
//    var stream = assembly.GetManifestResourceStream("ASampleApp.Droid.HelloWorld.png");
//#endif

			////byte [] buffer;
			////using (Stream stream = assembly.GetManifestResourceStream (imagePath)) {
			////  long length = stream.Length;
			////  buffer = new byte [length];
			////  stream.Read (buffer, 0, (int)length);


			////var stream = assembly.GetManifestResourceStream (imagePath);

			////var bytes = new byte [stream.Length];
			////await stream.ReadAsync (bytes, 0, (int)stream.Length);
			////string base64 = System.Convert.ToBase64String (bytes);
			//await blockBlob.UploadFromStreamAsync(stream);

			////var storeragePath = await iStorageService.SaveBinaryObjectToStorageAsync (string.Format (FileNames.ApplicationIcon, app.ApplicationId), buffer);
			////app.IconURLLocal = storeragePath;
		}




        public async Task UploadToBlobViaStreamSync(CloudBlobContainer myCloudBlobContainer, MediaFile myMediaFile)
        {

            string imageName = ContainerPrefix + Guid.NewGuid();
            this.FileNameForBlob = imageName;
            CloudBlockBlob blockBlob = myCloudBlobContainer.GetBlockBlobReference(imageName);
            var stream = myMediaFile.GetStream();
            await blockBlob.UploadFromStreamAsync(stream);
            return;
        }

        public async Task RunForALongTime()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 100000000; i++)
                    Console.WriteLine("Still running...");

            });
        }





		public static async Task performBlobOperation2()
        {
            //https://stackoverflow.com/questions/22014384/getting-images-in-the-azure-blob-storage
            //        var credentials = new StorageCredentials("asampleappfive", "UPDATE_YOUR_OWN_comcQ==");



            // Retrieve storage account from connection string.
            //          CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=your_account_name_here;AccountKey=your_account_key_here");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AzureBlobConstants.BlobUrlAndKey);

            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol = https; AccountName = asampleappfive; AccountKey = UPDATE_YOUR_OWN_comcQ==; EndpointSuffix = core.windows.net");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("my9container");
            // Create the container if it doesn't already exist.

            await container.CreateIfNotExistsAsync();

            var perm = new BlobContainerPermissions();
            perm.PublicAccess = BlobContainerPublicAccessType.Blob;
            await container.SetPermissionsAsync(perm);

            //UPLOAD IMAGE
            const string ImageToUpload = "HelloWorld.png";
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(ImageToUpload);


            string imagePath = "ASampleApp.BlobStorage.HelloWorld.png";
            Assembly assembly = typeof(AzureBlobStorage).GetTypeInfo().Assembly;

            //byte [] buffer;
            //using (Stream stream = assembly.GetManifestResourceStream (imagePath)) {
            //  long length = stream.Length;
            //  buffer = new byte [length];
            //  stream.Read (buffer, 0, (int)length);


#if __IOS__
            var stream = assembly.GetManifestResourceStream("ASampleApp.iOS.HelloWorld.png");
#endif

#if __ANDROID__
    var stream = assembly.GetManifestResourceStream("ASampleApp.Droid.HelloWorld.png");
#endif

            //var stream = assembly.GetManifestResourceStream (imagePath);

            //var bytes = new byte [stream.Length];
            //await stream.ReadAsync (bytes, 0, (int)stream.Length);
            //string base64 = System.Convert.ToBase64String (bytes);
            await blockBlob.UploadFromStreamAsync(stream);

            //var storeragePath = await iStorageService.SaveBinaryObjectToStorageAsync (string.Format (FileNames.ApplicationIcon, app.ApplicationId), buffer);
            //app.IconURLLocal = storeragePath;
        }







        public static async Task performBlobOperation ()
{
        //https://stackoverflow.com/questions/22014384/getting-images-in-the-azure-blob-storage
//        var credentials = new StorageCredentials("asampleappfive", "Update_your_owncomcQ==");



			// Retrieve storage account from connection string.
			//			CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=your_account_name_here;AccountKey=your_account_key_here");
			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AzureBlobConstants.BlobUrlAndKey);

	//CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol = https; AccountName = asampleappfive; AccountKey = UPDATE_YOUR_OWN_comcQ==; EndpointSuffix = core.windows.net");

	// Create the blob client.
	CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient ();
	// Retrieve reference to a previously created container.
	CloudBlobContainer container = blobClient.GetContainerReference ("my9container");
	// Create the container if it doesn't already exist.

	await container.CreateIfNotExistsAsync ();

    var perm = new BlobContainerPermissions();
    perm.PublicAccess = BlobContainerPublicAccessType.Blob;
    await container.SetPermissionsAsync(perm);

	//UPLOAD IMAGE
	const string ImageToUpload = "HelloWorld.png";
	CloudBlockBlob blockBlob = container.GetBlockBlobReference (ImageToUpload);


    string imagePath = "ASampleApp.BlobStorage.HelloWorld.png";
	Assembly assembly = typeof (AzureBlobStorage).GetTypeInfo ().Assembly;

	//byte [] buffer;
	//using (Stream stream = assembly.GetManifestResourceStream (imagePath)) {
	//	long length = stream.Length;
	//	buffer = new byte [length];
	//	stream.Read (buffer, 0, (int)length);


#if __IOS__
	var stream = assembly.GetManifestResourceStream ("ASampleApp.iOS.HelloWorld.png");
#endif

#if __ANDROID__
	var stream = assembly.GetManifestResourceStream("ASampleApp.Droid.HelloWorld.png");
#endif

			//var stream = assembly.GetManifestResourceStream (imagePath);

			//var bytes = new byte [stream.Length];
			//await stream.ReadAsync (bytes, 0, (int)stream.Length);
			//string base64 = System.Convert.ToBase64String (bytes);
			await blockBlob.UploadFromStreamAsync (stream);

	//var storeragePath = await iStorageService.SaveBinaryObjectToStorageAsync (string.Format (FileNames.ApplicationIcon, app.ApplicationId), buffer);
	//app.IconURLLocal = storeragePath;
}


		//public static async Task<string> getTheUrlFromBlob()
		public static string getTheUrlFromBlob()

		{
			// Retrieve storage account from connection string.
			//          CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=your_account_name_here;AccountKey=your_account_key_here");
			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AzureBlobConstants.BlobUrlAndKey);

			//CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol = https; AccountName = asampleappfive; AccountKey = UPDATE_YOUR_ACCOUNT_KEY_HERE_iQgcomcQ==; EndpointSuffix = core.windows.net");

			// Create the blob client.
			CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
			// Retrieve reference to a previously created container.
			CloudBlobContainer container = blobClient.GetContainerReference("my7container");
			// Create the container if it doesn't already exist.
			//await container.CreateIfNotExistsAsync();

			container.CreateIfNotExistsAsync();

			//UPLOAD IMAGE
			const string ImageToUpload = "HelloWorld.png";
			CloudBlockBlob blockBlob = container.GetBlockBlobReference(ImageToUpload);


			string imagePath = "ASampleApp.BlobStorage.HelloWorld.png";
			Assembly assembly = typeof(AzureBlobStorage).GetTypeInfo().Assembly;

#if __IOS__
			var stream = assembly.GetManifestResourceStream("ASampleApp.iOS.HelloWorld.png");
#endif

#if __ANDROID__
    var stream = assembly.GetManifestResourceStream("ASampleApp.Droid.HelloWorld.png");
#endif
			//await blockBlob.UploadFromStreamAsync(stream);

			blockBlob.UploadFromStreamAsync(stream);
            string hi = "https://xamarin.com/content/images/pages/forms/example-app.png";

            return hi ;
		}



		//public static async Task<byte[]> GetFileAsync(ContainerType containerType, string name)
		//{
		//	var container = GetContainer(containerType);

		//	var blob = container.GetBlobReference(name);
		//	if (await blob.ExistsAsync())
		//	{
		//		await blob.FetchAttributesAsync();
		//		byte[] blobBytes = new byte[blob.Properties.Length];

		//		await blob.DownloadToByteArrayAsync(blobBytes, 0);
		//		return blobBytes;
		//	}
		//	return null;
		//}










































		//		public static async Task performBlobOperation()
		//		{
		//			// Retrieve storage account from connection string.
		//			//			CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=your_account_name_here;AccountKey=your_account_key_here");
		//			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AzureBlobConstants.BlobUrlAndKey);

		//			//CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol = https; AccountName = asampleappfive; AccountKey = UPDATE_WITH_YOUR_OWN_QgcomcQ==; EndpointSuffix = core.windows.net");

		//            // Create the blob client.
		//			CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

		//			// Retrieve reference to a previously created container.
		//			CloudBlobContainer container = blobClient.GetContainerReference("my6container");

		//			// Create the container if it doesn't already exist.
		//			await container.CreateIfNotExistsAsync();


		//			//UPLOAD IMAGE
		//			const string ImageToUpload = "HelloWorld.png";
		//			CloudBlockBlob blockBlob = container.GetBlockBlobReference (ImageToUpload);

		////			blockBlob.Properties.ContentType = "image/png";
		////			Console.WriteLine("- {0} (type: {1})", blockBlob.Uri, blockBlob.GetType());
		////UPLOAD FROM FILE?
		////			await blockBlob.UploadFromFileAsync (ImageToUpload);

		////			https://stackoverflow.com/questions/41707205/xamarin-forms-image-to-stream

		////			string imagePath = "ASampleApp.BlobStorage.HelloWorld.png";
		//			string imagePath = "ASampleApp.BlobStorage.HelloWorld.png";


		//			Assembly assembly = typeof (MyClass).GetTypeInfo ().Assembly;

		//			//byte [] buffer;
		//			//using (Stream stream = assembly.GetManifestResourceStream (imagePath)) {
		//			//	long length = stream.Length;
		//			//	buffer = new byte [length];
		//			//	stream.Read (buffer, 0, (int)length);

		//			var stream = assembly.GetManifestResourceStream ("ASampleApp.iOS.HelloWorld.png");
		//			//var stream = assembly.GetManifestResourceStream (imagePath);

		//			//var bytes = new byte [stream.Length];
		//			//await stream.ReadAsync (bytes, 0, (int)stream.Length);
		//			//string base64 = System.Convert.ToBase64String (bytes);
		//			await blockBlob.UploadFromStreamAsync (stream);

		//				//var storeragePath = await iStorageService.SaveBinaryObjectToStorageAsync (string.Format (FileNames.ApplicationIcon, app.ApplicationId), buffer);
		//				//app.IconURLLocal = storeragePath;
		//		}


		//UPLOAD FROM STREAM


		//UPLOAD FROM TEXT



		////UPLOAD TEXT
		//// Retrieve reference to a blob named "myblob".
		//CloudBlockBlob blockBlob = container.GetBlockBlobReference ("mypictureblob");
		//await blockBlob.UploadTextAsync ("Hello, world!");






		// Create the "myblob" blob with the text "Hello, world!"
		//			await blockBlob.UploadTextAsync ("Hello, world!");

		//// Retrieve reference to a blob named "myblob".
		//CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");

		//// Create the "myblob" blob with the text "Hello, world!"
		//await blockBlob.UploadTextAsync("Hello, world!");
	}


	//public static async Task performBlobOperation ()
	//{
	//	// Retrieve storage account from connection string.
	//	//			CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=your_account_name_here;AccountKey=your_account_key_here");
	//	CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AzureBlobConstants.BlobUrlAndKey);
	//	//CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol = https; AccountName = asampleappfive; AccountKey = UPDATE_WITH_YOUR_OWN_QgcomcQ==; EndpointSuffix = core.windows.net");

	//	// Create the blob client.
	//	CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient ();

	//	// Retrieve reference to a previously created container.
	//	CloudBlobContainer container = blobClient.GetContainerReference ("mycontainer");

	//	// Create the container if it doesn't already exist.
	//	await container.CreateIfNotExistsAsync ();

	//	// Retrieve reference to a blob named "myblob".
	//	CloudBlockBlob blockBlob = container.GetBlockBlobReference ("myblob");

	//	// Create the "myblob" blob with the text "Hello, world!"
	//	await blockBlob.UploadTextAsync ("Hello, world!");
	//}

}
