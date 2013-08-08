using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Media.PhoneExtensions;

namespace HTML5App2
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Url of Home page
        private string MainUri = "/Html/index.html";

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Browser_Loaded(object sender, RoutedEventArgs e)
        {
            Browser.IsScriptEnabled = true;

            // Add your URL here
            Browser.Navigate(new Uri(MainUri, UriKind.Relative));
        }

        // Navigates back in the web browser's navigation stack, not the applications.
        private void BackApplicationBar_Click(object sender, EventArgs e)
        {
            Browser.GoBack();
        }

        // Navigates forward in the web browser's navigation stack, not the applications.
        private void ForwardApplicationBar_Click(object sender, EventArgs e)
        {
            Browser.GoForward();
        }

        // Navigates to the initial "home" page.
        private void HomeMenuItem_Click(object sender, EventArgs e)
        {
            Browser.Navigate(new Uri(MainUri, UriKind.Relative));
        }

        // Handle navigation failures.
        private void Browser_NavigationFailed(object sender, System.Windows.Navigation.NavigationFailedEventArgs e)
        {
            MessageBox.Show("Navigation to this page failed, check your internet connection");
        }

        private void Browser_ScriptNotify(object sender, NotifyEventArgs e)
        {
            string fileName = "01_Susies_dnc_09-05-12.mp3";
            var resource = Application.GetResourceStream(new Uri(@"media/" + fileName, UriKind.Relative));
            IsolatedStorageFileStream fileStream = IsolatedStorageFile.GetUserStoreForApplication().CreateFile(fileName);
            resource.Stream.CopyTo(fileStream, 4096);
            resource.Stream.Close();
            fileStream.Close();

            Uri fileUri = new Uri(fileName, UriKind.RelativeOrAbsolute);
            var ml = new MediaLibrary();

            try
            {
                MediaLibraryExtensions.SaveSong(ml, fileUri, null, SaveSongOperation.CopyToLibrary);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message.ToString();
            }
        }
    }
}
