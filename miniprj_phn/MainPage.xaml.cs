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


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace miniprj_phn
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }
        IMobileServiceTable<Fleet> emp = App.new365Client.GetTable<Fleet>();
        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var loginemail = await emp.Where(p => p.busno == busno.Text).ToListAsync();

                if (loginemail[0].pwd == pwd.Password && loginemail[0].busno == busno.Text)
                {
                    App.busnoo = loginemail[0].busno;
                    Frame.Navigate(typeof(MainPage));
                    /* var x = grd.SelectedValue as Fleet;*/
                    Frame.Navigate(typeof(SecondPage), App.busnoo);
                }

                else
                {
                    MessageDialog delete = new MessageDialog("Please enter valid credentials and  try again later.");
                    delete.ShowAsync();
                }
            }
            catch (Exception exLogin)
            {
                MessageDialog delete = new MessageDialog("Error Occured due to low internet connectivity. Please try again later.");
                delete.ShowAsync();
            }
        }
    }

} 
