using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpinerBaseBE.Layers.BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinerBaseBE.Layers.BackEnd.Tests
{
    [TestClass()]
    public class PythonInterperterTests
    {
        [TestMethod()]
        public void ProcessStringTest()
        {
            string strCommand;
            string strreturn;

            try
            {

                strCommand = "def process(n):\r\n";
                strCommand = strCommand + "\treturn n.replace('<%TEST%>', '<%REPLACED%>')";                             
                strreturn = PythonInterperter.ProcessString(strCommand, "Command test = <%TEST%>");

                if(strreturn != "Command test = <%REPLACED%>")
                {
                    Assert.Fail("Wrong return: " + strreturn);
                }

            }
            catch (Exception ex)
            {                
                Assert.Fail(ex.Message);
            }            
        }
    }
}