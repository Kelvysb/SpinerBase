using BControls;
using SpinerBase.Basic;
using SpinerBase.Layers.BackEnd;
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
using static SpinerBase.Layers.BackEnd.SpinerBaseBO;

namespace SpinerBase.Layers.FrontEnd
{
    /// <summary>
    /// Interaction logic for uscReport.xaml
    /// </summary>
    public partial class uscReport : UserControl
    {

        #region Declarations
        private Card objCard;
        #endregion

        #region Constructors
        public uscReport(Card p_card)
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
        public event EventHandler evRemove;
        protected virtual void onEvREmove()
        {
            evRemove?.Invoke(this, new EventArgs());
        }

        public event EventHandler evBeginWait;
        protected virtual void onEvBeginWait()
        {
            evBeginWait?.Invoke(this, new EventArgs());
        }

        public event EventHandler evEndWait;
        protected virtual void onEvEndWait()
        {
            evEndWait?.Invoke(this, new EventArgs());
        }
        #endregion

        #region Methods
     
        #endregion

    }
}
