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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SpinerBase.Layers.FrontEnd
{
    /// <summary>
    /// Interaction logic for SpinerBaseCardConfig.xaml
    /// </summary>
    public partial class uscCardConfig : UserControl
    {

        #region Declarations
        private List<uscParameterConfig> parametersControls;
        private Card objCard;
        private bool blnLoaded = false;
        #endregion

        #region Events

        public event EventHandler<CardEventArgs> evSaved;
        protected virtual void onEvSaved()
        {
            evSaved?.Invoke(this, new CardEventArgs(objCard));
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

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbExport();
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

        private void radQuery_Checked(object sender, RoutedEventArgs e)
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

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
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

        private void txtDescription_TextChanged(object sender, TextChangedEventArgs e)
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

        private void btnPrePy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbPrePy();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }
        
        private void btnPosPy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbPosPy();
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
                objCard = p_card;
                parametersControls = new List<uscParameterConfig>();
                sbLoadParameters();
                sbSetFields();
                blnLoaded = true;
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
            List<Parameter> objAuxRemoveParameters;
            int intauxIndex;

            try
            {
                sbSetFields();

                objAuxParameters = SpinerBaseBO.Instance.fnExtractParameters(objCard.Command);
            
                objAuxRemoveParameters = new List<Parameter>();
                foreach (Parameter parameter in objCard.Parameters)
                {
                    if (objAuxParameters.Find(par => par.Tag.Trim() == parameter.Tag.Trim()) is null)
                    {
                        objAuxRemoveParameters.Add(parameter);
                    }
                }

                foreach (Parameter parameter in objCard.Parameters)
                {
                    intauxIndex = objAuxParameters.RemoveAll(par => par.Tag.Trim() == parameter.Tag.Trim());
                }

                if (objAuxParameters.Count > 0)
                {
                    objCard.Parameters.AddRange(objAuxParameters);
                }
               
                foreach (Parameter parameter in objAuxRemoveParameters)
                {
                    objCard.Parameters.Remove(parameter);                  
                }

                sbLoadParameters();

            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbSetFields()
        {
            try
            {

                txtCommand.Text = objCard.Command;
                txtName.Text = objCard.Name;
                txtDescription.Text = objCard.Description;

                if (objCard.DataBaseType == enmDataBaseType.MsSQL)
                {
                    radMsSql.IsChecked = true;
                }
                else if (objCard.DataBaseType == enmDataBaseType.MySQL)
                {
                    radMySql.IsChecked = true;
                }
                else if (objCard.DataBaseType == enmDataBaseType.SQLite)
                {
                    radSqlite.IsChecked = true;
                }
                else
                {
                    radOracle.IsChecked = true;
                }


                if (objCard.ResultType == enmResultType.Grid)
                {
                    radGrid.IsChecked = true;
                }
                else
                {
                    radText.IsChecked = true;
                }


                if (objCard.Type == enmCardType.Query)
                {
                    radQuery.IsChecked = true;
                }
                else
                {
                    radMigrate.IsChecked = true;
                }


            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbUpdateFileds()
        {
            try
            {
                if (objCard is null == false && blnLoaded)
                {

                    objCard.Command = txtCommand.Text;
                    objCard.Name = txtName.Text;
                    objCard.Description = txtDescription.Text;

                    if ((bool)radMsSql.IsChecked)
                    {
                        objCard.DataBaseType = enmDataBaseType.MsSQL;
                    }
                    else if ((bool)radMySql.IsChecked)
                    {
                        objCard.DataBaseType = enmDataBaseType.MySQL;
                    }
                    else if((bool)radSqlite.IsChecked)
                    {
                        objCard.DataBaseType = enmDataBaseType.SQLite;
                    }
                    else
                    {
                        objCard.DataBaseType = enmDataBaseType.Oracle;
                    }

                    if ((bool)radGrid.IsChecked)
                    {
                        objCard.ResultType = enmResultType.Grid;
                    }
                    else
                    {
                        objCard.ResultType = enmResultType.Text;
                    }

                    if ((bool)radQuery.IsChecked)
                    {
                        objCard.Type = enmCardType.Query;
                    }
                    else
                    {
                        objCard.Type = enmCardType.Migration;
                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
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
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbExport()
        {
            CommonSaveFileDialog objDialog;

            try
            {

                sbUpdateFileds();
                objDialog = new CommonSaveFileDialog();
                objDialog.Title = Properties.Resources.ResourceManager.GetString("AppName").ToString();
                objDialog.DefaultFileName = objCard.Name.Replace(" ", "_") + ".sbc";
                objDialog.DefaultExtension = "sbc";
                objDialog.Filters.Add(new CommonFileDialogFilter("SpineBase Card", "*.sbc"));

                if (objDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    objCard.Save(objDialog.FileName);
                    BMessage.Instance.fnMessage(Properties.Resources.ResourceManager.GetString("msgSaved").ToString(), Properties.Resources.ResourceManager.GetString("AppName").ToString(), MessageBoxButton.OK);
                }

                objDialog = null;

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

                foreach (Parameter parameter in objCard.Parameters)
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

        private void sbPrePy()
        {
            PythonScript objPyWindow;

            try
            {
                objPyWindow = new PythonScript(Card.PrePythonScript, "Card Pre Python Script: " + Card.Name);
                objPyWindow.ShowDialog();
                Card.PrePythonScript = objPyWindow.Script;
                objPyWindow = null;
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }
        private void sbPosPy()
        {
            PythonScript objPyWindow;

            try
            {
                objPyWindow = new PythonScript(Card.PosPythonScript, "Card Pos Python Script: " + Card.Name);
                objPyWindow.ShowDialog();
                Card.PosPythonScript = objPyWindow.Script;
                objPyWindow = null;
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }
        #endregion

        #region Properties
        public Card Card { get => objCard; set => objCard = value; }

        #endregion

        
    }
}
