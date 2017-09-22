//METHOD 1 - Observable Collection
using System;
using System.Threading.Tasks;

using Xamarin.Forms;

using ASampleApp.Models;

namespace ASampleApp
{
	public class DogListMVVMPage : BaseContentPage<DogListMVVMViewModel>
	{
		ListView _dogListView;

		public DogListMVVMPage()
		{
			_dogListView = new ListView();

			var myTemplate = new DataTemplate(typeof(DogViewCell));
			_dogListView.ItemTemplate = myTemplate;
			_dogListView.SetBinding(ListView.ItemsSourceProperty, nameof(MyViewModel.ObservableCollectionOfDogs));

			//_dogListView.HasUnevenRows = true;
			_dogListView.RowHeight = 58;

			Content = new StackLayout()
			{
				Children = {
					_dogListView
				}
			};
		}
	}


    public class DogViewCell : ViewCell
    {
        MenuItem _deleteAction;

        public DogViewCell()
        {
            AutomationId = "DogViewCell";

            var myTextProperty = new Label() { };//Text = "Text" };
            var myDetailProperty = new Label() { }; //Text = "Details" };

            var model = BindingContext as Dog;

            myTextProperty.SetBinding(Label.TextProperty, nameof(model.Name));
            myDetailProperty.SetBinding(Label.TextProperty, nameof(model.FurColor));

            var textStack = new StackLayout
            {
                //Padding = 10, //E
                //HorizontalOptions = LayoutOptions.FillAndExpand, //NE
                Margin = new Thickness(5, 2, 0, 0),
                //VerticalOptions = LayoutOptions.FillAndExpand, //NE
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    myTextProperty,
                    myDetailProperty
                }
            };

            var cellLayoutStack = new StackLayout
            {
                Margin = new Thickness(0, 0, 0, 0),
                //Spacing = 10, //default is 6
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    //dogImage,
                    textStack
                }

            };

            View = textStack;

            //MenuItem ITEM AND CONTEXT ACTIONS

            //var moreAction = new MenuItem { Text = "More" };
            //moreAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            //moreAction.Clicked += async (sender, e) =>
            //{
            //    var mi = ((MenuItem)sender);
            //    Debug.WriteLine("More Context Action clicked: " + mi.CommandParameter);
            //};
            // add to the ViewCell's ContextActions property
            //ContextActions.Add(moreAction);


            _deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
            _deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));

            //_deleteAction.Clicked += async (sender, e) =>
            //{
            //    var mi = ((MenuItem)sender);
            //    Debug.WriteLine("Delete Context Action clicked: " + mi.CommandParameter);
            //};

			_deleteAction.Clicked += HandleDeleteActionClicked;
            ContextActions.Add(_deleteAction);
        }

        ~DogViewCell()
        {
            _deleteAction.Clicked -= HandleDeleteActionClicked;
        }

        private async void HandleDeleteActionClicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            var myMenuItem = (MenuItem)sender;

			
            var selectedModel = myMenuItem.BindingContext as Dog;
            
			if (selectedModel != null)
            {
				//DELETE FROM DATABASE - AND BEFORE REFRESH THE DATA SOURCE ON THE UI 
				App.DogRep.DeleteDog(selectedModel);
            }
            //Wait for the iOS animation to finish
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    await Task.Delay(250);
                    break;
            }

            //var navigationPage = Application.Current.MainPage as NavigationPage;
            //var dogListPhotoPage = navigationPage.CurrentPage as DogListMVVMPage;
            var dogListPhotoViewModel = App.MyDogListMVVMPage.BindingContext as DogListMVVMViewModel;

            //dogListPhotoViewModel.RefreshAllDataCommand?.Execute(true); //BM
            //BM - refresh all the data

            var myMenuItemCommandParameter = (Dog)((MenuItem)sender).CommandParameter;

            //MA - just remove the specific item and the observable collection + INotifyPropertyChanged will auto-update the UI as necessary
            dogListPhotoViewModel.DeleteDogFromListCommand.Execute(myMenuItemCommandParameter);

            //FormList.Remove(item); //MA

        }
    }


}

//METHOD 2 - IList 
//using System;
//using Xamarin.Forms;
//namespace ASampleApp
//{
//	public class DogListMVVMPage : BaseContentPage<DogListMVVMViewModel>
//	{

//		ListView _dogListView;

//		public DogListMVVMPage ()
//		{
//			_dogListView = new ListView ();


//			var myTemplate = new DataTemplate (typeof (TextCell));
//			_dogListView.ItemTemplate = myTemplate;
//			myTemplate.SetBinding (TextCell.TextProperty, "Name");
//			myTemplate.SetBinding (TextCell.DetailProperty, "FurColor");

//			_dogListView.SetBinding (ListView.ItemsSourceProperty, nameof (MyViewModel.ListOfDogs));

//			Content = new StackLayout () {
//				Children = {
//					_dogListView
//				}
//			};
//		}



//	}
//}
