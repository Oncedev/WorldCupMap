using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using System.Collections.Generic;
using MonoTouch.CoreLocation;

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
				new object[] {"Algeria", 28.667122,2.677408},
				new object[] {"Argentina", -35.003106,-64.741499},
				new object[] {"Australia", -24.734834,134.248120},
				new object[] {"Belgium", 50.503887,4.469936},
				new object[] {"Bosnia and Herzegovina", 42.5564516,15.7223665},
				new object[] {"Brazil", -14.235004,-51.92528},
				new object[] {"Cameroon", 7.369721999999999,12.354722},
				new object[] {"Chile", -35.675147,-71.542969},
				new object[] {"Colombia", 4.570868,-74.29733299999999},
				new object[] {"Costa Rica", 9.748916999999999,-83.753428},
				new object[] {"Côte d'Ivoire", 7.539988999999999,-5.547079999999999},
				new object[] {"Croatia", 45.1,15.2},
				new object[] {"Ecuador", -1.831239,-78.18340599999999},
				new object[] {"England", 52.3555177,-1.1743197},
				new object[] {"France", 46.227638,2.213749},
				new object[] {"Germany", 51.165691,10.451526},
				new object[] {"Ghana", 7.946527,-1.023194},
				new object[] {"Greece", 39.074208,21.824312},
				new object[] {"Honduras", 15.199999,-86.241905},
				new object[] {"Iran", 32.427908,53.688046},
				new object[] {"Italy", 41.87194,12.56738},
				new object[] {"Japan", 36.204824,138.252924},
				new object[] {"Korea Republic", 35.907757,127.766922},
				new object[] {"Mexico", 23.634501,-102.552784},
				new object[] {"Netherlands", 52.132633,5.291265999999999},
				new object[] {"Nigeria", 9.081999,8.675276999999999},
				new object[] {"Portugal", 39.39987199999999,-8.224454},
				new object[] {"Russia", 61.52401,105.318756},
				new object[] {"Spain", 40.46366700000001,-3.74922},
				new object[] {"Switzerland", 46.818188,8.227511999999999},
				new object[] {"Uruguay", -32.522779,-55.765835},
				new object[] {"USA", 37.09024,-95.712891},
				new object[] {"Arena Fonte Nova", -12.97883,-38.5043711},
				new object[] {"Estádio das Dunas", -5.8268266,-35.2124299},
				new object[] {"Estádio Castelão", -3.8072311,-38.5224338},
				new object[] {"Arena Pernambuco", -8.0406496,-35.0082049},
				new object[] {"Arena Amazônia", -3.083142,-60.02810849999999},
				new object[] {"Estádio Nacional (Mané Garrincha)", -15.7835191,-47.899211},
				new object[] {"Arena Pantanal", -15.604017,-56.1216325},
				new object[] {"Arena Corinthians", -23.5453331,-46.4737017},
				new object[] {"Estádio do Maracanã", -22.9121089,-43.2301558},
				new object[] {"Estádio Mineirão", -19.865867,-43.97113150000001},
				new object[] {"Arena da Baixada", -25.4482116,-49.2769866},
				new object[] {"Estádio Beira-Rio", -30.065066,-51.2365144}
			};

			var infoList = new List<WorldCupInfo> ();
			foreach (var info in infoMatrix) {
				infoList.Add (new WorldCupInfo () {
					Title = (string) info[0],
					Latitude = (double) info[1],
					Longitude = (double) info[2]
				});
			}

			return infoList;
																																																		
		}

		public override void LoadView ()
		{
			map = new MKMapView (UIScreen.MainScreen.Bounds);
			View = map;

			_infoList.ForEach (x => map.AddAnnotation (new MKPointAnnotation () {
				Title = x.Title,
				Coordinate = new CLLocationCoordinate2D() {
					Latitude = x.Latitude,
					Longitude = x.Longitude
				}
			}));
		}
	}
}

