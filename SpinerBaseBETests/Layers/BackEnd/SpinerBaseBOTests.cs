using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpinerBaseBE.Basic;
using SpinerBaseBE.Layers.BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinerBaseBE.Layers.BackEnd.Tests
{
    [TestClass()]
    public class SpinerBaseBOTests
    {

        [TestMethod()]
        public void InitiateInstanceTest()
        {
            SpinerBaseBO.InitiateInstance(Environment.CurrentDirectory + "\\SpinerBaseData.json");
            if (SpinerBaseBO.Instance == null)
            {
                Assert.Fail("Instance Fail.");
            }
        }

        [TestMethod()]
        public void fnExtractParametersTest()
        {

            try
            {
                //Basic Test
                SpinerBaseBO.InitiateInstance(Environment.CurrentDirectory + "\\SpinerBaseData.json");
                String strTestString = "teste string to Stract #TAGS; from This is the next #TAG; and #ComplexTAG;";
                List<Parameter> testReturn;
                testReturn = SpinerBaseBO.Instance.fnExtractParameters(strTestString);

                if (testReturn.Count != 3)
                {
                    Assert.Fail("Wrong return count.");
                }

                if (testReturn[0].Tag != "#TAGS;" || testReturn[1].Tag != "#TAG;" || testReturn[2].Tag != "#ComplexTAG;")
                {
                    Assert.Fail("Wrong return values.");
                }

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }

        }
       
    }
}