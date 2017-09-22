using System;
using System.Threading.Tasks;
using ASampleApp.BlobStorage;
using ASampleApp.Pages;
using Microsoft.Azure.Mobile.Crashes;
using Xamarin.Forms;
namespace ASampleApp
{
	public class FirstPage : BaseContentPage<FirstViewModel>
	{
        Button _crashButton;

		Label _firstLabel;
		//Entry _firstEntry;
        //Entry _secondEntry;
		Button _firstButton;
        Button _clearLocalDBButton;

        //Button _goToDogListButton;

		Button _addAddDogPhotoButton;
		Button _addAddDogPhotoURLButton;
        Button _goToDogPhotoListButton;

		Button _addAddDogPhotoBase64Button;
		Button _goToDogPhotoBase64ListButton;

        //BLOB STORAGE
        Button _addAddDogPhotoBlobButton;
        Button _goToDogPhotoBlobListButton;

		Label _emptyLabel;
        Label _emptyLabel2;

		public FirstPage ()
		{

            this.Title = "A Sample App Tutorial";

		

            //CREATE A BUTTON THAT CRASHES THE APP FOR MOBILE CENTER
			_crashButton = new Button
			{
				Text = "Click to Crash App",
				//TextColor = Color.White,
				//BackgroundColor = Color.Transparent
                //AutomationId = AutomationIdConstants.CrashButton
			};

			//METHOD#1 non-MVVM
			//BindingContext = new FirstViewModel ();

			_firstLabel = new Label (); //{ Text = "Hello"};
            //_firstEntry = new Entry () {Placeholder = "Dog Name"};
            //_secondEntry = new Entry() { Placeholder = "Fur color" };

			_firstButton = new Button () { Text = "Clear Cosmos and Local DB" };
            _clearLocalDBButton = new Button() { Text = "Clear Local DB" };



            //_goToDogListButton = new Button () { Text = "Go to Dog List" };

			_emptyLabel = new Label () { Text = " " };
			_emptyLabel2 = new Label() { Text = " " };

			

			//ADD PHOTO BUTTONS
			_addAddDogPhotoButton = new Button() { Text = "Add Dog Photo" };
            _addAddDogPhotoURLButton = new Button { Text = "Add Dog Photo URL" };
			_addAddDogPhotoBase64Button = new Button { Text = "Add Dog Photo Base64" };
            _addAddDogPhotoBlobButton = new Button() { Text = "Add Dog Photo Blob" }; 
		
			//ADD LIST BUTTON
			_goToDogPhotoListButton = new Button() { Text = "Go to Dog Photo List"};
 //           _goToDogPhotoBase64ListButton = new Button() { Text = "Go to Dog Photo Base64 List" }; 
            _goToDogPhotoBlobListButton = new Button() { Text = "Go to Dog Photo Blob List"};

			//METHOD#2 MVVM
			//
            _firstLabel.SetBinding (Label.TextProperty, nameof (MyViewModel.FirstLabel), BindingMode.TwoWay);
			//_firstEntry.SetBinding (Entry.TextProperty, nameof (MyViewModel.FirstEntryText));
			//_secondEntry.SetBinding(Entry.TextProperty, nameof(MyViewModel.SecondEntryText));

			_firstButton.SetBinding (Button.CommandProperty, nameof (MyViewModel.MyFavoriteCommand));
            _clearLocalDBButton.SetBinding(Button.CommandProperty, nameof(MyViewModel.MySecondFavoriteCommand));




			var mainStackLayout = new StackLayout() { 
                Margin = 20,
				Children =
				{


					_firstLabel,
					//_firstEntry,
					//_secondEntry,
					_firstButton,
                    //_clearLocalDBButton, //COMBINE THE COSMOS DB AND LOCAL DB DELETION

					//_goToDogListButton,

					_emptyLabel,
					_addAddDogPhotoButton,
					_addAddDogPhotoURLButton,
					_goToDogPhotoListButton,
					_emptyLabel2,
					//_addAddDogPhotoBase64Button,
					//_goToDogPhotoBase64ListButton
                    _addAddDogPhotoBlobButton,
                    _goToDogPhotoBlobListButton,
					_emptyLabel2,
					_crashButton




		}
            
            };

			var myScrollView = new ScrollView() { };
            myScrollView.Content = mainStackLayout;

            Content = myScrollView;


		}

