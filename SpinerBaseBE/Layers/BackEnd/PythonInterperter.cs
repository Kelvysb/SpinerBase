using System;
using System.Collections.Generic;
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
    public class PythonInterperter
    {

        #region Declarations
        private static ScriptEngine objPyEng = null;
        #endregion

        #region Constructor
        private PythonInterperter()
        {
        }
        #endregion

        #region Functions
        public static List<Parameter> ProcessParameters(string p_PythonCommand, List<Parameter> p_Parameters)
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
                    objReturn.Last().Value = ProcessString(p_PythonCommand, item.Value);
                }


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

                if(objPyEng is null)
                {
                    objPyEng = Python.CreateEngine();
                }

                objSource = objPyEng.CreateScriptSourceFromString(p_PythonCommand, SourceCodeKind.Statements);
                objScope = objPyEng.CreateScope();

                objSource.Execute(objScope);

                objExecute = objScope.GetVariable<Func<string, string>>("process");

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
