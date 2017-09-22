using System;
using System.Windows.Input;
using System.Threading.Tasks;

using Xamarin.Forms;

using Plugin.Media;
using Plugin.Media.Abstractions;

using ASampleApp.CosmosDB;

namespace ASampleApp.ViewModels
{
	public class AddDogPhotoBlobViewModel : BaseViewModel
	{
		string _firstLabel;
		string _firstEntryText;
		string _secondEntryText;
		string _photoURLEntry;
        string _photoURLBlobEntry;
		string _photoSourceEntry;
		MediaFile _file;

		string _photoSourceBaseSixtyFourEntry;

		public EventHandler<AlertEventArgs> TakePhotoFailed;
		public class AlertEventArgs : EventArgs
		{
			public string Title { get; set; }
			public string Message { get; set; }
		}

		public EventHandler<PhotoSavedSuccessAlertEventArgs> TakePhotoSucceeded;

		public class PhotoSavedSuccessAlertEventArgs : EventArgs
		{
			public string Title { get; set; }
			public string Message { get; set; }

		}

		public ICommand MyFavoriteCommand { get; set; }
		public ICommand MySecondFavoriteCommand { get; set; }

		public string FirstLabel
		{
			get { return _firstLabel; }
			set { SetProperty(ref _firstLabel, value); }
		}

		public string FirstEntryText
		{
			get { return _firstEntryText; }
			set { SetProperty(ref _firstEntryText, value); }

		}

		public string SecondEntryText
		{
			get { return _secondEntryText; }
			set { SetProperty(ref _secondEntryText, value); }
		}

		public string PhotoURLEntry
		{
			get { return _photoURLEntry; }
			set
			{
				SetProperty(ref _photoURLEntry, value);
				this.PhotoSourceEntry = _photoURLEntry;
			}
		}

		public string PhotoURLBlobEntry
		{
			get { return _photoURLBlobEntry; }
			set
			{
				SetProperty(ref _photoURLBlobEntry, value);
				this.PhotoSourceEntry = _photoURLBlobEntry;
			}
		}

		public string PhotoSourceEntry
		{
			get { return _photoSourceEntry; }
			set { SetProperty(ref _photoSourceEntry, value); }
		}

		public string PhotoSourceBaseSixtyFourEntry
		{
			get { return _photoSourceBaseSixtyFourEntry; }
			set
			{
				SetProperty(ref _photoSourceBaseSixtyFourEntry, value);

			}
		}


		public AddDogPhotoBlobViewModel()
		{
			MyFavoriteCommand = new Command(OnMyFavoriteAction);
			MySecondFavoriteCommand = new Command(OnMySecondFavoriteAction);

		}

