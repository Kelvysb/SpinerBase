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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BControls;
using Microsoft.WindowsAPICodePack.Dialogs;
using SpinerBase.Basic;
using SpinerBase.Layers.BackEnd;

namespace SpinerBase.Layers.FrontEnd
{
    /// <summary>
    /// Interaction logic for SpinerBaseMain.xaml
    /// </summary>
    public partial class SpinerBaseMain : Window
    {

        #region Declarations
        private List<uscCard> objCards;
        private uscConnectionConfig objConnectionConfig;
        private uscGridResult objCardGrid;
        private uscTextResult objCardText;
        private uscCardConfig objCardEditor;
        #endregion

        #region Constructor
        public SpinerBaseMain()
        {
            try
            {
                InitializeComponent();
                BMessage.sbInitialize((Brush)FindResource("BaseColor"),
                                        (Brush)FindResource("BackColor"),
                                        (Brush)FindResource("FontColor"),
                                        (Brush)FindResource("FontColor"),
                                        (FontFamily)FindResource("Font"),
                                        Environment.CurrentDirectory + "\\Log");

                SpinerBaseBO.InitiateInstance(Environment.CurrentDirectory + "\\SpinerBaseData.json");

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbAddCard(new Card());
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbImportCard();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sbFilter();
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

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbAbout();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void evRemoveConfigFromResults(object sender, EventArgs e)
        {
            try
            {
                sbclearResultsGrid();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void evRemoveGridCardFromResults(object sender, EventArgs e)
        {
            try
            {
                sbclearResultsGrid();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void evRemoveTextCardFromResults(object sender, EventArgs e)
        {
            try
            {
                sbclearResultsGrid();
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void evSelectCard(object sender, CardEventArgs e)
        {
            try
            {
                sbSelectCard(e.card);
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void evEditCard(object sender, CardEventArgs e)
        {
            try
            {
                sbEditCard(e.card);
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void evCardSaved(object sender, CardEventArgs e)
        {
            try
            {
                sbclearResultsGrid();
                sbUpdateCard(e.card);
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void evRemoveCard(object sender, CardEventArgs e)
        {
            try
            {
                sbRemoveCard(e.card);
            }
            catch (Exception ex)
            {
                BMessage.Instance.fnErrorMessage(ex);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

                wrpCards.Children.Clear();

                if(SpinerBaseBO.Instance.actualConnection is null == false)
                {
                    SpinerBaseBO.Instance.fnConnect(SpinerBaseBO.Instance.actualConnection);
                    lblConnection.Content = SpinerBaseBO.Instance.actualConnection.Name.Trim() + " - " + SpinerBaseBO.Instance.actualConnection.DataBaseType.ToString();
                }


                objCards = new List<uscCard>();

                objCards = (from Card card in SpinerBaseBO.Instance.ConfigBase.Cards
                            select new uscCard(card)).ToList();

                foreach (uscCard card in objCards)
                {
                    card.Margin = new Thickness(2);
                    card.evEdit += evEditCard;
                    card.evPlay += evSelectCard;
                    card.evRemove += evRemoveCard;
                    wrpCards.Children.Add(card);
                }

                if(SpinerBaseBO.Instance.ConfigBase.LastConnection is null == false)
                {
                    SpinerBaseBO.Instance.fnConnect(SpinerBaseBO.Instance.ConfigBase.LastConnection);
                    lblConnection.Content = SpinerBaseBO.Instance.actualConnection.Name.Trim() + " - " + SpinerBaseBO.Instance.actualConnection.DataBaseType.ToString();
                }

                if (SpinerBaseBO.Instance.ConfigBase.LastCard is null == false)
                {
                    sbSelectCard(SpinerBaseBO.Instance.ConfigBase.LastCard);
                }

                sbFilter();

            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbUpdateCard(Card p_card)
        {

            uscCard objauxCard;

            try
            {

                objauxCard = objCards.Find(card => card.Card.Equals(p_card));
                objauxCard.Update();
                SpinerBaseBO.Instance.sbSaveConfig();
                sbFilter();

            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbAddCard(Card p_objCard)
        {
            try
            {

                SpinerBaseBO.Instance.ConfigBase.Cards.Add(p_objCard);

                objCards.Add(new uscCard(SpinerBaseBO.Instance.ConfigBase.Cards.Last()));
                objCards.Last().Margin = new Thickness(2);
                objCards.Last().evEdit += evEditCard;
                objCards.Last().evPlay += evSelectCard;
                objCards.Last().evRemove += evRemoveCard;
                wrpCards.Children.Add(objCards.Last());
                sbEditCard(SpinerBaseBO.Instance.ConfigBase.Cards.Last());
                //BMessage.Instance.fnMessage(Properties.Resources.ResourceManager.GetString("msgAdded").ToString(), Properties.Resources.ResourceManager.GetString("AppName").ToString(), MessageBoxButton.OK);
                sbFilter();
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbImportCard()
        {
            CommonOpenFileDialog objDialog;

            try
            {

                objDialog = new CommonOpenFileDialog();
                objDialog.Title = Properties.Resources.ResourceManager.GetString("AppName").ToString();
                objDialog.DefaultExtension = "sbc";
                objDialog.Filters.Add(new CommonFileDialogFilter("SpineBase Card", "*.sbc"));

                if (objDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    sbAddCard(Card.Load(objDialog.FileName));                                       
                }

                objDialog = null;

            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbEditCard(Card p_card)
        {
            try
            {

                sbclearResultsGrid();

                objCardEditor = new uscCardConfig(p_card);
                objCardEditor.evSaved += evCardSaved;
                grdResults.Children.Add(objCardEditor);

            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbSelectCard(Card p_card)
        {
            try
            {

                sbclearResultsGrid();
                
                if(p_card.ResultType == enmResultType.Text)
                {
                    objCardText = new uscTextResult(p_card);
                    objCardText.evRemove += evRemoveTextCardFromResults;
                    grdResults.Children.Add(objCardText);
                }
                else
                {
                    objCardGrid = new uscGridResult(p_card);
                    objCardGrid.evRemove += evRemoveGridCardFromResults;
                    grdResults.Children.Add(objCardGrid);
                }

                SpinerBaseBO.Instance.ConfigBase.LastCard = p_card;
                
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }

        }

        private void sbFilter()
        {

            List<uscCard> objFilteredResults;

            try
            {

                objFilteredResults = new List<uscCard>();

                foreach (uscCard card in objCards)
                {
                    card.Visibility = Visibility.Visible;
                }

                //Remove diferent databases cards, if it's connected
                if (SpinerBaseBO.Instance.actualConnection is null == false)
                {
                    objFilteredResults.AddRange(objCards.FindAll(card => card.Card.DataBaseType != SpinerBaseBO.Instance.actualConnection.DataBaseType));
                }

                //Use Filter
                if(txtFilter.Text.Trim() != "")
                {
                    objFilteredResults.AddRange(objCards.FindAll(card => !card.Card.Name.ToUpper().Contains(txtFilter.Text.Trim().ToUpper())));
                }

                foreach (uscCard card in objFilteredResults)
                {
                    card.Visibility = Visibility.Collapsed;
                }


            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbRemoveCard(Card p_card)
        {

            uscCard objauxCard;

            try
            {
                if(BMessage.Instance.fnMessage(Properties.Resources.ResourceManager.GetString("msgRemoveCard").ToString(), Properties.Resources.ResourceManager.GetString("AppName").ToString(), MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    objauxCard = objCards.Find(card => card.Card.Equals(p_card));
                    if(objauxCard is null == false)
                    {
                        objauxCard.evEdit -= evEditCard;
                        objauxCard.evPlay -= evSelectCard;
                        objauxCard.evRemove -= evRemoveCard;
                        wrpCards.Children.Remove(objauxCard);
                        objCards.Remove(objauxCard);
                    }               

                    SpinerBaseBO.Instance.ConfigBase.Cards.Remove(p_card);
                    SpinerBaseBO.Instance.sbSaveConfig();
                    sbclearResultsGrid();
                    BMessage.Instance.fnMessage(Properties.Resources.ResourceManager.GetString("msgRemoved").ToString(), Properties.Resources.ResourceManager.GetString("AppName").ToString(), MessageBoxButton.OK);                    

                }               
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbclearResultsGrid()
        {
            try
            {

                grdResults.Children.Clear();

                if (objCardText is null == false)
                {
                    objCardText.evRemove -= evRemoveTextCardFromResults;
                    objCardText = null;
                }

                if (objCardGrid is null == false)
                {
                    objCardGrid.evRemove -= evRemoveGridCardFromResults;
                    objCardGrid = null;
                }

                if (objConnectionConfig is null == false)
                {
                    objConnectionConfig.evClose -= evRemoveConfigFromResults;
                    objConnectionConfig = null;
                }

                if (objCardEditor is null == false)
                {
                    objCardEditor.evSaved -= evCardSaved;
                    objCardEditor = null;
                }


                if (SpinerBaseBO.Instance.actualConnection is null == false)
                {
                    SpinerBaseBO.Instance.fnConnect(SpinerBaseBO.Instance.actualConnection);
                    lblConnection.Content = SpinerBaseBO.Instance.actualConnection.Name.Trim() + " - " + SpinerBaseBO.Instance.actualConnection.DataBaseType.ToString();
                }

                sbFilter();
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbConnect()
        {
            try
            {

                grdResults.Children.Clear();
                objConnectionConfig = new uscConnectionConfig();
                objConnectionConfig.evClose += evRemoveConfigFromResults;
                grdResults.Children.Add(objConnectionConfig);

            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }
   
        private void sbAbout()
        {
            About objAbout;
            try
            {
                objAbout = new About();
                objAbout.ShowDialog();
                objAbout = null;
            }
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.ResourceManager.GetString("msgError").ToString(), ex);
            }
        }

        private void sbClose()
        {
            try
            {
                SpinerBaseBO.Instance.sbSaveConfig();
            }
            catch (Exception)
            {
                
            }
        }
        #endregion

        #region Properties

        #endregion

        
    }
}
