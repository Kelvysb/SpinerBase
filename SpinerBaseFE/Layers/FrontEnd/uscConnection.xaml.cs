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

namespace SpinerBase.Layers.FrontEnd
{
    /// <summary>
    /// Interaction logic for uscConnection.xaml
    /// </summary>
    public partial class uscConnection : UserControl
    {
        #region Declarations
        private Connection connection;
        #endregion

        #region Constructor        
        public uscConnection()
        {
            try
            {
                InitializeComponent();
                connection = new Connection();
                sbLoad();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public uscConnection(Connection p_connection)
        {
            try
            {
                InitializeComponent();
                connection = p_connection;
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
            evPlay?.Invoke(this, new ConnectionEventArgs(connection));
        }

        public event EventHandler evEdit;
        protected virtual void onEvEdit()
        {
            evEdit?.Invoke(this, new ConnectionEventArgs(connection));
        }

        public event EventHandler evRemove;
        protected virtual void onEvRemove()
        {
            evRemove?.Invoke(this, new ConnectionEventArgs(connection));
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
                lblName.Content = connection.Name;
                lblDescription.Text = connection.Description;
                lblServer.Content = connection.Server;
                lblInstance.Content = connection.Instance;
                lblDatabase.Content = connection.DataBase;
                lblUser.Content = connection.User;
                lblPassword.Content = connection.Password;

                if(connection.DataBaseType == enmDataBaseType.MsSQL)
                {
                    lblType.Content = "MsSql";
                }
                else if(connection.DataBaseType == enmDataBaseType.MySQL)
                {
                    lblType.Content = "MySql";
                }
                else
                {
                    lblType.Content = "SQLite";
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Properties
        public Connection Connection { get => connection; set => connection = value; }
        #endregion
    }
}
