
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpinerBase.Basic
{
    public enum enmResultType
    {
        Text = 0,
        Grid = 1
    }

    public enum enmDataBaseType
    {
        MsSQL = 0,
        MySQL = 1,
        SQLite = 2,
        Oracle = 3
    }

    public enum enmParameterType
    {
        Text,
        Number,
        Decimal,
        SeparatedText,
        SeparatedNumber,
        DateTime        
    }

    public class CardEventArgs : EventArgs
    {
        public CardEventArgs(Card card)
        {
            this.card = card;
        }

        public Card card { get; set; }
    }

    public class ConnectionEventArgs : EventArgs
    {
        public ConnectionEventArgs(Connection connection)
        {
            this.connection = connection;
        }

        public Connection connection { get; set; }
    }

    public class SpinerBaseConfig
    {

        #region Constructor
        public SpinerBaseConfig()
        {
            User = "";
            Cards = new List<Card>();
            Connections = new List<Connection>();
            LastCard = null;
            LastConnection = null;
        }
        #endregion

        #region Functions

        public SpinerBaseConfig Clone()
        {
            try
            {
                return SpinerBaseConfig.Deserialize(Serialize());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public String Serialize()
        {
            try
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static SpinerBaseConfig Deserialize(String p_strJson)
        {
            try
            {
                return JsonConvert.DeserializeObject<SpinerBaseConfig>(p_strJson);
            }
            catch (Exception)
            {
                throw;
            }
        }
      
        public void Save(String p_path)
        {

            StreamWriter objFile;

            try
            {

                if (File.Exists(p_path))
                {
                    File.Delete(p_path);
                }
                objFile = new StreamWriter(p_path);
                objFile.Write(this.Serialize());
                objFile.Close();
                objFile.Dispose();
                objFile = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static SpinerBaseConfig Load(String p_path)
        {
            SpinerBaseConfig objReturn;
            String strFile;
            StreamReader objFile;

            try
            {
                objReturn = null;

                if (!File.Exists(p_path))
                {
                    new SpinerBaseConfig().Save(p_path);
                }

                objFile = new StreamReader(p_path);
                strFile = objFile.ReadToEnd();
                objFile.Close();
                objFile.Dispose();
                objFile = null;
                objReturn = SpinerBaseConfig.Deserialize(strFile);

                return objReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Properties
        [JsonProperty("USER")]
        public String User { get; set; }

        [JsonProperty("CARDS")]
        public List<Card> Cards { get; set; }

        [JsonProperty("CONNECTIONS")]
        public List<Connection> Connections { get; set; }

        [JsonProperty("LASTCONNECTION")]
        public Connection LastConnection { get; set; }

        [JsonProperty("LASTCARD")]
        public Card LastCard { get; set; }
        #endregion

    }

    public class Card
    {

        #region Constructor
        public Card()
        {
            try
            {
                Name = "";
                Description = "";
                Tags = "";
                Parameters = new List<Parameter>();
                Command = "";
                Result = "";
                ResultType = enmResultType.Text;
                DataBaseType = enmDataBaseType.MsSQL;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Functions
        public Card Clone()
        {
            try
            {
                return Card.Deserialize(Serialize());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public String Serialize()
        {
            try
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Card Deserialize(String p_strJson)
        {
            try
            {
                return JsonConvert.DeserializeObject<Card>(p_strJson);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Save(String p_path)
        {

            StreamWriter objFile;

            try
            {

                if (File.Exists(p_path))
                {
                    File.Delete(p_path);
                }
                objFile = new StreamWriter(p_path);
                objFile.Write(this.Serialize());
                objFile.Close();
                objFile.Dispose();
                objFile = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Card Load(String p_path)
        {
            Card objReturn;
            String strFile;
            StreamReader objFile;

            try
            {
                objReturn = null;

                if (!File.Exists(p_path))
                {
                    throw new FileNotFoundException(p_path);
                }

                objFile = new StreamReader(p_path);
                strFile = objFile.ReadToEnd();
                objFile.Close();
                objFile.Dispose();
                objFile = null;
                objReturn = Card.Deserialize(strFile);

                return objReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Properties
        [JsonProperty("NAME")]
        public String Name { get; set; }

        [JsonProperty("DESCRIPTION")]
        public String Description { get; set; }

        [JsonProperty("TAGS")]
        public String Tags { get; set; }

        [JsonProperty("PARAMETERS")]
        public List<Parameter> Parameters { get; set; }

        [JsonProperty("COMMAND")]
        public String Command { get; set; }

        [JsonProperty("RESULT")]
        public String Result { get; set; }

        [JsonProperty("RESULTTYPE")]
        public enmResultType ResultType { get; set; }

        [JsonProperty("DATABASETYPE")]
        public enmDataBaseType DataBaseType { get; set; }
        #endregion
    }

    public class Parameter
    {
        #region Declarations
        public Parameter()
        {
            try
            {
                Tag = "";
                Description = "";
                Value = "";
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Properties
        [JsonProperty("TAG")]
        public string Tag { get; set; }

        [JsonProperty("DESCRIPTION")]
        public string Description { get; set; }

        [JsonProperty("VALUE")]
        public string Value { get; set; }

        [JsonProperty("")]
        public enmParameterType Type { get; set; }
        #endregion
    }

    public class Connection
    {
        public Connection()
        {
            Name = "";
            Description = "";
            Server = "";
            Instance = "";
            DataBase = "";
            User = "";
            Password = "";
        }
        #region Constructor

        #endregion

        #region Properties
        [JsonProperty("NAME")]
        public String Name { get; set; }

        [JsonProperty("DESCRIPTION")]
        public String Description { get; set; }

        [JsonProperty("SERVER")]
        public String Server { get; set; }

        [JsonProperty("INSTANCE")]
        public String Instance { get; set; }

        [JsonProperty("DATABASE")]
        public String DataBase { get; set; }

        [JsonProperty("USER")]
        public String User { get; set; }

        [JsonProperty("PASSWORD")]
        public String Password { get; set; }

        [JsonProperty("DATABASETYPE")]
        public enmDataBaseType DataBaseType { get; set; }
        #endregion
    }

    public class Migration
    {

        #region Constructor
        public Migration()
        {
            try
            {
                Name = "";
                Description = "";
                Parameters = new List<Parameter>();
                TargetConnection = null;
                UseTempFile = true;
                Command = "";
                Result = "";

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Functions
        public Migration Clone()
        {
            try
            {
                return Migration.Deserialize(Serialize());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public String Serialize()
        {
            try
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Migration Deserialize(String p_strJson)
        {
            try
            {
                return JsonConvert.DeserializeObject<Migration>(p_strJson);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Save(String p_path)
        {

            StreamWriter objFile;

            try
            {

                if (File.Exists(p_path))
                {
                    File.Delete(p_path);
                }
                objFile = new StreamWriter(p_path);
                objFile.Write(this.Serialize());
                objFile.Close();
                objFile.Dispose();
                objFile = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Migration Load(String p_path)
        {
            Migration objReturn;
            String strFile;
            StreamReader objFile;

            try
            {
                objReturn = null;

                if (!File.Exists(p_path))
                {
                    throw new FileNotFoundException(p_path);
                }

                objFile = new StreamReader(p_path);
                strFile = objFile.ReadToEnd();
                objFile.Close();
                objFile.Dispose();
                objFile = null;
                objReturn = Migration.Deserialize(strFile);

                return objReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Card GetCard()
        {
            Card objReturn;

            try
            {
                objReturn = new Card();
                objReturn.Name = Name;
                objReturn.Description = Description;
                objReturn.Parameters = Parameters;
                objReturn.ResultType = enmResultType.Grid;
                objReturn.Command = Command;

                return objReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Properties

        [JsonProperty("NAME")]
        public String Name { get; set; }

        [JsonProperty("DESCRIPTION")]
        public String Description { get; set; }

        [JsonProperty("PARAMETERS")]
        public List<Parameter> Parameters { get; set; }

        [JsonProperty("TARGETCONNECTION")]
        public Connection TargetConnection { get; set; }

        [JsonProperty("USETEMPFILE")]
        public bool UseTempFile { get; set; }

        [JsonProperty("COMMAND")]
        public String Command { get; set; }

        [JsonProperty("RESULT")]
        public String Result { get; set; }

        #endregion

    }

}
