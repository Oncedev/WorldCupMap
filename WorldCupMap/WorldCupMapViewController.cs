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


			var infoList = new List<WorldCupInfo> () {
				new WorldCupInfo("Algeria", 28.667122,2.677408, "algeria.png" ),
				new WorldCupInfo("Argentina", -35.003106,-64.741499, "argentina.png" ),
				new WorldCupInfo("Australia", -24.734834,134.248120, "australia.png" ),
				new WorldCupInfo("Belgium", 50.503887,4.469936, "belgium.png" ),
				new WorldCupInfo("Bosnia and Herzegovina", 42.5564516,15.7223665, "bosnia.png" ),
				new WorldCupInfo("Brazil", -14.235004,-51.92528, "brazil.png" ),
				new WorldCupInfo("Cameroon", 7.369721999999999,12.354722, "cameroon.png" ),
				new WorldCupInfo("Chile", -35.675147,-71.542969, "chile.png" ),
				new WorldCupInfo("Colombia", 4.570868,-74.29733299999999, "colombia.png" ),
				new WorldCupInfo("Costa Rica", 9.748916999999999,-83.753428, "costa-rica.png" ),
				new WorldCupInfo("Côte d'Ivoire", 7.539988999999999,-5.547079999999999, "cote-ivoire.png" ),
				new WorldCupInfo("Croatia", 45.1,15.2, "croatia.png" ),
				new WorldCupInfo("Ecuador", -1.831239,-78.18340599999999, "ecuador.png" ),
				new WorldCupInfo("England", 52.3555177,-1.1743197, "england.png" ),
				new WorldCupInfo("France", 46.227638,2.213749, "france.png" ),
				new WorldCupInfo("Germany", 51.165691,10.451526, "germany.png" ),
				new WorldCupInfo("Ghana", 7.946527,-1.023194, "ghana.png" ),
				new WorldCupInfo("Greece", 39.074208,21.824312, "greece.png" ),
				new WorldCupInfo("Honduras", 15.199999,-86.241905, "honduras.png" ),
				new WorldCupInfo("Iran", 32.427908,53.688046, "iran.png" ),
				new WorldCupInfo("Italy", 41.87194,12.56738, "italy.png" ),
				new WorldCupInfo("Japan", 36.204824,138.252924, "japan.png" ),
				new WorldCupInfo("Korea Republic", 35.907757,127.766922, "korea.png" ),
				new WorldCupInfo("Mexico", 23.634501,-102.552784, "mexico.png" ),
				new WorldCupInfo("Netherlands", 52.132633,5.291265999999999, "netherlands.png" ),
				new WorldCupInfo("Nigeria", 9.081999,8.675276999999999, "nigeria.png" ),
				new WorldCupInfo("Portugal", 39.39987199999999,-8.224454, "portugal.png" ),
				new WorldCupInfo("Russia", 61.52401,105.318756, "russia.png" ),
				new WorldCupInfo("Spain", 40.46366700000001,-3.74922, "spain.png" ),
				new WorldCupInfo("Switzerland", 46.818188,8.227511999999999, "switzerland.png" ),
				new WorldCupInfo("Uruguay", -32.522779,-55.765835, "uruguay.png" ),
				new WorldCupInfo("USA", 37.09024,-95.712891, "usa.png" ),
				new WorldCupInfo("Arena Fonte Nova", -12.97883,-38.5043711, "fonte-nova.jpeg" ),
				new WorldCupInfo("Estádio das Dunas", -5.8268266,-35.2124299, "arena-dunas.jpg" ),
				new WorldCupInfo("Estádio Castelão", -3.8072311,-38.5224338, "estadio-castelao.jpg" ),
				new WorldCupInfo("Arena Pernambuco", -8.0406496,-35.0082049, "arena-pernambuco.jpg" ),
				new WorldCupInfo("Arena Amazônia", -3.083142,-60.02810849999999, "arena-amazonia.jpg" ),
				new WorldCupInfo("Estádio Nacional", -15.7835191,-47.899211, "estadio-nacional.jpg" ),
				new WorldCupInfo("Arena Pantanal", -15.604017,-56.1216325, "arena-pantanal.jpg" ),
				new WorldCupInfo("Arena Corinthians", -23.5453331,-46.4737017, "arena-corinthians.jpg" ),
				new WorldCupInfo("Estádio do Maracanã", -22.9121089,-43.2301558, "estadio-maracana.jpg" ),
				new WorldCupInfo("Estádio Mineirão", -19.865867,-43.97113150000001, "estadio-mineirao.jpg" ),
				new WorldCupInfo("Arena da Baixada", -25.4482116,-49.2769866, "arena-baixada.jpg" ),
				new WorldCupInfo("Estádio Beira-Rio", -30.065066,-51.2365144, "arena-beira-rio.jpg")
			};				

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

