using System;
using CoreLocation;
using UIKit;

namespace MyGeoFenceiOS
{
    public partial class ViewController : UIViewController
    {

        CLLocationManager locMngr = new CLLocationManager(); 
        partial void UIButton412_TouchUpInside(UIButton sender)
        {
            CLLocationCoordinate2D loc = new CLLocationCoordinate2D(Double.Parse(txtLoc.Text),Double.Parse(txtLong.Text));
            map.Camera.CenterCoordinate = loc;
            map.Camera.Altitude = 500;
            //map.Camera.Pitch = 45;
            map.ShowsBuildings = true; // Only works on the device
            map.PitchEnabled = true;    
        }

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            map.MapType = MapKit.MKMapType.Hybrid;
            map.ShowsUserLocation = true;

            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0) == true)
                locMngr.RequestWhenInUseAuthorization();
            UpdateUI(locMngr.Location);
            if(CLLocationManager.LocationServicesEnabled)
            {
                locMngr.LocationsUpdated += OnLocationChanged;
                locMngr.StartUpdatingLocation();
            }
            locMngr.PausesLocationUpdatesAutomatically = false; // Battery Intensive
            // Perform any additional setup after loading the view, typically from a nib.
        }

        private void OnLocationChanged(object sender, CLLocationsUpdatedEventArgs e)
        {
            CLLocation newLocation = e.Locations[e.Locations.Length - 1];
            //UpdateUI(newLocation);
        }
        private void UpdateUI(CLLocation locData)
        {
            map.Camera.CenterCoordinate = locData.Coordinate;
            map.Camera.Altitude = 500;
            //map.Camera.Pitch = 45;
            map.ShowsBuildings = true; // Only works on the device
            map.PitchEnabled = true;

        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