		protected override void OnAppearing ()
		{
            Task.Run(async () => await BlobStorage.AzureBlobStorage.performBlobOperation());

			base.OnAppearing ();

            //if (_firstEntry.Text != null)
            //{
            //    _firstEntry.Text = string.Empty;
            //}

            //if(_secondEntry.Text != null)
            //{
            //    _secondEntry.Text = string.Empty;
            //}

			if (_firstLabel.Text != null)
			{
				_firstLabel.Text = string.Empty;
			}


			//METHOD 1
			//			_firstButton.Clicked += OnFirstButtonClicked;

			//_goToDogListButton.Clicked += OnToDogListClicked;
			_addAddDogPhotoButton.Clicked += OnAddDogPhotoButtonClicked;
			_addAddDogPhotoURLButton.Clicked += OnAddDogPhotoURLButtonClicked;
            _addAddDogPhotoBase64Button.Clicked += OnAddDogPhotoBase64ButtonClicked;
			_goToDogPhotoListButton.Clicked += OnAddDogPhotoListLButtonClicked;
			//_goToDogPhotoBase64ListButton.Clicked += OnAddDogPhotoBase64ListButtonClicked;
            _addAddDogPhotoBlobButton.Clicked += OnAddDogPhotoBlobButtonClicked;
            _goToDogPhotoBlobListButton.Clicked += OnAddDogPhotoBlogListButtonClicked;

            _crashButton.Clicked += OnCrashButtonClicked;



		}



        protected override void OnDisappearing ()
		{
			base.OnDisappearing ();

			//METHOD 1
			//			_firstButton.Clicked -= OnFirstButtonClicked;
			//_goToDogListButton.Clicked -= OnToDogListClicked;
            _addAddDogPhotoButton.Clicked -= OnAddDogPhotoButtonClicked;
			_addAddDogPhotoURLButton.Clicked -= OnAddDogPhotoURLButtonClicked;
			_addAddDogPhotoBase64Button.Clicked -= OnAddDogPhotoBase64ButtonClicked;
			_goToDogPhotoListButton.Clicked -= OnAddDogPhotoListLButtonClicked;
	//		_goToDogPhotoBase64ListButton.Clicked -= OnAddDogPhotoBase64ListButtonClicked;
			_addAddDogPhotoBlobButton.Clicked -= OnAddDogPhotoBlobButtonClicked;
			_goToDogPhotoBlobListButton.Clicked -= OnAddDogPhotoBlogListButtonClicked;

			_crashButton.Clicked -= OnCrashButtonClicked;

		}

		private void OnCrashButtonClicked(object sender, EventArgs e)
		{
			Crashes.GenerateTestCrash();
			throw new Exception("Crash Button Tapped");
		}

		private void OnAddDogPhotoButtonClicked(object sender, EventArgs e)
        {
//            throw new NotImplementedException();
            Device.BeginInvokeOnMainThread(()=> Navigation.PushAsync(new AddPuppyPhotoPage()));
        }

		void OnAddDogPhotoURLButtonClicked (object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread (()=>Navigation.PushAsync (new AddDogPhotoURLPage()));	
		}

		//ADD PHOTO
		private void OnAddDogPhotoBase64ButtonClicked(object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(new AddDogPhotoBaseSixtyFourPage()));
		}

		private void OnAddDogPhotoBlobButtonClicked(object sender, EventArgs e)
		{

            Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(new AddDogPhotoBlobPage()));
			//          Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(new AddDogPhotoBlobPage()));
			//          throw new NotImplementedException();
		}


		private void OnAddDogPhotoBlogListButtonClicked(object sender, EventArgs e)
		{
            Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(App.MyDogListPhotoBlobPage));

			//          Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(App.MyDogListPhotoBlobPage));
			//          throw new NotImplementedException();
		}


		void OnToDogListClicked(object sender, EventArgs e)
		{
			//OPTION 1
			//Device.BeginInvokeOnMainThread (() => Navigation.PushAsync (new DogListPage ()));

			//OPTION 2
            Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(App.MyDogListMVVMPage));
		}


		void OnAddDogPhotoListLButtonClicked(object sender, EventArgs e)
		{
			//Option 1 - creating a new DogListPhotoPage
			//THIS OPTION IS NOT COMPATIBLE WITH //ADD DOG TO OBSERVABLE COLLECTION OF THE LISTVIEW
			//Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(new DogListPhotoPage()));

			//TODO - using a static DogListPhotoPage
			//Option 2 - using a static DogListPhotoBase64Page
            Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(App.MyDogListPhotoPage));

		}

		//LIST
		void OnAddDogPhotoBase64ListButtonClicked(object sender, EventArgs e)
		{
			//Option 1 - creating a new DogListPhotoPage
			//Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(new DogListPhotoBase64Page()));

			//TODO - using a static DogListPhotoBase64Page
			//Option 2 - using a static DogListPhotoBase64Page
			Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(App.MyDogListPhotoBase64Page));

		}

	}
}
