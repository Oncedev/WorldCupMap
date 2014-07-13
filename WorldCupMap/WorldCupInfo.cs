using System;

namespace WorldCupMap
{
	public class WorldCupInfo
	{
		public WorldCupInfo (string title, double latitude,
			double longitude, string image)
		{
			Title = title;
			Latitude = latitude;
			Longitude = longitude;
			Image = image;
		}

		public string Title {
			get;
			set;
		}

		public double Latitude {
			get;
			set;
		}

		public double Longitude {
			get;
			set;
		}

		public string Image {
			get;
			set;
		}
	}
}

