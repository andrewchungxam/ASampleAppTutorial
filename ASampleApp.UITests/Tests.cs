using System;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace ASampleApp.UITests
{
//    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

		//[Ignore ("REPL Tests only for Testing/Developing")]
		//[Test]
		//public void Repl()
		//{
		//	app.Repl();
		//}


        [Test]
        public void AddDogAndDeleteItem()
        {
            Thread.Sleep(3000);
            app.EnterText("Dog Name", "Rover");
			app.DismissKeyboard();
			app.EnterText("Fur color", "Brown");
			app.DismissKeyboard();
			app.Tap(x => x.Class("UIButton").Marked("Submit"));
			app.Tap(x => x.Class("UIButton").Marked("Go to Dog List"));
            app.Tap(x => x.Marked("Back"));
            app.Tap(x => x.Class("UIButton").Marked("Go to Dog List"));

			try
			{
				app.SwipeRightToLeft(c => c.Marked("Rover"));
			}
			catch 
            { 
            }

            app.Tap(x => x.Marked("Delete"));
            Thread.Sleep(1500);

            Assert.IsFalse(app.Query(x => x.Marked("Rover")).Any());

			//Assert it does not exist

		}


        public void SelectFirstCellInList(int timeoutInSeconds = 20)
		{
			Func<AppQuery, AppQuery> firstCellInList;

			//if (OnAndroid)
			//	firstCellInList = x => x.Class("ViewCellRenderer_ViewCellContainer").Index(0);
			//else if (OniOS)
			firstCellInList = x => x.Marked("{AutomationId of ViewCell}").Index(0);
			app.WaitForElement(firstCellInList, "Timed our waiting for the first user to appear", TimeSpan.FromSeconds(timeoutInSeconds));
			app.Tap(firstCellInList);
		}
    }
}
