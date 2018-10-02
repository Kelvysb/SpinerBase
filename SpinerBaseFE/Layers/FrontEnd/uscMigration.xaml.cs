using BControls;
using Microsoft.WindowsAPICodePack.Dialogs;
using SpinerBase.Basic;
using SpinerBase.Layers.BackEnd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using static SpinerBase.Layers.BackEnd.SpinerBaseBO;

namespace SpinerBase.Layers.FrontEnd
{
    /// <summary>
    /// Interaction logic for uscMigration.xaml
    /// </summary>
    public partial class uscMigration : UserControl
    {

        #region Declarations
        private List<uscParameter> parametersControls;
        private Card card;
        private Thread objThreadExecution;
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
                sbSaveLog();
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

        private void objBOMigrate_Progress(object sender, SpinerBaseEventArgs e)
        {
            try
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    sbAddLog(e);
                }));
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }


        private void objBOMigrate_Finished(object sender, EventArgs e)
        {
            try
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    onEvEndWait();
                }));
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        #endregion

        #region Constructor
        public uscMigration(Card p_card)
        {
            try
            {
                InitializeComponent();
                card = p_card;
                ListLog.Items.Clear();
                parametersControls = new List<uscParameter>();
                sbLoadParameters();

                if (card.TargetConnection is null == false)
                {
                    lblConnection.Content = card.TargetConnection.Name.Trim() + " - " + card.TargetConnection.DataBaseType.ToString();
                }
                else
                {
                    lblConnection.Content = "Not Connected.";
                }

                SpinerBaseBO.Instance.evProgress += objBOMigrate_Progress;
                SpinerBaseBO.Instance.evFinished += objBOMigrate_Finished;

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
                onEvBeginWait();
                ListLog.Items.Clear();
                Thread.Sleep(100);
                objThreadExecution = new Thread(new ParameterizedThreadStart(SpinerBaseBO.Instance.sbDoMigration));
                objThreadExecution.Start(card);

            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }

        }

        private void sbConnect()
        {
            ConnectionSelect objConnectionSelect;
            try
            {
                objConnectionSelect = new ConnectionSelect(false);
                objConnectionSelect.ShowDialog();
                Card.TargetConnection = objConnectionSelect.SelectedConnection;
                lblConnection.Content = Card.TargetConnection.Name.Trim() + " - " + Card.TargetConnection.DataBaseType.ToString();
                objConnectionSelect = null;
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbSaveLog()
        {
            CommonSaveFileDialog objDialog;
            StreamWriter objFile;

            try
            {

                if (ListLog.Items.Count > 0)
                {
                    objDialog = new CommonSaveFileDialog();
                    objDialog.Title = Properties.Resources.ResourceManager.GetString("AppName").ToString();
                    objDialog.DefaultFileName = card.Name.Replace(" ", "_") + ".txt";
                    objDialog.DefaultExtension = "txt";
                    objDialog.Filters.Add(new CommonFileDialogFilter("Log Text", "*.txt"));

                    if (objDialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        objFile = new StreamWriter(objDialog.FileName);
                        foreach (String item in ListLog.Items)
                        {
                            objFile.WriteLine(item);
                        }
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

        private void sbAddLog(SpinerBaseEventArgs p_eventArgs)
        {

            string strMessage;

            try
            {
                strMessage = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                if(p_eventArgs.TotalProgress == 0)
                {
                    strMessage = strMessage + " - " + p_eventArgs.Message;
                }
                else
                {
                    strMessage = strMessage + " - " + ((int)((Double)p_eventArgs.CurrentProgress / p_eventArgs.TotalProgress * 100f)).ToString() + "%";
                    strMessage = strMessage + " - " + p_eventArgs.Message;
                }
                ListLog.Items.Add(strMessage);

            }
            catch (Exception)
            {
             
            }            
        }
        #endregion

        #region Properties
        public Card Card { get => card; set => card = value; }


        #endregion

    }

}
