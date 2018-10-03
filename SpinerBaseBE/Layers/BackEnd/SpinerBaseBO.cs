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
using SpinerBase.Basic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace SpinerBase.Layers.BackEnd
{
    public class SpinerBaseBO
    {
        #region Types
        public class SpinerBaseEventArgs : EventArgs
        {
            public SpinerBaseEventArgs(int currentProgress, int totalProgress, string message)
            {
                CurrentProgress = currentProgress;
                TotalProgress = totalProgress;
                Message = message;
            }

            public int CurrentProgress { get; set; }
            public int TotalProgress { get; set; }
            public string Message { get; set; }
        }
        #endregion

        #region Declarations
        private string strBasePath;
        private static SpinerBaseBO instance;
        private SpinerBaseConfig configBase;
        private Repository.SpinerBaseRep objRepository;
        #endregion

        #region Constructor
        private SpinerBaseBO(String p_basePath)
        {
            try
            {
                strBasePath = p_basePath;
                configBase = SpinerBaseConfig.Load(strBasePath);
                objRepository = null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Events
        public event EventHandler<SpinerBaseEventArgs> evProgress;
        protected virtual void onEvProgress(int currentProgress, int totalProgress, string message)
        {
            evProgress?.Invoke(this, new SpinerBaseEventArgs(currentProgress, totalProgress, message));
        }
        public event EventHandler evFinished;
        protected virtual void onEvFinished()
        {
            evFinished?.Invoke(this, new EventArgs());
        }
        #endregion

        #region Functions       
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

        public DataSet fnExecuteCardDataSet(Card p_card)
        {
            try
            {
                if (objRepository != null)
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

        public String fnExecuteCardText(Card p_card)
        {
            try
            {
                if (objRepository != null)
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

            try

            {
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
                foreach (DataRow row in objReturn.Tables[0].Rows)
                {
                    intProcessed++;
                    try
                    {
                        objTargetRepository.sbExecuteDirect(row[0].ToString());
                        onEvProgress(intProcessed, objReturn.Tables[0].Rows.Count, "Processed: " + row[0].ToString());
                    }
                    catch (Exception ex)
                    {
                        onEvProgress(intProcessed, objReturn.Tables[0].Rows.Count, "Error: " + ex.Message);
                    }
                }

                onEvProgress(intProcessed, objReturn.Tables[0].Rows.Count, "Finished.");

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                onEvFinished();
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

                            if (strAuxDescription.Contains("|"))
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
                            else if (strAuxType.Trim().ToUpper() == "SEPARATEDNUMBER")
                            {
                                objReturn.Last().Type = enmParameterType.SeparatedNumber;
                            }
                            else if (strAuxType.Trim().ToUpper() == "DATETIME")
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

        #endregion
    }
}