        //SUBMIT BUTTON
		void OnMyFavoriteAction()
		{

            if (_file != null)
            {
                // PUT INTO AZURE BLOB STORAGE
                //                Task.Run(async () => await App.MyAzureBlobStorage.UploadToBlobViaStream(App.MyCloudBlobContainer, _file));
                App.MyAzureBlobStorage.UploadToBlobViaStreamSync(App.MyCloudBlobContainer, _file);

//                RunBlobMethod(_file);

				//GIVE ME THE NAME OF THE FILE (POSSIBLY NEED TO JUST CREATE A PUBLIC PROPERTY ON THE AUZRE BLOB STORAGE CLASS AND THEN ACCESS IT
				//THE PROPERTY IS THE " STRING NAME OF FILE FOR BLOB {GET;SET;}
				// you need the URL name
				// you need the base URL (which includes container name)
				// plus you need the name of the GUID name
				// the GUID NAME is located in AZUREBLOBSTORAGE CLASS -> AROUND LINE 125
				// string imageName = ContainerPrefix + Guid.NewGuid();
				// YOU NEED TO STORE THIS SOMEWHERE THAT MAKES SENSE
				// CREATE THE URL VIA BASE URL (INCLUDING CONTAINER CONSTANT FROM APP CLASS) + FILE 
				var myURLForBlobStorage = String.Format("https://asampleappfive.blob.core.windows.net/{0}/{1}", App.ContainerName, App.MyAzureBlobStorage.FileNameForBlob);

                // {GET;SET; -> URL}
                // POST THE URL -> PROPERTY URL


                this.PhotoURLBlobEntry = myURLForBlobStorage;


				//ADD TO DOG REP BLOB.ADD NEW DOG PHOTO BLOB SOURCE (THIS.FIRSTENTRYTEXT, THIS.SECONDENTRYTEXT, THIS.BLOBURL);
				//MODIFY THIS METHOD TO REFLECT BLOB NOT BASE 64
				// PUTTING THIS INTO THE DATABASE
				//
				//				App.DogRepBaseSixtyFour.AddNewDogPhotoSourceB64(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceBaseSixtyFourEntry); //this.PhotoSourceEntry);

                //NOTE THAT SETTING PHOTOURLENTRY -> WILL AUTOMATICALLY SET THE PHOTOSOURCE ENTRY 
                App.DogRepBlob.AddNewDogPhotoURLBlob(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceEntry); //this.PhotoSourceEntry);

				//MODIFY THIS METHOD TO REFLECT BLOB NOT BASE 64
				AddLastDogToCosmosDBAsync();

				//MODIFY THIS METHOD TO REFLECT BLOB NOT BASE 64
				//ADDING THIS TO THE TOP OF THE PAGE
				//string _lastNameString = App.DogRepBaseSixtyFour.GetLastDogB64().Name;
				//string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
				//this.FirstLabel = _lastNameStringAdd;

                string _lastNameString = App.DogRepBlob.GetLastDogBlob().Name;
				string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
				this.FirstLabel = _lastNameStringAdd;

				//MODIFY THIS METHOD TO REFLECT BLOB NOT BASE 64
				//ADD THE LAST DOG TO THE ViewModel
				//var tempLastDog = App.DogRepBaseSixtyFour.GetLastDogB64();
                //App.MyDogListPhotoBase64Page.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);

                var tempLastDog = App.DogRepBlob.GetLastDogBlob();

				//App.MyDogListPhotoBase64Page.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);
                App.MyDogListPhotoBlobPage.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);

				return;
            }
            else
            {

				//ADD TO DOG REP BLOB.ADD NEW DOG PHOTO BLOB SOURCE (THIS.FIRSTENTRYTEXT, THIS.SECONDENTRYTEXT, THIS.BLOBURL);
				//MODIFY THIS METHOD TO REFLECT BLOB NOT BASE 64
				// PUTTING THIS INTO THE DATABASE
				//
				//              App.DogRepBaseSixtyFour.AddNewDogPhotoSourceB64(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceBaseSixtyFourEntry); //this.PhotoSourceEntry);

				//NOTE THAT SETTING PHOTOURLENTRY -> WILL AUTOMATICALLY SET THE PHOTOSOURCE ENTRY 
				App.DogRepBlob.AddNewDogPhotoURLBlob(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceEntry); //this.PhotoSourceEntry);

				//MODIFY THIS METHOD TO REFLECT BLOB NOT BASE 64
				AddLastDogToCosmosDBAsync();

				//MODIFY THIS METHOD TO REFLECT BLOB NOT BASE 64
				//ADDING THIS TO THE TOP OF THE PAGE
				//string _lastNameString = App.DogRepBaseSixtyFour.GetLastDogB64().Name;
				//string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
				//this.FirstLabel = _lastNameStringAdd;

				string _lastNameString = App.DogRepBlob.GetLastDogBlob().Name;
				string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
				this.FirstLabel = _lastNameStringAdd;

				//MODIFY THIS METHOD TO REFLECT BLOB NOT BASE 64
				//ADD THE LAST DOG TO THE ViewModel
				//var tempLastDog = App.DogRepBaseSixtyFour.GetLastDogB64();
				//App.MyDogListPhotoBase64Page.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);

				var tempLastDog = App.DogRepBlob.GetLastDogBlob();

				//App.MyDogListPhotoBase64Page.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);
				App.MyDogListPhotoBlobPage.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);


