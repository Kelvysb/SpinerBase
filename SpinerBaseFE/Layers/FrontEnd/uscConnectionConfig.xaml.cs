/*
Copyright 2018 Kelvys B. Pantaleão

This file is part of SpinerBase

SpinerBase Is free software: you can redistribute it And/Or modify
it under the terms Of the GNU General Public License As published by
the Free Software Foundation, either version 3 Of the License, Or
(at your option) any later version.

This program Is distributed In the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty Of
MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License For more details.

You should have received a copy Of the GNU General Public License
along with this program.  If Not, see <http://www.gnu.org/licenses/>.

Este arquivo é parte Do programa SpinerBase

SpinerBase é um software livre; você pode redistribuí-lo e/ou 
modificá-lo dentro dos termos da Licença Pública Geral GNU como 
publicada pela Fundação Do Software Livre (FSF); na versão 3 da 
Licença, ou(a seu critério) qualquer versão posterior.

Este programa é distribuído na esperança de que possa ser  útil, 
mas SEM NENHUMA GARANTIA; sem uma garantia implícita de ADEQUAÇÃO
a qualquer MERCADO ou APLICAÇÃO EM PARTICULAR. Veja a
Licença Pública Geral GNU para maiores detalhes.

Você deve ter recebido uma cópia da Licença Pública Geral GNU junto
com este programa, Se não, veja <http://www.gnu.org/licenses/>.

'GitHub: https://github.com/Kelvysb/SpinerBase
*/

using SpinerBase.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BControls;
using SpinerBase.Layers.BackEnd;

namespace SpinerBase.Layers.FrontEnd
{
    /// <summary>
    /// Interaction logic for usConnectionConfig.xaml
    /// </summary>
    public partial class uscConnectionConfig : UserControl
    {

        #region Declarations
        private Connection objSelectedConnection;
        private List<uscConnection> objConnections;        
        #endregion

        #region Constructor
        public uscConnectionConfig()
        {
            try
            {
                InitializeComponent();
                objConnections = new List<uscConnection>();
                objSelectedConnection = null;
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

        public event EventHandler evClose;
        protected virtual void onEvClose()
        {
            evClose?.Invoke(this, new EventArgs());
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
                    SpinerBaseBO.Instance.fnConnect(objSelectedConnection);
                    BMessage.Instance.fnMessage(Properties.Resources.ResourceManager.GetString("msgConnected").ToString(), Properties.Resources.ResourceManager.GetString("AppName").ToString(), MessageBoxButton.OK);
                    onEvClose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError") + ex.Message, ex);
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

        #endregion

        #region Properties

        #endregion

    }
}
