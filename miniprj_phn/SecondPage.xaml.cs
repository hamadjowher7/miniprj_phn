using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Windows.Devices.Geolocation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace miniprj_phn
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SecondPage : Page
    {
        string mylat;
        string mylong;
        string edity;
        int x = 1;
        public SecondPage()
        {
            this.InitializeComponent();
        }
        IMobileServiceTable<Locationnn> abc = App.new365Client.GetTable<Locationnn>();
        IMobileServiceTable<Fleet> xyz = App.new365Client.GetTable<Fleet>();
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                if (App.Geolocator == null)
                {
                    App.Geolocator = new Geolocator();
                    App.Geolocator.DesiredAccuracy = PositionAccuracy.High;
                    App.Geolocator.MovementThreshold = 20; // The units are meters.
                    App.Geolocator.PositionChanged += geolocator_PositionChanged;
                }
                edity = App.busnoo;
            }
            catch
            {
                MessageDialog delete = new MessageDialog("Error Occured due to low internet connectivity. Please try again later.");
                delete.ShowAsync();
                
            }
        }
      
        async void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {

            {
                if (!App.RunningInBackground)
                {
                    // Dispatcher.BeginInvoke(() =>
                    // { 
                
                    if (x == 10)
                    {
                        IMobileServiceTable<Locationnn> emp = App.new365Client.GetTable<Locationnn>();
                        Locationnn obj = new Locationnn();
                        obj.latttitude= args.Position.Coordinate.Latitude.ToString("0.0000000");
                        obj.lonngitude = args.Position.Coordinate.Longitude.ToString("0.0000000");
                        obj.busno = edity;

                        await emp.InsertAsync(obj);
                        x = 2;
                    }
                    else
                    {
                        IMobileServiceTable<Locationnn> emp = App.new365Client.GetTable<Locationnn>();
                        Locationnn obj = new Locationnn();
                        var usernewDetails = await emp.Where(p => p.busno == edity).ToListAsync();
                        var y = usernewDetails.ToArray()[0];
                        y.latttitude = args.Position.Coordinate.Latitude.ToString("0.0000000");
                        y.lonngitude= args.Position.Coordinate.Longitude.ToString("0.0000000");
                        
                        await emp.UpdateAsync(y);
                    }


                    // });
                }
                else
                {
                    IMobileServiceTable<Locationnn> emp = App.new365Client.GetTable<Locationnn>();
                    Locationnn obj = new Locationnn();
                    var usernewDetails = await emp.Where(p => p.busno == edity).ToListAsync();
                    var y = usernewDetails.ToArray()[0];
                    y.lonngitude = args.Position.Coordinate.Latitude.ToString("0.0000000");
                    y.latttitude = args.Position.Coordinate.Longitude.ToString("0.0000000");
                   
                    await emp.UpdateAsync(y);


                }
                
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var taskassign = await xyz.Where(p => p.busno == edity).ToListAsync();
                if(pwd.Password==taskassign[0].pwd)
                {
                    Application.Current.Exit();
                }
                else
                {
                    MessageDialog delete = new MessageDialog("Passwords Do Not Match. Please try again.");
                    delete.ShowAsync();
                }

            }
            catch
            {
                MessageDialog delete = new MessageDialog("Poor Internet Connectivity. Please try again.");
                delete.ShowAsync();
            }
        }
    }
}
