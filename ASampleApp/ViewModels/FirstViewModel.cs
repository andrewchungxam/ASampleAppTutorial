using System;
using System.Net.Http;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using Xamarin.Forms;

using ASampleApp.Data;
using ASampleApp.CosmosDB;

namespace ASampleApp
{
    public class FirstViewModel : BaseViewModel
    {

        string _firstLabel;
        string _firstEntryText;
        string _secondEntryText;

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


        public FirstViewModel()
        {
            MyFavoriteCommand = new Command(OnMyFavoriteAction);
            MySecondFavoriteCommand = new Command(OnMySecondFavoriteAction);
            //           MySecondFavoriteCommand = new Command(async () => await OnMySecondFavoriteCommand());

        }

        void OnMySecondFavoriteAction()
        {
            //DELETE THE LOCAL FILES
            App.DogPhotoRep.DeleteAllDogsPhoto();
            App.DogRepBlob.DeleteAllDogsPhoto();
            App.MyDogListPhotoPage.MyViewModel.DeleteAllDogsFromList();
            App.MyDogListPhotoBlobPage.MyViewModel.DeleteAllDogsFromList();


            return;
        }

        void OnMyFavoriteAction()
        {
            //DELETE FROM COSMOS DB
            HttpRequestMessage myDeleteRequest = new HttpRequestMessage(HttpMethod.Delete, String.Format("http://asampleapptutorial.azurewebsites.net/api/DeleteAllRecords"));
            App.myHttpClient.SendAsync(myDeleteRequest);

            //DELETE LOCAL DB
            OnMySecondFavoriteAction();
    
   //         //ADD NEW DOG
   //         App.DogRep.AddNewDog(this.FirstEntryText, this.SecondEntryText);
			//AddLastDogToCosmosDBAsync();

			////ADD DOG TO OBSERVABLE COLLECTION OF THE LISTVIEW
			//var tempLastDog = App.DogRep.GetLastDog();
            //App.MyDogListMVVMPage.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);

            //string _lastNameString = App.DogRep.GetLastDog().Name;

            //string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
            //this.FirstLabel = _lastNameStringAdd;

            return;

        }

		private async void AddLastDogToCosmosDBAsync()
		{
			var myDog = App.DogRep.GetLastDog();
			var myCosmosDog = DogConverter.ConvertToCosmosDog(myDog);
            await CosmosDB.CosmosDBService.PostCosmosDogAsync(myCosmosDog);
		}

    }
}