				return;
            }



		}

        public static void RunBlobMethod(MediaFile _file)
        {
            Task.Run(async () => await App.MyAzureBlobStorage.UploadToBlobViaStream(App.MyCloudBlobContainer, _file));

		}


		public static string getTheUrlFromBlob()

		{
            // you need the URL name
            // you need the base URL (which includes container name)
            // plus you need the name of the GUID name
            // the GUID NAME is located in AZUREBLOBSTORAGE CLASS -> AROUND LINE 125
            // string imageName = ContainerPrefix + Guid.NewGuid();
            // YOU NEED TO STORE THIS SOMEWHERE THAT MAKES SENSE
            //

            String myURL = "https://www.espn.com";
			return myURL;

//			// Retrieve storage account from connection string.
//			//          CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=your_account_name_here;AccountKey=your_account_key_here");
//			CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=asampleappfive;AccountKey=update_with_your_own_keyiQgcomcQ==");

//			//CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol = https; AccountName = asampleappfive; AccountKey = UPDATE_WITH_YOUR_OWN_KEY+iQgcomcQ==; EndpointSuffix = core.windows.net");

//			// Create the blob client.
//			CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
//			// Retrieve reference to a previously created container.
//			CloudBlobContainer container = blobClient.GetContainerReference("my7container");
//			// Create the container if it doesn't already exist.
//			//await container.CreateIfNotExistsAsync();

//			container.CreateIfNotExistsAsync();

//			//UPLOAD IMAGE
//			const string ImageToUpload = "HelloWorld.png";
//			CloudBlockBlob blockBlob = container.GetBlockBlobReference(ImageToUpload);


//			string imagePath = "ASampleApp.BlobStorage.HelloWorld.png";
//			Assembly assembly = typeof(AzureBlobStorage).GetTypeInfo().Assembly;

//#if __IOS__
//			var stream = assembly.GetManifestResourceStream("ASampleApp.iOS.HelloWorld.png");
//#endif

//#if __ANDROID__
//    var stream = assembly.GetManifestResourceStream("ASampleApp.Droid.HelloWorld.png");
//#endif
			////await blockBlob.UploadFromStreamAsync(stream);

			//blockBlob.UploadFromStreamAsync(stream);
			//string hi = "https://xamarin.com/content/images/pages/forms/example-app.png";

			//return hi;
		}



		//private async void AddLastDogToCosmosDBAsync()
		//{
  //          var myDog = App.DogRepBlob.GetLastDogBlob();
		//	var myCosmosDog = DogConverter.ConvertToCosmosDog(myDog);
		//	await CosmosDB.CosmosDBService.PostCosmosDogAsync(myCosmosDog);
		//}

		private async void AddLastDogToCosmosDBAsync()
		{
			var myDog = App.DogPhotoRep.GetLastDogPhoto();
			var myCosmosDog = DogConverter.ConvertToCosmosDog(myDog);
            await CosmosDB.CosmosDBServicePhotoBlob.PostCosmosDogAsync(myCosmosDog);
		}



        //TAKE PHOTO
		private async void OnMySecondFavoriteAction(object obj)
		{
            



			await CrossMedia.Current.Initialize();

			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				TakePhotoFailed?.Invoke(this, new AlertEventArgs { Title = "No Camera", Message = "no camera" });

				return;
			}

			_file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
			{

				PhotoSize = PhotoSize.Small,
				CompressionQuality = 10,
				//CustomPhotoSize = 50,
				Directory = "Sample",
				Name = "test.jpg"
			});

			if (_file == null)
				return;


			var stream = _file.GetStream();

            //TAKE THE PUBLIC BLOCK --> UPLOAD THE STREAM




			var bytes = new byte[stream.Length];
			await stream.ReadAsync(bytes, 0, (int)stream.Length);
			string base64 = System.Convert.ToBase64String(bytes);

			this.PhotoSourceBaseSixtyFourEntry = base64;

			TakePhotoSucceeded?.Invoke(this, new PhotoSavedSuccessAlertEventArgs { Title = "File Location", Message = _file.Path });
			//await DisplayAlert("File Location", _file.Path, "OK");

			//_dogImage.Source = ImageSource.FromStream(() =>
			//{
			//    var stream = file.GetStream();
			//    file.Dispose();
			//    return stream;
			//});


			//or:
			//HANDLE VIA BINDING
			//_dogImage.Source = ImageSource.FromFile(_file.Path);

			this.PhotoURLEntry = _file.Path;

			//_dogImage.Source = ImageSource.FromFile(_file.Path);

			stream.Dispose();
            //don't dispose - you need this later
//			_file.Dispose();




		}


	}
}






//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


//using System;
//using System.Windows.Input;
//using ASampleApp.CosmosDB;
//using Xamarin.Forms;

//namespace ASampleApp
//{
//public class AddPuppyPhotoViewModel : BaseViewModel
//{
//string _firstLabel;
//string _firstEntryText;
//string _secondEntryText;
//string _photoURLEntry;
//string _photoSourceEntry;

//public ICommand MyFavoriteCommand { get; set; }
//public ICommand MySecondFavoriteCommand { get; set; }

//public string FirstLabel
//{
//  get { return _firstLabel; }
//  set { SetProperty(ref _firstLabel, value); }
//}

//public string FirstEntryText
//{
//  get { return _firstEntryText; }
//  set { SetProperty(ref _firstEntryText, value); }

//}

//public string SecondEntryText
//{
//  get { return _secondEntryText; }
//  set { SetProperty(ref _secondEntryText, value); }
//}

//public string PhotoURLEntry
//{
//  get { return _photoURLEntry; }
//  set
//  {
//      SetProperty(ref _photoURLEntry, value);
//      this.PhotoSourceEntry = _photoURLEntry;
//  }
//}

//public string PhotoSourceEntry
//{
//  get { return _photoSourceEntry; }
//  set { SetProperty(ref _photoSourceEntry, value); }
//}

//public AddPuppyPhotoViewModel()
//{
//  MyFavoriteCommand = new Command(OnMyFavoriteAction);
//}

//void OnMyFavoriteAction()
//{

//  //point 1
//  //App.DogRep.AddNewDogPhotoURL(this.FirstEntryText, this.SecondEntryText, this.PhotoURLEntry);

//  //point 2
//  App.DogRep.AddNewDogPhotoSource(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceEntry);
//  AddLastDogToCosmosDBAsync();
//  string _lastNameString = App.DogRep.GetLastDog().Name;
//  string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
//  this.FirstLabel = _lastNameStringAdd;

//  return;
//}

//private async void AddLastDogToCosmosDBAsync()
//{
//  var myDog = App.DogRep.GetLastDog();
//  var myCosmosDog = DogConverter.ConvertToCosmosDog(myDog);
//  await CosmosDB.CosmosDBService.PostCosmosDogAsync(myCosmosDog);
//}
//  }
//}

