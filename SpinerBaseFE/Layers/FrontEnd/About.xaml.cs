using BControls;
using System;
using System.Diagnostics;
using System.Windows;

namespace SpinerBase.Layers.FrontEnd
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {

        #region Events
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }



        private void hypGithub_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void hypKelvysb_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    
        #region Constructor
        public About()
        {
            InitializeComponent();
        }
        #endregion
      
    }
}
