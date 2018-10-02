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
using System.Windows.Shapes;

namespace SpinerBase.Layers.FrontEnd
{
    /// <summary>
    /// Interaction logic for ConnectionSelect.xaml
    /// </summary>
    public partial class ConnectionSelect : Window
    {

        #region Declarations
        private Connection objSelectedConnection;
        private List<uscConnection> objConnections;
        private bool blnDirectConnect;

        #endregion

        #region Constructor
        public ConnectionSelect(bool p_blnDirectConnect)
        {
            try
            {
                InitializeComponent();
                objConnections = new List<uscConnection>();
                objSelectedConnection = null;
                blnDirectConnect = p_blnDirectConnect;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        #region Events
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
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

        private void conSelect(object sender, ConnectionEventArgs e)
        {
            try
            {
                sbSelectConnection(e.connection);
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void conRemove(object sender, ConnectionEventArgs e)
        {
            try
            {
                sbRemoveConnection(e.connection);
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbClose();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        #endregion

        #region Functions
        private void sbLoadPage()
        {
            try
            {

                cmbType.Items.Add("MsSql");
                cmbType.Items.Add("MySql");
                cmbType.Items.Add("Sqlite");
                cmbType.Items.Add("Oracle");
                cmbType.SelectedIndex = 0;

                foreach (Connection connection in SpinerBaseBO.Instance.ConfigBase.Connections)
                {
                    objConnections.Add(new uscConnection(connection));
                    objConnections.Last().Margin = new Thickness(5);
                    objConnections.Last().evSelect += conSelect;
                    objConnections.Last().evRemove += conRemove;
                    wrpConnections.Children.Add(objConnections.Last());
                }

                if (SpinerBaseBO.Instance.actualConnection is null == false)
                {
                    sbSelectConnection(SpinerBaseBO.Instance.actualConnection);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError") + ex.Message, ex);
            }
        }

        private void sbConnect()
        {
            try
            {
                if (objSelectedConnection is null == false)
                {

                    if (blnDirectConnect)
                    {
                        grdLoading.Visibility = Visibility.Visible;
                        SpinerBaseBO.Instance.fnConnect(objSelectedConnection);
                        BMessage.Instance.fnMessage(Properties.Resources.ResourceManager.GetString("msgConnected").ToString(), Properties.Resources.ResourceManager.GetString("AppName").ToString(), MessageBoxButton.OK);
                        grdLoading.Visibility = Visibility.Collapsed;
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError") + ex.Message, ex);
            }
            finally
            {
                grdLoading.Visibility = Visibility.Collapsed;
            }
        }

        private void sbSave()
        {

            uscConnection objAuxConnection;

            try
            {


                if (objSelectedConnection is null || txtName.Text != objSelectedConnection.Name)
                {

                    objSelectedConnection = new Connection();
                    objSelectedConnection.Name = txtName.Text;
                    objSelectedConnection.Description = txtDescription.Text;
                    objSelectedConnection.Server = txtServer.Text;
                    objSelectedConnection.DataBase = txtDatabase.Text;
                    objSelectedConnection.User = txtUser.Text;
                    objSelectedConnection.Password = txtPassword.Text;
                    objSelectedConnection.DataBaseType = (enmDataBaseType)cmbType.SelectedIndex;

                    SpinerBaseBO.Instance.ConfigBase.Connections.Add(objSelectedConnection);
                    SpinerBaseBO.Instance.sbSaveConfig();

                    objConnections.Add(new uscConnection(objSelectedConnection));
                    objConnections.Last().Margin = new Thickness(5);
                    objConnections.Last().evSelect += conSelect;
                    objConnections.Last().evRemove += conRemove;
                    wrpConnections.Children.Add(objConnections.Last());

                }
                else
                {
                    objSelectedConnection.Description = txtDescription.Text;
                    objSelectedConnection.Server = txtServer.Text;
                    objSelectedConnection.DataBase = txtDatabase.Text;
                    objSelectedConnection.User = txtUser.Text;
                    objSelectedConnection.Password = txtPassword.Text;
                    objSelectedConnection.DataBaseType = (enmDataBaseType)cmbType.SelectedIndex;

                    objAuxConnection = objConnections.Find(conn => conn.Connection.Name.Trim().ToUpper() == objSelectedConnection.Name.Trim().ToUpper());
                    objAuxConnection.Update();

                }

                BMessage.Instance.fnMessage(Properties.Resources.ResourceManager.GetString("msgSaved").ToString(), Properties.Resources.ResourceManager.GetString("AppName").ToString(), MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError") + ex.Message, ex);
            }
        }

        private void sbSelectConnection(Connection p_connection)
        {
            try
            {
                objSelectedConnection = p_connection;
                txtName.Text = objSelectedConnection.Name;
                txtDescription.Text = objSelectedConnection.Description;
                txtServer.Text = objSelectedConnection.Server;
                txtDatabase.Text = objSelectedConnection.DataBase;
                txtUser.Text = objSelectedConnection.User;
                txtPassword.Text = objSelectedConnection.Password;
                cmbType.SelectedIndex = (int)objSelectedConnection.DataBaseType;

            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError") + ex.Message, ex);
            }
        }

        private void sbRemoveConnection(Connection p_connection)
        {

            uscConnection objAuxConnection;

            try
            {


                if (BMessage.Instance.fnMessage(Properties.Resources.ResourceManager.GetString("msgRemoveConnection").ToString(), Properties.Resources.ResourceManager.GetString("AppName").ToString(), MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    objAuxConnection = objConnections.Find(conn => conn.Connection.Equals(p_connection));
                    objAuxConnection.evSelect -= conSelect;
                    objAuxConnection.evRemove -= conRemove;
                    wrpConnections.Children.Remove(objAuxConnection);

                    SpinerBaseBO.Instance.ConfigBase.Connections.Remove(p_connection);
                    SpinerBaseBO.Instance.sbSaveConfig();

                    objSelectedConnection = null;

                    txtName.Text = "";
                    txtDescription.Text = "";
                    txtServer.Text = "";
                    txtDatabase.Text = "";
                    txtUser.Text = "";
                    txtPassword.Text = "";
                    cmbType.SelectedIndex = 0;

                    BMessage.Instance.fnMessage(Properties.Resources.ResourceManager.GetString("msgRemoved").ToString(), Properties.Resources.ResourceManager.GetString("AppName").ToString(), MessageBoxButton.OK);

                }



            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError") + ex.Message, ex);
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
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError") + ex.Message, ex);
            }
        }
        #endregion

        #region Properties
        public Connection SelectedConnection { get => objSelectedConnection; set => objSelectedConnection = value; }
        #endregion

    }
}
