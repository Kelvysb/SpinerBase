using BControls;
using SpinerBase.Layers.BackEnd;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SpinerBaseBE
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {

            Dictionary<String, String> startupParams;
            List<String> parmsList;
            List<String> commands;
            string strWorkDirectory;

            try
            {

                strWorkDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SpinerBase\\";

                if (Directory.Exists(strWorkDirectory) == false)
                {
                    Directory.CreateDirectory(strWorkDirectory);
                }
              
                if (e.Args.Length > 0)
                {
                    startupParams = new Dictionary<string, string>();
                    parmsList = e.Args.ToList();
                    commands = parmsList.FindAll(command => command.StartsWith("-"));

                    commands.ForEach(command =>
                    {
                        if ((parmsList.IndexOf(command) + 1 <= parmsList.Count() - 1) 
                                && !parmsList[parmsList.IndexOf(command) + 1].StartsWith("-"))
                        {
                            startupParams.Add(command, parmsList[parmsList.IndexOf(command) + 1]);
                        }
                        else
                        {
                            startupParams.Add(command, "");
                        }
                    });
                    SpinerBaseBO.InitiateInstance(strWorkDirectory + "\\SpinerBaseData.json", startupParams);
                }
                else
                {
                    SpinerBaseBO.InitiateInstance(strWorkDirectory + "\\SpinerBaseData.json");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Wrong Parameters");
            }
        }
    }
}
