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
using System.IO;
using System.Reflection;

#endregion

namespace StartupCreator
{
    public static class Settings
    {
        #region MEMBERS

        public static string WebsiteLocation = "http://soenneker.com";
        public static string HelpLocation = "https://startupcreator.codeplex.com";

        public static string TemporaryDataDir;
        public static string StartupDirectory;
        public static string ProgramFilesDirectory;
        public static string StandardOpenDialogDirectory;
        public static string ScriptOpenDialogDirectory;

        /// <summary> A solid and sure location of where the executable really is. </summary>
        public static string ApplicationDirectory;

        public static string ApplicationFileName;
        public static string DefaultFileName = "Startup.vbs";

        public static string FirstLine = "'Created by Startup Creator - Soenneker LLC";
        public static string SecondLine = "'" + HelpLocation;
        public static string FourthLine = "Dim objShell";
        public static string FifthLine = "Set objShell = CreateObject( \"Shell.Application\" )";

        public static string ServiceLine = "'Service";
        public static string StandardLine = "'Standard";

        public static string TimeLine = "WScript.Sleep( ";

        #endregion MEMBERS

        public static void InitializeSettings()
        {
            try
            {
                TemporaryDataDir = Path.GetTempPath() + @"StartupCreator\";

                if (!Directory.Exists(TemporaryDataDir))
                    Directory.CreateDirectory(TemporaryDataDir);
                else
                {
                    if (File.Exists(TemporaryDataDir + DefaultFileName))
                        File.Delete(TemporaryDataDir + DefaultFileName);
                }
            }
            catch (Exception e)
            {
                Engine.Error(e.ToString());
            }

            try
            {
                StartupDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\";
            }
            catch (Exception e)
            {
                Engine.Error(e.ToString());
            }

            try
            {
                ApplicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
            }
            catch (Exception e)
            {
                Engine.Error(e.ToString());
            }

            try
            {
                ProgramFilesDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\";
            }
            catch (Exception e)
            {
                Engine.Error(e.ToString());
            }

            StandardOpenDialogDirectory = ProgramFilesDirectory;
            ScriptOpenDialogDirectory = StartupDirectory;

            ApplicationFileName = Assembly.GetEntryAssembly().GetName().Name + ".exe";
        }
    }
}