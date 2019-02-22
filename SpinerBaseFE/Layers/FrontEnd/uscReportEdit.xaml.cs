using SpinerBase.Basic;
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

namespace SpinerBase.Layers.FrontEnd
{
    /// <summary>
    /// Interaction logic for uscReportEdit.xaml
    /// </summary>
    public partial class uscReportEdit : UserControl
    {

        #region Declarations
        private Card objCard;
        #endregion

        #region Constructors
        public uscReportEdit(Card p_card)
        {
            try
            {
                InitializeComponent();
                objCard = p_card;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Events
        public event EventHandler<CardEventArgs> evSaved;
        protected virtual void onEvSaved()
        {
            evSaved?.Invoke(this, new CardEventArgs(objCard));
        }
        #endregion

        #region Methods

        #endregion
    }
}
