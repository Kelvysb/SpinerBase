using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SpinerBaseBE.Basic;

namespace SpinerBaseBE.Layers.BackEnd
{
    public class SpinerBaseBO
    {
        #region Declarations
        private static SpinerBaseBO instance;
        private SpinerBaseConfig configBase;
        #endregion

        #region Constructor
        private SpinerBaseBO(String p_basePath)
        {
            try
            {
                configBase = SpinerBaseConfig.Load(p_basePath);
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
                    intIndex = p_command.IndexOf("#", intLastIndex);

                    if(intIndex != -1)
                    {
                        intFinalIndex = p_command.IndexOf(";", intIndex);

                        if(intFinalIndex != -1)
                        {

                            strAuxTag = p_command.Substring(intIndex,  intFinalIndex - intIndex + 1);
                            
                            if(!strAuxTag.Contains(" "))
                            {

                                objReturn.Add(new Parameter());
                                objReturn.Last().Tag = strAuxTag;
                                objReturn.Last().Description = strAuxTag.Substring(1, strAuxTag.Length-2);

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
        #endregion

        #region Properties
        public static SpinerBaseBO Instance
        { get
            {
                return instance;
            }            
        }

        public SpinerBaseConfig ConfigBase { get => configBase; }      

        #endregion
    }
}
