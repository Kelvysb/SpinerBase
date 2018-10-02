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
using System;
using System.Windows;

namespace SpinerBase.Layers.FrontEnd
{
    /// <summary>
    /// Interaction logic for PythonScript.xaml
    /// </summary>
    public partial class PythonScript : Window
    {

        #region Constructor
        public PythonScript(string p_script, string p_Title)
        {
            try
            {
                InitializeComponent();
                Script = p_script;
                this.Title = Properties.Resources.ResourceManager.GetString("AppName").ToString() + " - " + p_Title;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Events
        private void PythonScript_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sbLoad();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbOk();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }     
        #endregion

        #region Functions
        private void sbLoad()
        {
            try
            {
                txtCommand.Text = Script;
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbOk()
        {
            try
            {

                if (txtCommand.Text.Trim() != "" &&  txtCommand.Text.Trim().ToUpper().Contains("DEF MAIN("))
                {
                    Script = txtCommand.Text;
                    this.Close();
                }
                else if(txtCommand.Text.Trim() != "")
                {
                    BMessage.Instance.fnMessage("Main method not found.", Properties.Resources.ResourceManager.GetString("AppName").ToString(), MessageBoxButton.OK, BMessage.enm_MessageImages.Warning);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }
        #endregion

        #region Parameters
        public string Script { get; set; }

        #endregion

        
    }
}
