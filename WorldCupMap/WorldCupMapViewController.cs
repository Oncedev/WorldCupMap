using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using System.Collections.Generic;
using MonoTouch.CoreLocation;
using System.IO;

namespace WorldCupMap
{
	public partial class WorldCupMapViewController : UIViewController
	{
		protected MKMapView map;
		private List<WorldCupInfo> _infoList;

		public WorldCupMapViewController (IntPtr handle) : base (handle)
		{
			_infoList = GetInfoList ();
		}

		public List<WorldCupInfo> GetInfoList ()
		{
			var infoMatrix = new object[][] {
				new object[] {"Algeria", 28.667122,2.677408, "algeria.png" },
				new object[] {"Argentina", -35.003106,-64.741499, "argentina.png" },
				new object[] {"Australia", -24.734834,134.248120, "australia.png" },
				new object[] {"Belgium", 50.503887,4.469936, "belgium.png" },
				new object[] {"Bosnia and Herzegovina", 42.5564516,15.7223665, "bosnia.png" },
				new object[] {"Brazil", -14.235004,-51.92528, "brazil.png" },
				new object[] {"Cameroon", 7.369721999999999,12.354722, "cameroon.png" },
				new object[] {"Chile", -35.675147,-71.542969, "chile.png" },
				new object[] {"Colombia", 4.570868,-74.29733299999999, "colombia.png" },
				new object[] {"Costa Rica", 9.748916999999999,-83.753428, "costa-rica.png" },
				new object[] {"Côte d'Ivoire", 7.539988999999999,-5.547079999999999, "cote-ivoire.png" },
				new object[] {"Croatia", 45.1,15.2, "croatia.png" },
				new object[] {"Ecuador", -1.831239,-78.18340599999999, "ecuador.png" },
				new object[] {"England", 52.3555177,-1.1743197, "england.png" },
				new object[] {"France", 46.227638,2.213749, "france.png" },
				new object[] {"Germany", 51.165691,10.451526, "germany.png" },
				new object[] {"Ghana", 7.946527,-1.023194, "ghana.png" },
				new object[] {"Greece", 39.074208,21.824312, "greece.png" },
				new object[] {"Honduras", 15.199999,-86.241905, "honduras.png" },
				new object[] {"Iran", 32.427908,53.688046, "iran.png" },
				new object[] {"Italy", 41.87194,12.56738, "italy.png" },
				new object[] {"Japan", 36.204824,138.252924, "japan.png" },
				new object[] {"Korea Republic", 35.907757,127.766922, "korea.png" },
				new object[] {"Mexico", 23.634501,-102.552784, "mexico.png" },
				new object[] {"Netherlands", 52.132633,5.291265999999999, "netherlands.png" },
				new object[] {"Nigeria", 9.081999,8.675276999999999, "nigeria.png" },
				new object[] {"Portugal", 39.39987199999999,-8.224454, "portugal.png" },
				new object[] {"Russia", 61.52401,105.318756, "russia.png" },
				new object[] {"Spain", 40.46366700000001,-3.74922, "spain.png" },
				new object[] {"Switzerland", 46.818188,8.227511999999999, "switzerland.png" },
				new object[] {"Uruguay", -32.522779,-55.765835, "uruguay.png" },
				new object[] {"USA", 37.09024,-95.712891, "usa.png" },
				new object[] {"Arena Fonte Nova", -12.97883,-38.5043711, "fonte-nova.jpeg" },
				new object[] {"Estádio das Dunas", -5.8268266,-35.2124299, "arena-dunas.jpg" },
				new object[] {"Estádio Castelão", -3.8072311,-38.5224338, "estadio-castelao.jpg" },
				new object[] {"Arena Pernambuco", -8.0406496,-35.0082049, "arena-pernambuco.jpg" },
				new object[] {"Arena Amazônia", -3.083142,-60.02810849999999, "arena-amazonia.jpg" },
				new object[] {"Estádio Nacional (Mané Garrincha)", -15.7835191,-47.899211, "estadio-nacional.jpg" },
				new object[] {"Arena Pantanal", -15.604017,-56.1216325, "arena-pantanal.jpg" },
				new object[] {"Arena Corinthians", -23.5453331,-46.4737017, "arena-corinthians.jpg" },
				new object[] {"Estádio do Maracanã", -22.9121089,-43.2301558, "estadio-maracana.jpg" },
				new object[] {"Estádio Mineirão", -19.865867,-43.97113150000001, "estadio-mineirao.jpg" },
				new object[] {"Arena da Baixada", -25.4482116,-49.2769866, "arena-baixada.jpg" },
				new object[] {"Estádio Beira-Rio", -30.065066,-51.2365144, "arena-beira-rio.jpg" }
			};

			var infoList = new List<WorldCupInfo> ();
			foreach (var info in infoMatrix) {
				infoList.Add (new WorldCupInfo () {
					Title = (string) info[0],
					Latitude = (double) info[1],
					Longitude = (double) info[2],
					Image = (string) info[3]
				});
			}

			return infoList;
																																																		
		}

		public override void LoadView ()
		{
			map = new MKMapView (UIScreen.MainScreen.Bounds);
			map.Delegate = new MyMapDelegate (_infoList);
			View = map;

			_infoList.ForEach (x => map.AddAnnotation (new MKPointAnnotation () {
				Title = x.Title,
				Coordinate = new CLLocationCoordinate2D() {
					Latitude = x.Latitude,
					Longitude = x.Longitude
				}
			}));
		}		

		class MyMapDelegate : MKMapViewDelegate
		{
			private List<WorldCupInfo> _infoList;

			public MyMapDelegate(List<WorldCupInfo> infoList) 
			{
				_infoList = infoList;
			}

			string pId = "PinAnnotation";

			public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, NSObject annotation)
			{
				MKAnnotationView anView;

				if (annotation is MKUserLocation)
					return null; 

				// create pin annotation view
				anView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation (pId);

				if (anView == null)
					anView = new MKPinAnnotationView (annotation, pId);

				var pointAnnotation = (MKPointAnnotation) annotation;
				var info = _infoList.Find (x => x.Title == pointAnnotation.Title);					

				anView.Image = GetImage (info.Image);
				anView.CanShowCallout = true;

				return anView;
			}

			public UIImage GetImage(string imageName)
			{
				var documents =
					Environment.GetFolderPath (Environment.SpecialFolder.Resources);

				var filename = Path.Combine (documents, imageName);

				var image = UIImage.FromFile (filename).Scale(new SizeF() {Height=20, Width=30});

				return image;
			}
		}
	}
}

