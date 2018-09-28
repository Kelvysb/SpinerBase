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
using System.Text;
using System.Threading.Tasks;
using SpinerBase.Basic;
using BDataBase;
using System.Data;

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
                else
                {
                    dataBaseConfig.Type = DataBase.enmDataBaseType.Oracle;
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
            String strCommand;

            try
            {

                objReturn = null;

                strCommand = p_card.Command;

                foreach (Parameter parameter in p_card.Parameters)
                {
                    strCommand = strCommand.Replace(parameter.Tag, parameter.Value);
                }

                objReturn = objDataBase.fnExecute(strCommand);

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
            DataSet objAuxReturn;
            String strCommand;

            try
            {
                strReturn = "";
                objAuxReturn = null;

                strCommand = p_card.Command;

                foreach (Parameter parameter in p_card.Parameters)
                {
                    strCommand = strCommand.Replace(parameter.Tag, parameter.Value);
                }

                objAuxReturn = objDataBase.fnExecute(strCommand);
                
                foreach (DataRow row in objAuxReturn.Tables[0].Rows)
                {
                    foreach (DataColumn column in objAuxReturn.Tables[0].Columns)
                    {
                        strReturn = strReturn + row[column.ColumnName].ToString() + (char)9;
                    }
                    strReturn = strReturn + (char)13 + (char)10;
                }

            }
            catch (DataBaseException exbd)
            {
                if(exbd.Code != DataBaseException.enmDataBaseExeptionCode.NotExists && !exbd.Message.Trim().ToUpper().Contains("NOT EXISTS"))
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
                objDataBase.sbBegin();
                objDataBase.sbExecute(p_command);
                objDataBase.sbCommit();
            }
            catch (DataBaseException exbd)
            {              
                try
                {
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
