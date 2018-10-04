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
    /// Interaction logic for uscParameter.xaml
    /// </summary>
    public partial class uscParameter : UserControl
    {

        #region Declarations

        #endregion

        #region Events
        private void txtParameter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sbUpdateValue();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dtpParameter_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                sbUpdateValue();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtParameterNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
        #endregion

        #region Constructor
        public uscParameter(Parameter p_parameter)
        {
            try
            {
                InitializeComponent();
                parameter = p_parameter;

                if (parameter.Type == enmParameterType.DateTime)
                {
                    txtParameterNumber.Visibility = Visibility.Collapsed;
                    txtParameter.Visibility = Visibility.Collapsed;
                    dtpParameter.Visibility = Visibility.Visible;
                }
                else if (parameter.Type == enmParameterType.Number)
                {
                    txtParameterNumber.Visibility = Visibility.Visible;
                    txtParameter.Visibility = Visibility.Collapsed;
                    dtpParameter.Visibility = Visibility.Collapsed;
                }
                else
                {
                    txtParameterNumber.Visibility = Visibility.Collapsed;
                    txtParameter.Visibility = Visibility.Visible;
                    dtpParameter.Visibility = Visibility.Collapsed;
                }

                Update();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Functions
        public void Update()
        {

            DateTime objAuxDate;

            try
            {
                lblParameter.Content = parameter.Description;

                if (parameter.Type == enmParameterType.DateTime)
                {
                    if(DateTime.TryParse(parameter.Value, out objAuxDate))
                    {
                        dtpParameter.SelectedDate = objAuxDate;
                    }
                    else{
                        objAuxDate = DateTime.Now;
                        parameter.Value = objAuxDate.ToString();
                        dtpParameter.SelectedDate = objAuxDate;
                    }
                }
                else if (parameter.Type == enmParameterType.Number)
                {
                    txtParameterNumber.Text = parameter.Value;
                }
                else
                {
                    txtParameter.Text = parameter.Value;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void sbUpdateValue()
        {
            try
            {
                if (parameter.Type == enmParameterType.DateTime)
                {
                    parameter.Value = dtpParameter.SelectedDate.ToString();
                }
                else if (parameter.Type == enmParameterType.Number)
                {
                    parameter.Value = txtParameterNumber.Text;
                }
                else
                {
                    parameter.Value = txtParameter.Text;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Properties
        public Parameter parameter { get; set; }

        #endregion

        
    }
}
