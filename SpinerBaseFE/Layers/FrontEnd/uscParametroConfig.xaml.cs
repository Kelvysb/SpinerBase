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
    /// Interaction logic for uscParametroConfig.xaml
    /// </summary>
    public partial class uscParameterConfig : UserControl
    {
        #region Declarations

        #endregion

        #region Events
        private void txtDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sbUpdateValue();
            }
            catch (Exception)
            {
                throw;
            }
        }        
        #endregion

        #region Constructor
        public uscParameterConfig(Parameter p_parameter)
        {
            try
            {
                InitializeComponent();
                parameter = p_parameter;
                Update();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Functions
        public void Update()
        {
            try
            {
                lblTag.Content = parameter.Tag;
                txtDescription.Text = parameter.Description;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void sbUpdateValue()
        {
            try
            {
                parameter.Description = txtDescription.Text;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Properties
        public Parameter parameter { get; set; }
        #endregion

    }
}
