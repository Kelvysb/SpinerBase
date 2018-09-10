using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpinerBaseBE.Basic
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
        SQLite = 2
    }

    public class CardEventArgs : EventArgs
    {
        public CardEventArgs(Card card)
        {
            this.card = card;
        }

        public Card card { get; set; }
    }

    public class SpinerBaseConfig
    {

        #region Constructor
        public SpinerBaseConfig()
        {
            User = "";
            Cards = new List<Card>();
            Connections = new List<Connection>();
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
}
