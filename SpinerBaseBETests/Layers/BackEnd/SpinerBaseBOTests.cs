
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpinerBase.Basic;
using SpinerBase.Layers.BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SpinerBase.Layers.BackEnd.Tests
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
                String strTestString = "teste string to Stract <%TAGS%> from This is the next #<%TAG%> and <%ComplexTAG%>";
                List<Parameter> testReturn;
                testReturn = SpinerBaseBO.Instance.fnExtractParameters(strTestString);

                if (testReturn.Count != 3)
                {
                    Assert.Fail("Wrong return count.");
                }

                if (testReturn[0].Tag != "<%TAGS%>" || testReturn[1].Tag != "<%TAG%>" || testReturn[2].Tag != "<%ComplexTAG%>")
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

        [TestMethod()]
        public void sbDoMigrationTest()
        {
            try
            {
                Migration objMigration;
                Connection objConnection;

                SpinerBaseBO.InitiateInstance(Environment.CurrentDirectory + "\\SpinerBaseData.json");

                objConnection = fnGetExampleConection();
                objMigration = fnGetExampleMigration();

                SpinerBaseBO.Instance.fnConnect(objConnection);
                SpinerBaseBO.Instance.sbDoMigration(objMigration);

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

        private Connection fnGetExampleConectionMsSQL()
        {
            Connection objConnection;

            try
            {

                objConnection = new Connection();
                objConnection.Server = "127.0.0.1,1433";
                objConnection.DataBase = "testDatabase";
                objConnection.User = "test";
                objConnection.Password = "test";
                objConnection.DataBaseType = enmDataBaseType.MsSQL;


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
                objCard.Command = "Select * from testTable where age > <%AGE%>;;";
                objCard.Parameters.Add(new Parameter());
                objCard.Parameters.Last().Description = "Age";
                objCard.Parameters.Last().Tag = "<%AGE%>";
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
                objCard.Command = "";
                objCard.Parameters.Add(new Parameter());
                objCard.Parameters.Last().Description = "Age";
                objCard.Parameters.Last().Tag = "<%AGE%>";
                objCard.Parameters.Last().Value = "20";

                return objCard;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private Migration fnGetExampleMigration()
        {
            Migration objMigration;

            try
            {

                objMigration = new Migration();
                objMigration.Name = "Test Card";
                objMigration.Description = "Test Card";
                objMigration.Card.Command = "select \"insert into tbTeste(name, surname, age, address) values('\" || name || \"', '\" || surname || \"', \" || age || \", '\" || address || \"')\" as Result from testTable";
                objMigration.Card.Parameters.Add(new Parameter());
                objMigration.Card.Parameters.Last().Description = "Age";
                objMigration.Card.Parameters.Last().Tag = "<%AGE%>";
                objMigration.Card.Parameters.Last().Value = "20";
                objMigration.TargetConnection = fnGetExampleConectionMsSQL();

                return objMigration;

            }
            catch (Exception)
            {
                throw;
            }
        }

     
    }
}