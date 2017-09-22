
using System;
using System.Collections.Generic;
using SQLite;

using ASampleApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ASampleApp.Data
{
	public class DogRepositoryBlob
	{

		public DogRepositoryBlob()
		{
			IfEmptyCheckCosmosDB();
		}

		private void IfEmptyCheckCosmosDB()
		{
			var list = new List<Dog> { };
            list = this.GetAllDogsBlob();

			if (!list.Any()) //if LIST == EMPTY
			{
                var myListOfCosmosDogs = Task.Run(async () => await CosmosDB.CosmosDBServicePhoto.GetAllCosmosDogs()).Result;
				foreach (var item in myListOfCosmosDogs)
				{
					var tempDog = CosmosDB.DogConverter.ConvertToDog(item);
                    this.AddNewDogPhotoSourceBlob(tempDog.Name, tempDog.FurColor, tempDog.DogPictureSource);

					//TODO: MW
					App.MyDogListPhotoBase64Page.MyViewModel._observableCollectionOfDogs.Add(tempDog);
					//_observableCollectionOfDogs.Add(item);
				}
			}
		}


		private SQLiteConnection sqliteConnection;

		public DogRepositoryBlob(string dbPath)
		{
			sqliteConnection = new SQLiteConnection(dbPath);
			sqliteConnection.CreateTable<Dog>();

		}

		public void AddNewDogBlob(string name, string furColor)
		{
			sqliteConnection.Insert(new Dog
			{
				Name = name,
				FurColor = furColor,
				//let's add a default dog image for entries via the text only field
				DogPictureSource = "https://s-media-cache-ak0.pinimg.com/736x/4b/c2/ac/4bc2acd1af5130a668a4c391805f3f29--teacup-poodle-puppies-teacup-poodles.jpg"
			});

		}

		public void DeleteDogBlob(Dog dog)
		{
			sqliteConnection.Delete(dog);
		}

		public void AddNewDogPhotoURLBlob(string name, string furColor, string dogURLBlob)
		{
            sqliteConnection.Insert(new Dog { Name = name, FurColor = furColor, DogPictureURLBlob = dogURLBlob });
		}

        //NOT NEEDED
		public void AddNewDogPhotoFileBlob(string name, string furColor, string dogFile)
		{
			sqliteConnection.Insert(new Dog { Name = name, FurColor = furColor, DogPictureFile = dogFile });
		}


		public void AddNewDogPhotoSourceBlob(string name, string furColor, string dogSource)
		{
			sqliteConnection.Insert(new Dog { Name = name, FurColor = furColor, DogPictureSource = dogSource });
		}


		public List<Dog> GetAllDogsBlob()
		{
			return sqliteConnection.Table<Dog>().ToList();
		}

		public Dog GetFirstDogBlob()
		{
			return sqliteConnection.Table<Dog>().FirstOrDefault();
		}

		public Dog GetLastDogBlob()
		{
			return sqliteConnection.Table<Dog>().LastOrDefault();
		}

	}
}

