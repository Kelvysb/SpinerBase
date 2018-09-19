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

using BControls;
using Microsoft.WindowsAPICodePack.Dialogs;
using SpinerBase.Basic;
using SpinerBase.Layers.BackEnd;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for SpinerBaseGridResult.xaml
    /// </summary>
    public partial class uscGridResult : UserControl
    {

        #region Declarations
        private List<uscParameter> parametersControls;
        private Card card;
        private DataSet objDataSet;
        private int intTableIndex;
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

        private void btnPlay_Click_1(object sender, RoutedEventArgs e)
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
        #endregion

        #region Constructor
        public uscGridResult(Card p_card)
        {
            try
            {
                intTableIndex = 0;
                InitializeComponent();
                card = p_card;
                grdResult.ItemsSource = null;
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
                onEvBeginWait();
                objDataSet = SpinerBaseBO.Instance.fnExecuteCardDataSet(card);
                if(objDataSet is null == false)
                {
                    intTableIndex = 0;
                    grdResult.ItemsSource = objDataSet.Tables[intTableIndex].DefaultView;
                }
                else
                {
                   grdResult.ItemsSource = null;
                   BMessage.Instance.fnMessage(Properties.Resources.ResourceManager.GetString("msgNoData").ToString(), Properties.Resources.ResourceManager.GetString("AppName").ToString(), MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
            finally
            {
                onEvEndWait();
            }
        }

        private void sbCopyResult()
        {
            try
            {
                if (objDataSet is null == false)
                {
                    Clipboard.SetText(BGlobal.GlobalFunctions.fnDataTableToText(objDataSet.Tables[intTableIndex], ";", true));
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

                if (objDataSet is null == false)
                {
                    objDialog = new CommonSaveFileDialog();
                    objDialog.Title = Properties.Resources.ResourceManager.GetString("AppName").ToString();
                    objDialog.DefaultFileName = card.Name.Replace(" ", "_") + ".csv";
                    objDialog.DefaultExtension = "csv";
                    objDialog.Filters.Add(new CommonFileDialogFilter("Comma-separated values", "*.csv"));

                    if (objDialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        objFile = new StreamWriter(objDialog.FileName);
                        objFile.Write(BGlobal.GlobalFunctions.fnDataTableToText(objDataSet.Tables[intTableIndex], ";", true));
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
