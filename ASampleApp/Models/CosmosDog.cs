using System;
using Newtonsoft.Json;

namespace ASampleApp.Models
{
    public class CosmosDog
    {
		//MAIN DIFFERENCE IS ID IS STRING FOR COSMOS DB
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "furColor")]
		public string FurColor { get; set; }

        //for dog picture urls
		[JsonProperty(PropertyName = "dogPictureURL")]
		public string DogPictureURL { get; set; }

        //for dog picture blobs
		[JsonProperty(PropertyName = "dogPictureURLBlob")]
		public string DogPictureURLBlob { get; set; }

		[JsonProperty(PropertyName = "dogPictureFile")]
		public string DogPictureFile { get; set; }

		[JsonProperty(PropertyName = "dogPictureSource")]
		public string DogPictureSource { get; set; }

	}
}
