﻿/*
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
using SpinerBase.Basic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace SpinerBase.Layers.BackEnd
{
    public class SpinerBaseBO
    {
        #region Types
        public class SpinerBaseMigrateEventArgs : EventArgs
        {
            public SpinerBaseMigrateEventArgs(int currentProgress, int totalProgress, string message)
            {
                CurrentProgress = currentProgress;
                TotalProgress = totalProgress;
                Message = message;
            }

            public int CurrentProgress { get; set; }
            public int TotalProgress { get; set; }
            public string Message { get; set; }
        }
        public class SpinerBaseExecuteEventArgs : EventArgs
        {
            public SpinerBaseExecuteEventArgs(string p_TextReturn, DataSet p_GridReturn, string message)
            {
                TextReturn = p_TextReturn;
                GridReturn = p_GridReturn;
                Message = message;
            }

            public string TextReturn { get; set; }
            public DataSet GridReturn { get; set; }
            public string Message { get; set; }
        }

        public class SpinerBaseFinishEventArgs : EventArgs
        {
            public SpinerBaseFinishEventArgs(string message)
            {
                Error = null;
                Message = message;
            }

            public SpinerBaseFinishEventArgs(string message, Exception p_Error)
            {
                Error = p_Error;
                Message = message;
            }

            public Exception Error { get; set; }
            public string Message { get; set; }
        }
        #endregion

        #region Declarations
        private string strBasePath;
        private static SpinerBaseBO instance;
        private SpinerBaseConfig configBase;
        private Repository.SpinerBaseRep objRepository;
        private Dictionary<String, String> startupParams;
        #endregion

        #region Constructor
        private SpinerBaseBO(String p_basePath)
        {
            try
            {
                strBasePath = p_basePath;
                configBase = SpinerBaseConfig.Load(strBasePath);
                objRepository = null;
                startupParams = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private SpinerBaseBO(String p_basePath, Dictionary<String, String> p_params)
        {
            try
            {
                strBasePath = p_basePath;
                configBase = SpinerBaseConfig.Load(strBasePath);
                objRepository = null;
                startupParams = p_params;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void InitiateInstance(String p_Path)
        {
            try
            {
                instance = new SpinerBaseBO(p_Path);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void InitiateInstance(String p_Path, Dictionary<String, String> p_params)
        {
            try
            {
                instance = new SpinerBaseBO(p_Path, p_params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Events
        public event EventHandler<SpinerBaseExecuteEventArgs> evExecute;
        protected virtual void onEvExecute(string p_TextReturn, DataSet p_GridReturn, string message)
        {
            evExecute?.Invoke(this, new SpinerBaseExecuteEventArgs(p_TextReturn, p_GridReturn, message));
        }
        public event EventHandler<SpinerBaseMigrateEventArgs> evProgress;
        protected virtual void onEvProgress(int currentProgress, int totalProgress, string message)
        {
            evProgress?.Invoke(this, new SpinerBaseMigrateEventArgs(currentProgress, totalProgress, message));
        }
        public event EventHandler<SpinerBaseFinishEventArgs> evFinished;
        protected virtual void onEvFinished(string p_Message)
        {
            evFinished?.Invoke(this, new SpinerBaseFinishEventArgs(p_Message));
        }
        protected virtual void onEvFinished(string p_Message, Exception p_Error)
        {
            evFinished?.Invoke(this, new SpinerBaseFinishEventArgs(p_Message, p_Error));
        }
        #endregion

        #region Functions       
        

        public DataSet fnExecuteCardDataSet(Card p_card)
        {
            Repository.SpinerBaseRep objAuxRepository = null;
            DataSet objResult = null;

            try
            {

                if (p_card.DefaultConnection != null)
                {
                    objAuxRepository = new Repository.SpinerBaseRep(p_card.DefaultConnection);
                }

                if (objAuxRepository != null)
                {
                    objResult = objAuxRepository.fnExecuteDataSet(p_card);
                    objAuxRepository.Dispose();
                    objAuxRepository = null;
                    return objResult;
                }
                else if (objRepository != null)
                {
                    return objRepository.fnExecuteDataSet(p_card);
                }
                else
                {
                    throw new Exception("Not connected");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void sbExecuteCardDataSetAsync(Object p_card)
        {
            DataSet objReturn;
            Repository.SpinerBaseRep objAuxRepository = null;
            try
            {

                if (((Card)p_card).DefaultConnection != null)
                {
                    objAuxRepository = new Repository.SpinerBaseRep(((Card)p_card).DefaultConnection);
                }

                if (objAuxRepository != null)
                {
                    objReturn = objAuxRepository.fnExecuteDataSet((Card)p_card);
                    onEvExecute("", objReturn, "Ok");
                    onEvFinished("Ok");
                    objAuxRepository.Dispose();
                    objAuxRepository = null;
                }
                else if (objRepository != null)
                {
                    objReturn = objRepository.fnExecuteDataSet((Card)p_card);
                    onEvExecute("", objReturn, "Ok");
                    onEvFinished("Ok");
                }
                else
                {
                    onEvFinished("Error", new Exception("Not connected"));
                }
            }
            catch (Exception ex)
            {
                onEvFinished("Error", ex);
            }
        }

        public string fnExecuteCardText(Card p_card)
        {

            Repository.SpinerBaseRep objAuxRepository = null;
            string strResult = "";

            try
            {

                if (p_card.DefaultConnection != null)
                {
                    objAuxRepository = new Repository.SpinerBaseRep(p_card.DefaultConnection);
                }

                if (objAuxRepository != null)
                {
                    strResult = objAuxRepository.fnExecuteText(p_card);
                    objAuxRepository.Dispose();
                    objAuxRepository = null;
                    return strResult;
                }
                else if (objRepository != null)
                {
                    return objRepository.fnExecuteText(p_card);
                }
                else
                {
                    throw new Exception("Not connected");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void sbExecuteCardTextAsync(Object p_card)
        {

            string strReturn;
            Repository.SpinerBaseRep objAuxRepository = null;

            try
            {

                if (((Card)p_card).DefaultConnection != null)
                {
                    objAuxRepository = new Repository.SpinerBaseRep(((Card)p_card).DefaultConnection);
                }

                if (objAuxRepository != null)
                {
                    strReturn = objAuxRepository.fnExecuteText((Card)p_card);
                    onEvExecute(strReturn, null, "Ok");
                    onEvFinished("Ok");
                    objAuxRepository.Dispose();
                    objAuxRepository = null;
                }
                else if (objRepository != null)
                {
                    strReturn = objRepository.fnExecuteText((Card)p_card);
                    onEvExecute(strReturn, null, "Ok");
                    onEvFinished("Ok");

                }
                else
                {
                    onEvFinished("Error", new Exception("Not connected"));                    
                }
            }
            catch (Exception ex)
            {
                onEvFinished("Error", ex);
            }
        }

        public void sbDoMigration(Object p_migration)
        {

            try
            {
                sbDoMigration((Card)p_migration);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void sbDoMigration(Card p_migration)
        {

            Repository.SpinerBaseRep objTargetRepository;
            DataSet objReturn;
            int intProcessed;
            bool blnSucess;
            try

            {

                if (p_migration.Type != enmCardType.Migration)
                {
                    throw new Exception("Wrong card type for migration.");
                }

                if (p_migration.TargetConnection == null)
                {
                    throw new Exception("Missing target connection.");
                }

                intProcessed = 0;

                //Read    
                onEvProgress(0, 0, "Reading source data.");
                objReturn = objRepository.fnExecuteDataSet(p_migration);
                onEvProgress(0, 0, "Data retreived.");

                //Prepare target
                objTargetRepository = new Repository.SpinerBaseRep(p_migration.TargetConnection);
                onEvProgress(0, 0, "Connecting to target.");

                //Write
                onEvProgress(0, objReturn.Tables[0].Rows.Count, "Sending data to target.");
                objTargetRepository.Begin();
                blnSucess = true;
                foreach (DataRow row in objReturn.Tables[0].Rows)
                {
                    intProcessed++;
                    try
                    {
                        objTargetRepository.sbExecuteDirect(row[0].ToString(), false);
                        onEvProgress(intProcessed, objReturn.Tables[0].Rows.Count, "Processed: " + row[0].ToString());
                        Thread.Sleep(10);
                    }
                    catch (Exception ex)
                    {                
                        onEvProgress(intProcessed, objReturn.Tables[0].Rows.Count, "Error: " + ex.Message);
                        Thread.Sleep(10);
                        blnSucess = false;
                        break;
                    }
                }

                if (blnSucess)
                {
                    onEvProgress(intProcessed, objReturn.Tables[0].Rows.Count, "Finished with sucess.");
                    Thread.Sleep(100);
                    objTargetRepository.Commit();
                }
                else
                {
                    onEvProgress(intProcessed, objReturn.Tables[0].Rows.Count, "Finished with error.");
                    Thread.Sleep(100);
                    objTargetRepository.RollBack();
                }
               
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Thread.Sleep(100);
                onEvFinished("Ok");
            }

        }

        public List<Parameter> fnExtractParameters(string p_command)
        {

            List<Parameter> objReturn;
            string strAuxTag;
            string strAuxDescription;
            string strAuxType;
            MatchCollection objMatchs;

            try
            {

                objReturn = new List<Parameter>();
              
                strAuxTag = "";
                strAuxDescription = "";
                strAuxType = "";

                objMatchs = Regex.Matches(p_command, "(<%).*?(%>)");

                foreach (Match match in objMatchs)
                {
                    if (match.Success)
                    {
                        strAuxTag = p_command.Substring(match.Index, match.Length);


                        if (!strAuxTag.Contains(" ") && objReturn.FindIndex(item => item.Tag == strAuxTag) == -1)
                        {

                            objReturn.Add(new Parameter());

                            strAuxDescription = strAuxTag.Substring(2, strAuxTag.Length - 4);

                            if (strAuxDescription.Contains("@"))
                            {
                                strAuxType = strAuxDescription.Split('@')[1].Trim();
                                strAuxDescription = strAuxDescription.Split('@')[0].Trim();
                            }
                            else if (strAuxDescription.Contains("|"))
                            {
                                strAuxType = strAuxDescription.Split('|')[1].Trim();
                                strAuxDescription = strAuxDescription.Split('|')[0].Trim();
                            }
                            else
                            {
                                strAuxType = "TEXT";
                            }

                            if (strAuxType.Trim().ToUpper() == "NUMBER")
                            {
                                objReturn.Last().Type = enmParameterType.Number;
                            }                 
                            else if (strAuxType.Trim().ToUpper() == "DATETIME" || strAuxType.Trim().ToUpper() == "DATE")
                            {
                                objReturn.Last().Type = enmParameterType.DateTime;
                            }
                            else
                            {
                                objReturn.Last().Type = enmParameterType.Text;
                            }

                            objReturn.Last().Tag = strAuxTag;
                            objReturn.Last().Description = strAuxDescription;

                        }

                    }
                }


                return objReturn;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RecursiveParameter> fnExtractRecursiveParameters(string p_command)
        {

            List<RecursiveParameter> objReturn;
            string strAuxTag;
            string strField;
            string strTable;
            MatchCollection objMatchs;
            try
            {

                objReturn = new List<RecursiveParameter>();

                strAuxTag = "";
                strField = "";
                strTable = "";

                objMatchs = Regex.Matches(p_command, "(<!).*?(!>)");

                foreach (Match match in objMatchs)
                {
                    if (match.Success)
                    {
                        strAuxTag = p_command.Substring(match.Index, match.Length);
                        if (!strAuxTag.Contains(" ") && objReturn.FindIndex(item => item.Tag == strAuxTag) == -1)
                        {

                            strField = strAuxTag.Substring(2, strAuxTag.Length - 4);

                            if (strField.Contains("@"))
                            {
                                strTable = strField.Split('@')[1].Trim();
                                strField = strField.Split('@')[0].Trim();
                            }

                            objReturn.Add(new RecursiveParameter(strAuxTag, strField, strTable));

                        }
                    }
                }

                return objReturn;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string fnGetTableName(string strCommand)
        {

            string strReturn;
            string strTag;
            Match objMatch;

            try
            {

                strReturn = "";
                objMatch = Regex.Match(strCommand, @"(<!TABLENAME@).*?(!>)");

                if (objMatch.Success)
                {
                    strTag = strCommand.Substring(objMatch.Index, objMatch.Length);
                    strTag = strTag.Replace("<!", "").Replace("!>", "");
                    strReturn = strTag.Split('@')[1];
                }

                return strReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string fnRemoveSpecialTags(string strCommand)
        {

            string strReturn;
            try
            {
                strReturn = strCommand;
                strReturn = Regex.Replace(strReturn, @"(<!TABLENAME@).*?(!>)", "");
                return strReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void fnConnect(Connection p_connection)
        {
            try
            {

                if (objRepository is null == false)
                {
                    objRepository.Dispose();
                    objRepository = null;
                }

                objRepository = new Repository.SpinerBaseRep(p_connection);

                ConfigBase.LastConnection = p_connection;


            }
            catch (Exception)
            {
                throw;
            }
        }

        public void sbSaveConfig()
        {

            try
            {
                configBase.Save(strBasePath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string sbExecuteReport(Card p_objReport)
        {
            string strResult = "";
            Dictionary<string, string> objCardResults;
            List<RecursiveParameter> objCardNamesParameters;

            try
            {

                //Check input
                if (p_objReport.Type != enmCardType.Report)
                {
                    throw new Exception("Wrong card type for report");
                }

                if (p_objReport.Report == null || p_objReport.Report.Body.Equals(""))
                {
                    throw new Exception("Missing report body.");
                }

                //Execution of sub-Cards
                objCardResults = new Dictionary<string, string>();
                p_objReport.Report.Cards.ForEach(card =>
                {
                    if(card.DefaultConnection == null)
                    {
                        card.DefaultConnection = p_objReport.DefaultConnection;
                    }
                    string strCardResult = fnExecuteCardText(card);
                    objCardResults.Add("<!" + card.Name.Replace(" ", "_") + "!>", strCardResult);
                });

                //Get card names parameters 
                objCardNamesParameters = fnExtractRecursiveParameters(p_objReport.Report.Body);


                //Replace card names paremeters 
                strResult = p_objReport.Report.Body;
                objCardNamesParameters.ForEach(card =>
                {
                    string value = "";
                    objCardResults.TryGetValue(card.Tag, out value);
                    strResult = strResult.Replace(card.Tag, value);                    
                });

                //Replace general parameters
                p_objReport.Parameters.ForEach(parameter =>
                {
                    strResult = strResult.Replace(parameter.Tag, parameter.Value);
                });
                
                return strResult;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Thread.Sleep(100);
                onEvFinished("Ok");
            }
        }
        #endregion

        #region Properties
        public static SpinerBaseBO Instance
        {
            get
            {
                return instance;
            }
        }

        public SpinerBaseConfig ConfigBase { get => configBase; }

        public Connection actualConnection
        {
            get
            {
                if (objRepository != null)
                {
                    return objRepository.Connection;
                }
                else
                {
                    return null;
                }

            }
        }

        public Dictionary<string, string> StartupParams { get => startupParams; set => startupParams = value; }

        #endregion
    }
}
