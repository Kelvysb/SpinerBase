using BControls;
using Microsoft.WindowsAPICodePack.Dialogs;
using SpinerBase.Basic;
using SpinerBase.Layers.BackEnd;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for SpinerBaseTextResult.xaml
    /// </summary>
    public partial class uscTextResult : UserControl
    {

        #region Declarations
        private List<uscParameter> parametersControls;
        private Card card;
        #endregion

        #region Events

        public event EventHandler evRemove;
        protected virtual void onEvREmove()
        {
            evRemove?.Invoke(this, new EventArgs());
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbSaveResult();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbCopyResult();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbExecute();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                onEvREmove();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        #endregion

        #region Constructor
        public uscTextResult(Card p_card)
        {
            try
            {
                InitializeComponent();
                card = p_card;
                txtResult.Text = "";
                parametersControls = new List<uscParameter>();
                sbLoadParameters();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Functions

        private void sbExecute()
        {
            try
            {
                txtResult.Text = SpinerBaseBO.Instance.fnExecuteCardText(card);
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbCopyResult()
        {
            try
            {
                if (txtResult.Text.Trim() != "")
                {
                    Clipboard.SetText(txtResult.Text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbSaveResult()
        {
            CommonSaveFileDialog objDialog;
            StreamWriter objFile;

            try
            {

                if (txtResult.Text.Trim() != "")
                {
                    objDialog = new CommonSaveFileDialog();
                    objDialog.Title = Properties.Resources.ResourceManager.GetString("AppName").ToString();
                    objDialog.DefaultFileName = card.Name.Replace(" ", "_") + ".txt";
                    objDialog.DefaultExtension = "txt";
                    objDialog.Filters.Add(new CommonFileDialogFilter("Result Text", "*.txt"));

                    if (objDialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        objFile = new StreamWriter(objDialog.FileName);
                        objFile.Write(txtResult.Text);
                        objFile.Close();
                        objFile.Dispose();
                        objFile = null;
                        BMessage.Instance.fnMessage(Properties.Resources.ResourceManager.GetString("msgSaved").ToString(), Properties.Resources.ResourceManager.GetString("AppName").ToString(), MessageBoxButton.OK);
                    }

                    objDialog = null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
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
                    parametersControls.Add(new uscParameter(parameter));
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
