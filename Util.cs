#region LICENSE

/*
Startup Creator - A simple application to effortlessly create Windows startup scripts.
Copyright (C) 2014 Soenneker LLC

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see <http://www.gnu.org/licenses/>.
*/

#endregion LICENSE

#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

#endregion

namespace StartupCreator
{
    public static class Util
    {
        /// <summary> Starts a process in a SAFE (try/catch) way. </summary>
        public static void StartProcess(string process)
        {
            Process proc = null;

            try
            {
                proc = Process.Start(process);
            }
            catch (Exception e)
            {
                Engine.Error(e.ToString());
            }
            finally
            {
                if (proc != null)
                    proc.Dispose();
            }
        }

        public static string[] SaveBrowsePrompt()
        {
            string[] rtnFile = new string[2];
            rtnFile[0] = "";

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.InitialDirectory = Settings.StartupDirectory;
            sfd.Filter = "Script files (*.vbs) | *.vbs;";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    rtnFile[0] = Path.GetDirectoryName(sfd.FileName) + @"\";
                    rtnFile[1] = Path.GetFileName(sfd.FileName);
                }
                catch (Exception e)
                {
                    Engine.Error(e.ToString());
                }
            }

            return rtnFile;
        }

        public static string GenerateID()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static string[] OpenBrowsePrompt(bool startup)
        {
            string[] rtnFile = new string[2];
            rtnFile[0] = "";

            OpenFileDialog ofd = new OpenFileDialog();

            if (startup)
            {
                ofd.InitialDirectory = Settings.ScriptOpenDialogDirectory;
                ofd.Filter = "Script files (*.vbs) | *.vbs;";
            }
            else
                ofd.InitialDirectory = Settings.StandardOpenDialogDirectory;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    rtnFile[0] = Path.GetDirectoryName(ofd.FileName) + @"\";
                    rtnFile[1] = Path.GetFileName(ofd.FileName);

                    if (startup)
                        Settings.ScriptOpenDialogDirectory = rtnFile[0];
                    else
                        Settings.StandardOpenDialogDirectory = rtnFile[0];
                }
                catch (Exception e)
                {
                    Engine.Error(e.ToString());
                }
            }

            return rtnFile;
        }

        public static List<Service> GetServices()
        {
            ServiceController[] services;
            services = ServiceController.GetServices();

            List<Service> lstServices = new List<Service>();

            foreach (ServiceController sc in services)
            {
                lstServices.Add(new Service(sc.DisplayName, sc.ServiceName, sc.Status.ToString()));
            }

            lstServices.Sort();

            return lstServices;
        }

        /// <summary> Prints a message to System.Diagnostics.Debug IF the program is in Debug mode. </summary>
        /// <param name="msg">The string to print.</param>
        [Conditional("DEBUG")]
        public static void Debug(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }

        /// <summary> Builds a file from a generic list. </summary>
        /// <param name="list"></param>
        public static void WriteListToFile(List<string> list, string location, string fileName)
        {
            StringBuilder sb = new StringBuilder();
            StreamWriter outfile = null;

            foreach (string s in list)
            {
                try
                {
                    sb.Append(s);
                    sb.AppendLine();
                }
                catch (Exception e)
                {
                    Engine.Error(e.ToString());
                }
            }

            try
            {
                outfile = new StreamWriter(location + fileName, false);

                outfile.Write(sb.ToString());
                outfile.Flush();
                outfile.Close();
            }
            catch (Exception e)
            {
                Engine.Error(e.ToString());
            }
            finally
            {
                if (outfile != null)
                    outfile.Dispose();
            }
        }

        /// <summary>
        /// Reads a specified file and returns a list of each line.
        /// Returns an empty list if it does not contain any lines, or an error occured.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static List<string> ReadListFromFile(string filename)
        {
            List<string> temp = new List<string>();

            try
            {
                string[] lines = File.ReadAllLines(filename);
                foreach (string s in lines)
                {
                    temp.Add(s);
                }
            }
            catch (Exception e)
            {
                Engine.Error(e.ToString());
            }

            return temp;
        }

        /// <summary>
        /// Retrieves the version of a executable via a file name. Local files UNACCEPTABLE (Full location only).
        /// Exception safe, and will give 0.0 if it fails.
        /// </summary>
        /// <param name="fullFileName"></param>
        /// <returns></returns>
        public static Version GetVersion(string fullFileName)
        {
            Version version = new Version();

            try
            {
                if (File.Exists(fullFileName))
                    version = AssemblyName.GetAssemblyName(fullFileName).Version;
                else
                    Engine.Error("Could not retrieve version! File does not exist!: " + fullFileName);
            }
            catch (Exception e)
            {
                Engine.Error("Could not load assembly version! Exception: " + e);
            }

            return version;
        }

        public static string[] SplitString(string str, string seperator)
        {
            try
            {
                return str.Split(new[] {Convert.ToChar(seperator)});
            }
            catch (Exception e)
            {
                Engine.Error(e.ToString());
                return new string[0];
            }
        }
    }
}