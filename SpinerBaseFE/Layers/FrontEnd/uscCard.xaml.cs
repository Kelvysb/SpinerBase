using BControls;
using SpinerBaseBE.Basic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpinerBaseBE.Layers.FrontEnd
{
    /// <summary>
    /// Interaction logic for SpinerBaseCard.xaml
    /// </summary>
    public partial class uscCard : UserControl
    {

        #region Declarations
        private Card card;
        #endregion

        #region Constructor        
        public uscCard(Card p_card)
        {
            try
            {
                InitializeComponent();
                card = p_card;
                sbLoad();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Events

        public event EventHandler evPlay;
        protected virtual void onEvPlay()
        {
            evPlay?.Invoke(this, new CardEventArgs(card));
        }

        public event EventHandler evEdit;
        protected virtual void onEvEdit()
        {
            evEdit?.Invoke(this, new CardEventArgs(card));
        }

        public event EventHandler evRemove;
        protected virtual void onEvRemove()
        {
            evRemove?.Invoke(this, new CardEventArgs(card));
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                onEvPlay();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void btnConfig_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                onEvEdit();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                onEvRemove();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }
        #endregion

        #region Function
        private void sbLoad()
        {
            try
            {
                lblName.Content = card.Name;
                lblDescription.Text = card.Description;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Properties
        public Card Card { get => card; set => card = value; }
        #endregion
    }
}
