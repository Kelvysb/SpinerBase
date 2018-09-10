using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BControls;
using SpinerBaseBE.Basic;
using SpinerBaseBE.Layers.BackEnd;

namespace SpinerBaseBE.Layers.FrontEnd
{
    /// <summary>
    /// Interaction logic for SpinerBaseMain.xaml
    /// </summary>
    public partial class SpinerBaseMain : Window
    {


        #region Declarations
        private List<uscCard> objCards;
        #endregion

        #region Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sbLoadPage();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbAddCard();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sbFilter();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbConnect();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }
        #endregion

        #region Constructor
        public SpinerBaseMain()
        {
            try
            {
                InitializeComponent();
                BMessage.sbInitialize((Brush)FindResource("BaseColor"),
                                        (Brush)FindResource("BackColor"), 
                                        (Brush)FindResource("FontColor"), 
                                        (Brush)FindResource("FontColor"), 
                                        (FontFamily)FindResource("Font"),
                                        Environment.CurrentDirectory + "\\Log");

                SpinerBaseBO.InitiateInstance(Environment.CurrentDirectory + "\\SpinerBaseData.json");

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Functions
        private void sbLoadPage()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception((String)FindResource("msgError"), ex);
            }
        }

        private void sbAddCard()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception((String)FindResource("msgError"), ex);
            }
        }

        private void sbEditCard(Card p_card)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception((String)FindResource("msgError"), ex);
            }
        }

        private void sbSelectCard(Card p_card)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception((String)FindResource("msgError"), ex);
            }

        }

        private void sbRemoveCard(Card p_card)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception((String)FindResource("msgError"), ex);
            }
        }

        private void sbFilter()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception((String)FindResource("msgError"), ex);
            }
        }

        private void sbConnect()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception((String)FindResource("msgError"), ex);
            }
        }

        private void sbClose()
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                throw new Exception((String)FindResource("msgError"), ex);
            }
        }

        #endregion

        #region Properties

        #endregion

        
    }
}
