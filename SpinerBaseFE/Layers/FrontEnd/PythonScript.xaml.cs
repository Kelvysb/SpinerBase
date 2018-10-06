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
using SpinerBaseBE.Layers.BackEnd;
using System;
using System.Threading;
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

        private void btnInitialize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbInitialize();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }
        
        private void btnClr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbClr();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void btnDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbDate();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void btnAux_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbAux();
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
                Topmost = true;
                Thread.Sleep(100);
                Topmost = false;
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbOk()
        {
            string strValidation;
            try
            {

                Script = txtCommand.Text;
                if (Script.Trim() != "")
                {
                    strValidation = PythonInterpreter.ValidatePython(Script);
                    if(strValidation.Trim() == "")
                    {
                        this.Close();
                    }
                    else
                    {
                        BMessage.Instance.fnMessage(strValidation, Properties.Resources.ResourceManager.GetString("AppName").ToString(), MessageBoxButton.OK, BMessage.enm_MessageImages.Critical); ;
                    }
                }
                else
                {
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbInitialize()
        {
            try
            {

                if(txtCommand.Text.Trim() != "")
                {
                    txtCommand.Text = txtCommand.Text + "\r\n";
                }
                txtCommand.Text = txtCommand.Text + "def main(input):\r\n\treturn input\r\n";

            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }
        private void sbClr()
        {
            try
            {
                if(!txtCommand.Text.StartsWith("import clr"))
                {
                    txtCommand.Text = txtCommand.Text.Insert(0, "import clr\r\nclr.AddReference('System')\r\nclr.AddReference('System.Data')\r\nfrom System import Data\r\nfrom System.Data import DataSet\r\nfrom System.Data import DataTable\r\nfrom System.Data import DataColumn\r\nfrom System.Data import DataRow\r\n");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbDate()
        {
            try
            {
                txtCommand.Text = txtCommand.Text + "\r\nfrom System import DateTime\r\ndef isDate(input):\r\n\ttry:\r\n\t\ttestDate = DateTime.Parse(input)\r\n\t\treturn True\r\n\texcept:\r\n\t\treturn False\r\n\r\ndef formatDate(input, format):\r\n\ttestDate = DateTime.Parse(input)\r\n\tstrReturn = testDate.ToString(format)\r\n\treturn strReturn\r\n";
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbAux()
        {
            try
            {
                txtCommand.Text = txtCommand.Text + "\r\ndef isString(input):\r\n\tif str(input) == \"System.Int64\" or str(input) == \"System.Int32\" or str(input) == \"System.Int16\" or str(input) == \"System.Double\" or str(input) == \"System.Float\" or str(input) == \"System.Decimal\":\r\n\t\treturn False\r\n\telse:\r\n\t\treturn True\r\n";
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
