
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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using SpinerBase.Basic;

namespace SpinerBaseBE.Layers.BackEnd
{
    public class PythonInterpreter
    {

        #region Declarations
        private static ScriptEngine objPyEng = null;
        #endregion

        #region Constructor
        private PythonInterpreter()
        {
        }
        #endregion

        #region Functions
        public static List<Parameter> ProcessParameters(List<Parameter> p_Parameters)
        {

            List<Parameter> objReturn;

            try
            {

                objReturn = new List<Parameter>();

                foreach (Parameter item in p_Parameters)
                {
                    objReturn.Add(new Parameter());
                    objReturn.Last().Tag = item.Tag;
                    objReturn.Last().Description = item.Description;
                    if(item.PythonScript.Trim() != "")
                    {
                        objReturn.Last().Value = ProcessString(item.PythonScript, item.Value);
                    }
                    else
                    {
                        objReturn.Last().Value =  item.Value;
                    }

                }


                return objReturn;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static DataSet ProcessDataSet(string p_PythonCommand, DataSet p_Value)
        {

            DataSet objReturn;
            ScriptSource objSource;
            ScriptScope objScope;
            Func<DataSet, DataSet> objExecute;

            try
            {

                objReturn = p_Value;

                if (!p_PythonCommand.Trim().ToUpper().Contains("DEF MAIN("))
                {
                    throw new Exception("Main method not found.");
                }

                if (!p_PythonCommand.Trim().ToUpper().Contains("IMPORT CLR"))
                {
                    throw new Exception("Must import clr library to use dataset.");
                }

                if (objPyEng is null)
                {
                    objPyEng = Python.CreateEngine();
                }

                objSource = objPyEng.CreateScriptSourceFromString(p_PythonCommand, SourceCodeKind.Statements);
                objScope = objPyEng.CreateScope();

                objSource.Execute(objScope);

                objExecute = objScope.GetVariable<Func<DataSet, DataSet>>("main");

                objReturn = objExecute(p_Value);

                return objReturn;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static string ProcessString(string p_PythonCommand, string  p_Value)
        {

            string strReturn;
            ScriptSource objSource;
            ScriptScope objScope;
            Func<string, string> objExecute;

            try
            {

                strReturn = "";

                if(!p_PythonCommand.Trim().ToUpper().Contains("DEF MAIN("))
                {
                    throw new Exception("Main method not found.");
                }

                if(objPyEng is null)
                {
                    objPyEng = Python.CreateEngine();
                }

                objSource = objPyEng.CreateScriptSourceFromString(p_PythonCommand, SourceCodeKind.Statements);
                objScope = objPyEng.CreateScope();

                objSource.Execute(objScope);

                objExecute = objScope.GetVariable<Func<string, string>>("main");

                strReturn = objExecute(p_Value);

                return strReturn;

            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

    }
}
