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
using BDataBase;
using System.Data;
using SpinerBaseBE.Layers.BackEnd;
using SpinerBase.Layers.BackEnd;
using System.Text.RegularExpressions;


namespace SpinerBase.Layers.Repository
{
    internal class SpinerBaseRep : IDisposable
    {

        #region Declarations
        private IDataBase objDataBase;
        private Connection connection;
        #endregion

        #region Constructor
        public SpinerBaseRep(Connection p_connection)
        {

            clsConfiguration dataBaseConfig;

            try
            {
                this.connection = p_connection;

                dataBaseConfig = new clsConfiguration();
                if (p_connection.Instance.Trim() != "")
                {
                    dataBaseConfig.Server = p_connection.Server + "/" + p_connection.Instance;
                }
                else
                {
                    dataBaseConfig.Server = p_connection.Server;
                }

                if (p_connection.DataBaseType == enmDataBaseType.MsSQL)
                {
                    dataBaseConfig.Type = DataBase.enmDataBaseType.MsSql;
                }
                else if (p_connection.DataBaseType == enmDataBaseType.MySQL)
                {
                    dataBaseConfig.Type = DataBase.enmDataBaseType.MySql;
                }
                else if (p_connection.DataBaseType == enmDataBaseType.SQLite)
                {
                    dataBaseConfig.Type = DataBase.enmDataBaseType.SqLite;
                    dataBaseConfig.Server = p_connection.Server + "\\" + p_connection.DataBase;
                }
                else if (p_connection.DataBaseType == enmDataBaseType.Oracle)
                {                    
                    dataBaseConfig.Type = DataBase.enmDataBaseType.Oracle;
                }
                else
                {
                    dataBaseConfig.Type = DataBase.enmDataBaseType.Postgre;
                }

                dataBaseConfig.DataBase = p_connection.DataBase;
                dataBaseConfig.User = p_connection.User;
                dataBaseConfig.Password = p_connection.Password;
                dataBaseConfig.ConnetionTimeout = 5;

                objDataBase = DataBase.fnOpenConnection(dataBaseConfig);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Functions

        public DataSet fnExecuteDataSet(Card p_card)
        {

            DataSet objReturn;
            DataSet objAuxReturn;
            String strCommand;
            List<Parameter> objAuxParameter;
            List<string> strStatements;
            int intTableIndex;
            string strTableName;

            try
            {

                objReturn = null;

                strCommand = p_card.Command;

                if (strCommand.Trim() != "")
                {

                    //Execute pre python script
                    if (p_card.PrePythonScript.Trim() != "")
                    {
                        strCommand = PythonInterpreter.ProcessString(p_card.PrePythonScript, strCommand);
                    }

                    //Execute python script by parameter
                    objAuxParameter = PythonInterpreter.ProcessParameters(p_card.Parameters);

                    foreach (Parameter parameter in objAuxParameter)
                    {
                        strCommand = strCommand.Replace(parameter.Tag, parameter.Value);
                    }

                    //Separete statements
                    strStatements = strCommand.Split(new string[] { "<!STATEMENT!>" }, StringSplitOptions.None).ToList();

                    objReturn = new DataSet();
                    intTableIndex = 0;
                    foreach (string statement in strStatements)
                    {
                        strCommand = statement;
                        strTableName = SpinerBaseBO.Instance.fnGetTableName(strCommand);
                        strCommand = SpinerBaseBO.Instance.fnRemoveSpecialTags(strCommand);
                        strCommand = fnRecursiveReplace(strCommand, objReturn);
                        objAuxReturn = objDataBase.fnExecute(strCommand);
                        foreach (DataTable table in objAuxReturn.Tables)
                        {
                            if (strTableName.Trim() != "")
                            {
                                table.TableName = strTableName;
                            }
                            else
                            {
                                table.TableName = "table_" + intTableIndex.ToString();
                            }
                            objReturn.Tables.Add(table.Copy());
                            intTableIndex++;
                        }

                    }

                    //Execute pos python script
                    if (p_card.PosPythonScript.Trim() != "")
                    {
                        objReturn = PythonInterpreter.ProcessDataSet(p_card.PosPythonScript, objReturn);
                    }
                }
                else
                {
                    throw new Exception("Empty command");
                }
            }
            catch (DataBaseException exbd)
            {
                if (exbd.Code != DataBaseException.enmDataBaseExeptionCode.NotExists && !exbd.Message.Trim().ToUpper().Contains("NOT EXISTS"))
                {
                    throw new Exception(exbd.Message, exbd);
                }
                else
                {
                    objReturn = null;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return objReturn;

        }

        public String fnExecuteText(Card p_card)
        {

            String strReturn;
            DataSet objReturn;
            DataSet objAuxReturn;
            String strCommand;
            List<Parameter> objAuxParameter;
            List<string> strStatements;
            int intTableIndex;
            string strTableName;

            try
            {
                strReturn = "";
                objAuxReturn = null;

                strCommand = p_card.Command;
                if (strCommand.Trim() != "")
                {
                    //Execute pre python script
                    if (p_card.PrePythonScript.Trim() != "")
                    {
                        strCommand = PythonInterpreter.ProcessString(p_card.PrePythonScript, strCommand);
                    }

                    //Execute python script by parameter
                    objAuxParameter = PythonInterpreter.ProcessParameters(p_card.Parameters);

                    foreach (Parameter parameter in objAuxParameter)
                    {
                        strCommand = strCommand.Replace(parameter.Tag, parameter.Value);
                    }

                    //Separete statements
                    strStatements = strCommand.Split(new string[] { "<!STATEMENT!>" }, StringSplitOptions.None).ToList();

                    objReturn = new DataSet();
                    intTableIndex = 0;
                    foreach (string statement in strStatements)
                    {
                        strCommand = statement;
                        strTableName = SpinerBaseBO.Instance.fnGetTableName(strCommand);
                        strCommand = SpinerBaseBO.Instance.fnRemoveSpecialTags(strCommand);
                        strCommand = fnRecursiveReplace(strCommand, objReturn);
                        objAuxReturn = objDataBase.fnExecute(strCommand);
                        foreach (DataTable table in objAuxReturn.Tables)
                        {
                            if (strTableName.Trim() != "")
                            {
                                table.TableName = strTableName;
                            }
                            else
                            {
                                table.TableName = "table_" + intTableIndex.ToString();
                            }
                            objReturn.Tables.Add(table.Copy());
                            intTableIndex++;
                        }
                    }

                    foreach (DataTable table in objReturn.Tables)
                    {
                        if (objReturn.Tables.Count > 1)
                        {
                            strReturn = strReturn + table.TableName + ":" + (char)13 + (char)10;
                        }
                        foreach (DataRow row in table.Rows)
                        {
                            foreach (DataColumn column in table.Columns)
                            {
                                strReturn = strReturn + row[column.ColumnName].ToString() + (char)9;
                            }
                            strReturn = strReturn + (char)13 + (char)10;
                        }
                    }

                    //Execute pos python script
                    if (p_card.PosPythonScript.Trim() != "")
                    {
                        strReturn = PythonInterpreter.ProcessString(p_card.PosPythonScript, strReturn);
                    }
                }
                else
                {
                    throw new Exception("Empty command");
                }

            }
            catch (DataBaseException exbd)
            {
                if (exbd.Code != DataBaseException.enmDataBaseExeptionCode.NotExists && !exbd.Message.Trim().ToUpper().Contains("NOT EXISTS"))
                {
                    throw new Exception(exbd.Message, exbd);
                }
                else
                {
                    strReturn = "";
                }
            }
            catch (Exception)
            {

                throw;
            }

            return strReturn;

        }

        public void sbExecuteDirect(string p_command)
        {
            try
            {
                sbExecuteDirect(p_command, true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void sbExecuteDirect(string p_command, bool blnAutoCommit)
        {
            try
            {
                if (blnAutoCommit)                
                    objDataBase.sbBegin();
                
                objDataBase.sbExecute(p_command);

                if (blnAutoCommit)
                    objDataBase.sbCommit();
            }
            catch (DataBaseException exbd)
            {
                try
                {
                    if (blnAutoCommit)
                        objDataBase.sbRollBack();
                }
                catch (Exception)
                {

                }
                throw new Exception(exbd.Message, exbd);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public  void Begin()
        {
            try
            {
                objDataBase.sbBegin();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Commit()
        {
            try
            {
                objDataBase.sbCommit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RollBack()
        {
            try
            {
                objDataBase.sbRollBack();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string fnRecursiveReplace(string statement, DataSet objReturn)
        {

            string strReturn;
            string strAuxField;
            int intAuxTableIndex;
            int intAuxFieldIndex;
            string strAuxTableName = "";
            string strAuxFieldName = "";
            List<RecursiveParameter> objParameters;

            try
            {

                strReturn = statement;

                objParameters = SpinerBaseBO.Instance.fnExtractRecursiveParameters(statement);

                foreach (RecursiveParameter parameter in objParameters)
                {

                    strAuxTableName = parameter.Table;
                    strAuxFieldName = parameter.Field;

                    if (int.TryParse(strAuxTableName, out intAuxTableIndex))
                    {
                        if (int.TryParse(strAuxFieldName, out intAuxFieldIndex))
                        {
                            strAuxField = objReturn.Tables[intAuxTableIndex].Rows[0][intAuxFieldIndex].ToString();
                        }
                        else
                        {
                            strAuxField = objReturn.Tables[intAuxTableIndex].Rows[0][strAuxFieldName].ToString();
                        }
                    }
                    else
                    {
                        if (int.TryParse(strAuxFieldName, out intAuxFieldIndex))
                        {
                            strAuxField = objReturn.Tables[strAuxTableName].Rows[0][intAuxFieldIndex].ToString();
                        }
                        else
                        {
                            strAuxField = objReturn.Tables[strAuxTableName].Rows[0][strAuxFieldName].ToString();
                        }
                    }

                    strReturn = strReturn.Replace(parameter.Tag, strAuxField);
                }

                return strReturn;

            }
            catch (Exception)
            {
                if(strAuxFieldName.Trim() != "" || strAuxTableName.Trim() != "")
                {
                    throw new Exception("Field '" + strAuxFieldName + "' not found on table '" + strAuxTableName + "' (Recursive parameters).");
                }
                else
                {
                    throw;
                }
            }
        }

        public bool isOpen()
        {
            bool blnReturn;

            try
            {
                blnReturn = false;

                if (objDataBase != null)
                {
                    blnReturn = objDataBase.isOpen;
                }

                return blnReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                connection = null;
                if (objDataBase != null)
                {
                    objDataBase.sbClose();
                }
                objDataBase = null;
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
