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
using BControls;
using SpinerBaseBE.Layers.BackEnd;

namespace SpinerBaseBE.Layers.FrontEnd
{
    /// <summary>
    /// Interaction logic for SpinerBaseCardConfig.xaml
    /// </summary>
    public partial class uscCardConfig : UserControl
    {

        #region Declarations
            private List<uscParameterConfig> parametersControls;
            private Card card;
        #endregion

        #region Events

        public event EventHandler evSaved;
        protected virtual void onEvSaved()
        {
            evSaved?.Invoke(this, new CardEventArgs(card));
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbSetParameters();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbSave();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void radMsSql_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                sbUpdateFileds();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void radGrid_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                sbUpdateFileds();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void txtCommand_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sbUpdateFileds();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }
        #endregion

        #region Constructor
        public uscCardConfig(Card p_card)
        {
            try
            {
                InitializeComponent();
                card = p_card;
                txtCommand.Text = card.Command;
                parametersControls = new List<uscParameterConfig>();
                sbLoadParameters();
                sbSetFields();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Functions
        private void sbSetParameters()
        {

            List<Parameter> objAuxParameters;
            int intauxIndex;

            try
            {
                sbSetFields();

                objAuxParameters = SpinerBaseBO.Instance.fnExtractParameters(card.Command);

                foreach (Parameter parameter in card.Parameters)
                {
                    intauxIndex = objAuxParameters.RemoveAll(par => par.Tag.Trim() == parameter.Tag.Trim());      
                }

                if(objAuxParameters.Count > 0)
                {
                    card.Parameters.AddRange(objAuxParameters);
                }

                sbLoadParameters();

            }
            catch (Exception ex)
            {
                throw new Exception((String)FindResource("msgError"), ex);
            }
        }

        private void sbSetFields()
        {
            try
            {
                if (card.DataBaseType == enmDataBaseType.MsSQL)
                {
                    radMsSql.IsChecked = true;
                }
                else if(card.DataBaseType == enmDataBaseType.MySQL)
                {
                    radMySql.IsChecked = true;
                }
                else
                {
                    radSqlite.IsChecked = true;
                }

                if (card.ResultType == enmResultType.Grid)
                {
                    radGrid.IsChecked = true;                    
                }
                else
                {
                    radText.IsChecked = true;
                }

                txtCommand.Text = card.Command;
            }
            catch (Exception ex)
            {
                throw new Exception((String)FindResource("msgError"), ex);
            }
        }

        private void sbUpdateFileds()
        {
            try
            {
                if ((bool)radMsSql.IsChecked)
                {
                    card.DataBaseType = enmDataBaseType.MsSQL;
                }
                else if((bool)radMySql.IsChecked)
                {
                    card.DataBaseType = enmDataBaseType.MySQL;
                }else
                {
                    card.DataBaseType = enmDataBaseType.SQLite;
                }

                if ((bool)radGrid.IsChecked)
                {
                    card.ResultType = enmResultType.Grid;
                }
                else
                {
                    card.ResultType = enmResultType.Text;
                }

                card.Command = txtCommand.Text;

            }
            catch (Exception ex)
            {
                throw new Exception((String)FindResource("msgError"), ex);
            }
        }

        private void sbSave()
        {
            try
            {
                sbUpdateFileds();
                onEvSaved();
            }
            catch (Exception ex)
            {
                throw new Exception((String)FindResource("msgError"), ex);
            }
        }

        private void sbLoadParameters()
        {
            try
            {

                parametersControls.Clear();
                stkParameters.Children.Clear();

                foreach (Parameter parameter in card.Parameters)
                {
                    parametersControls.Add(new uscParameterConfig(parameter));
                    parametersControls.Last().Margin = new Thickness(2);
                    stkParameters.Children.Add(parametersControls.Last());
                }

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
