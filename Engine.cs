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
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using StartupCreator.Enums;

#endregion

namespace StartupCreator
{
    public static class Engine
    {
        public static List<string> FileContents;

        public static List<Step> CurrentSteps;

        public static bool Saved;

        /// <summary> Not including the file name. </summary>
        public static string SaveLocation;

        public static string FileName;

        public static Version Version;

        public static void InitializeEngine()
        {
            Settings.InitializeSettings();
            Version = Util.GetVersion(Settings.ApplicationDirectory + Settings.ApplicationFileName);

            FileContents = new List<string>();
            CurrentSteps = new List<Step>();

            SaveLocation = Settings.StartupDirectory;
            FileName = Settings.DefaultFileName;
            Saved = true;
        }

        public static void AddStandardStep(string input, string parameters, int time)
        {
            Saved = false;
            CurrentSteps.Add(new Step(StepType.Standard, input, input, parameters, time));
        }

        public static void AddServiceStep(string display, string service, int time)
        {
            Saved = false;
            CurrentSteps.Add(new Step(StepType.Service, display, service, "", time));
        }

        public static bool OpenScriptFile()
        {
            List<string> file = Util.ReadListFromFile(SaveLocation + FileName);

            if (file.Count < 3)
                return false;

            if (file[0] != Settings.FirstLine || file[1] != Settings.SecondLine ||
                file[3] != Settings.FourthLine || file[4] != Settings.FifthLine)
                return false;

            int x = 0;

            foreach (string s in file)
            {
                if (x > 5)
                {
                    if (file[x].StartsWith(Settings.StandardLine))
                    {
                        int time = 0;
                        string parameters = "";
                        string displayName = "";

                        try
                        {
                            time = Convert.ToInt32(Regex.Match(file[x + 1], @"\d+").Value);
                        }
                        catch (Exception e)
                        {
                            Error(e.ToString());
                        }

                        string[] broken = Util.SplitString(file[x + 2], ",");

                        string command = broken[1].Substring(12);

                        string[] brokenCmd = Util.SplitString(command, @"""");

                        if (brokenCmd.Length > 0)
                        {
                            displayName = brokenCmd[0];

                            if (brokenCmd.Length > 4)
                                parameters = brokenCmd[4];
                        }

                        Step step = new Step(StepType.Standard, displayName, displayName, parameters, time);
                        CurrentSteps.Add(step);
                    }
                    else if (file[x].StartsWith(Settings.ServiceLine))
                    {
                        int time = 0;
                        string displayName = "";
                        string actionName = "";

                        displayName = file[x].Substring(10);
                        displayName = displayName.Substring(0, displayName.Length - 1);
                        try
                        {
                            time = Convert.ToInt32(Regex.Match(file[x + 1], @"\d+").Value);
                        }
                        catch (Exception e)
                        {
                            Error(e.ToString());
                        }

                        string[] broken = Util.SplitString(file[x + 2], ",");

                        string[] brokenCmd = Util.SplitString(broken[1], @"""");

                        if (brokenCmd.Length > 3)
                            actionName = brokenCmd[3];

                        Step step = new Step(StepType.Service, displayName, actionName, "", time);
                        CurrentSteps.Add(step);
                    }
                }

                x++;
            }

            return true;
        }

        public static void ClosingOverride(CancelEventArgs e)
        {
            if (!Saved)
            {
                DialogResult dR = MessageBox.Show("Save changes before exiting?", "Save?", MessageBoxButtons.YesNoCancel);

                switch (dR)
                {
                    case DialogResult.Yes:
                        e.Cancel = true;

                        string[] fullLocation = Util.SaveBrowsePrompt();

                        if (fullLocation[0] != "")
                            SaveScriptFile(fullLocation[0], fullLocation[1], false);

                        Program.ExitApplication();
                        break;
                    case DialogResult.No:
                        Program.ExitApplication();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
            else
                Program.ExitApplication();
        }

        /// <summary> Builds the script file. </summary>
        public static void FinalizeScriptFile()
        {
            FileContents.Clear();

            FileContents.Add(Settings.FirstLine);
            FileContents.Add(Settings.SecondLine);
            FileContents.Add("");
            FileContents.Add(Settings.FourthLine);
            FileContents.Add(Settings.FifthLine);
            FileContents.Add("");

            foreach (Step s in CurrentSteps)
            {
                switch (s.StepType)
                {
                    case StepType.Service:
                        FileContents.Add(Settings.ServiceLine + " \"" + s.DisplayName + "\"");
                        FileContents.Add(Settings.TimeLine + s.SecondsBeforeExecution + " )");

                        FileContents.Add("objShell.ShellExecute \"cmd\", \"/c NET START \"\"" + s.ActionName + "\"\"\", \"\", \"runas\", 0");
                        FileContents.Add("");
                        break;
                    case StepType.Standard:
                        FileContents.Add(Settings.StandardLine);
                        FileContents.Add(Settings.TimeLine + s.SecondsBeforeExecution + " )");

                        if (s.Parameters != "")
                            FileContents.Add("objShell.ShellExecute \"cmd\", \"/c CALL \"\"" + s.ActionName + "\"\" \"\"" + s.Parameters + "\"\"\", \"\", \"runas\", 0");
                        else
                            FileContents.Add("objShell.ShellExecute \"cmd\", \"/c CALL \"\"" + s.ActionName + "\"\"\", \"\", \"runas\", 0");
                        FileContents.Add("");
                        break;
                }
            }
        }

        public static void Error(string exception)
        {
            MessageBox.Show("Error!: " + exception, "Error!", MessageBoxButtons.OK);
        }

        public static void SaveScriptFile(string location, string fileName, bool temp)
        {
            FinalizeScriptFile();
            Util.WriteListToFile(FileContents, location, fileName);

            if (!temp)
            {
                SaveLocation = location;
                Saved = true;
            }
        }
    }
}