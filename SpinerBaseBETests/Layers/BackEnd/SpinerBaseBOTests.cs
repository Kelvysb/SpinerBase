using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpinerBaseBE.Basic;
using SpinerBaseBE.Layers.BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

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

        [TestMethod()]
        public void fnExecuteCardTextTest()
        {

            try
            {
                Card objCard;
                Connection objConnection;
                String strReturn = "";

                SpinerBaseBO.InitiateInstance(Environment.CurrentDirectory + "\\SpinerBaseData.json");

                objConnection = fnGetExampleConection();
                objCard = fnGetExampleCardForText();


                SpinerBaseBO.Instance.fnConnect(objConnection);
                strReturn = SpinerBaseBO.Instance.fnExecuteCardText(objCard);

                if (strReturn == "")
                {
                    Assert.Fail("No result");
                }

            }
            catch (Exception ex)
            {
                Assert.Fail("Error: " + ex.Message);
            }


        }

        [TestMethod()]
        public void fnExecuteCardDataSetTest()
        {
            try
            {
                Card objCard;
                Connection objConnection;
                DataSet objReturn = null;

                SpinerBaseBO.InitiateInstance(Environment.CurrentDirectory + "\\SpinerBaseData.json");

                objConnection = fnGetExampleConection();
                objCard = fnGetExampleCardForDataSet();

                SpinerBaseBO.Instance.fnConnect(objConnection);
                objReturn = SpinerBaseBO.Instance.fnExecuteCardDataSet(objCard);

                if (objReturn is null)
                {
                    Assert.Fail("No result");
                }

            }
            catch (Exception ex)
            {
                Assert.Fail("Error: " + ex.Message);
            }

        }

        private Connection fnGetExampleConection()
        {
            Connection objConnection;

            try
            {

                objConnection = new Connection();
                objConnection.Server = Environment.CurrentDirectory + "\\Resources";
                objConnection.DataBase = "exampledatabase.db";
                objConnection.User = "test";
                objConnection.Password = "test";
                objConnection.DataBaseType = enmDataBaseType.SQLite;



                return objConnection;

            }
            catch (Exception)
            {
                throw;
            }
        }



        private Card fnGetExampleCardForDataSet()
        {
            Card objCard;

            try
            {

                objCard = new Card();
                objCard.DataBaseType = enmDataBaseType.SQLite;
                objCard.Name = "Test Card";
                objCard.Description = "Test Card";
                objCard.Command = "Select * from testTable where age > #AGE;;";
                objCard.Parameters.Add(new Parameter());
                objCard.Parameters.Last().Description = "Age";
                objCard.Parameters.Last().Tag = "#AGE;";
                objCard.Parameters.Last().Value = "20";

                return objCard;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private Card fnGetExampleCardForText()
        {
            Card objCard;

            try
            {

                objCard = new Card();
                objCard.DataBaseType = enmDataBaseType.SQLite;
                objCard.Name = "Test Card";
                objCard.Description = "Test Card";
                objCard.Command = "Select Name || ' ' || Surname || ' Died with ' || Age || ' years.' from testTable where Age > #AGE;;";
                objCard.Parameters.Add(new Parameter());
                objCard.Parameters.Last().Description = "Age";
                objCard.Parameters.Last().Tag = "#AGE;";
                objCard.Parameters.Last().Value = "20";

                return objCard;

            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}