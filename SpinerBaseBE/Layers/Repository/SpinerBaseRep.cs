using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpinerBaseBE.Basic;
using BDataBase;
using System.Data;

namespace SpinerBaseBE.Layers.Repository
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
                else
                {
                    dataBaseConfig.Type = DataBase.enmDataBaseType.SqLite;
                    dataBaseConfig.Server = p_connection.Server + "\\" + p_connection.DataBase; 
                }

                dataBaseConfig.DataBase = p_connection.DataBase;
                dataBaseConfig.User = p_connection.User;
                dataBaseConfig.Password = p_connection.Password;

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
                if (exbd.Code != DataBaseException.enmDataBaseExeptionCode.NotExists)
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
                    strReturn = strReturn + row[0].ToString() + (char)13;
                }

            }
            catch (DataBaseException exbd)
            {
                if (exbd.Code != DataBaseException.enmDataBaseExeptionCode.NotExists)
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
