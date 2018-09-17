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

namespace SpinerBase.Layers.BackEnd
{
    public class SpinerBaseBO
    {
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
                if(objRepository != null)
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

        public List<Parameter> fnExtractParameters(string p_command)
        {

            List<Parameter> objReturn;
            int intIndex;
            int intFinalIndex;
            int intLastIndex;
            string strAuxTag;

            try
            {

                objReturn = new List<Parameter>();

                intIndex = -1;
                intFinalIndex = -1;
                intLastIndex = 0;
                strAuxTag = "";

                do
                {
                    intIndex = p_command.IndexOf("<%", intLastIndex);

                    if(intIndex != -1)
                    {
                        intFinalIndex = p_command.IndexOf("%>", intIndex);

                        if(intFinalIndex != -1)
                        {

                            strAuxTag = p_command.Substring(intIndex,  intFinalIndex - intIndex + 2);
                            
                            if(!strAuxTag.Contains(" "))
                            {

                                objReturn.Add(new Parameter());
                                objReturn.Last().Tag = strAuxTag;
                                objReturn.Last().Description = strAuxTag.Substring(2, strAuxTag.Length-4);

                            }


                        }

                        intLastIndex = intIndex + 1;
                    }


                } while (intIndex != -1);




                return objReturn;

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

                if(objRepository is null == false)
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
        { get
            {
                return instance;
            }            
        }

        public SpinerBaseConfig ConfigBase { get => configBase; }

        public Connection actualConnection
        {
            get
            {
                if(objRepository != null)
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
