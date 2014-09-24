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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#endregion

namespace StartupCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Engine.InitializeEngine();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ActiveControl = btnAddApp;
        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            Engine.ClosingOverride(e);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            ColoringReset();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Engine.Saved)
            {
                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
            }
            else
                saveToolStripMenuItem.Enabled = false;

            if (Engine.CurrentSteps.Count == 0)
            {
                testToolStripMenuItem.Enabled = false;
            }
            else
                testToolStripMenuItem.Enabled = true;

            if (!HelpToolStripMenuItem.Selected && !HelpToolStripMenuItem.DropDown.Visible && HelpToolStripMenuItem.ForeColor == Color.FromArgb(64, 64, 64))
                HelpToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);

            if (!fileToolStripMenuItem.Selected && !fileToolStripMenuItem.DropDown.Visible && fileToolStripMenuItem.ForeColor == Color.FromArgb(64, 64, 64))
                fileToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);
        }

        #region BUTTONS

        private void btnViewSteps_Click(object sender, EventArgs e)
        {
            ColoringReset();

            Engine.FinalizeScriptFile();

            ViewSteps vf = new ViewSteps(this);
            vf.Show();

            Visible = false;
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            ColoringReset();

            NewServiceStep nss = new NewServiceStep(this);
            nss.Show();

            Visible = false;
        }

        private void btnAddApp_Click(object sender, EventArgs e)
        {
            ColoringReset();

            NewStandardStep nas = new NewStandardStep(this);
            nas.Show();

            Visible = false;
        }

        #endregion BUTTONS

        #region MENU STRIP

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);
            Refresh();
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);

            About ab = new About(this);
            ab.Show();

            Visible = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);

            bool windowsStartup = false;

            if (Engine.SaveLocation == Settings.StartupDirectory)
                windowsStartup = true;

            #region INTEGRITY CHECK

            if (File.Exists(Engine.SaveLocation + Engine.FileName))
            {
                DialogResult dR;

                if (windowsStartup)
                    dR = MessageBox.Show("Overwrite existing script in the Windows startup directory?", "File Exists!", MessageBoxButtons.OKCancel);
                else
                    dR = MessageBox.Show("Overwrite existing script " + Engine.SaveLocation + Engine.FileName + " ?", "File Exists!", MessageBoxButtons.OKCancel);

                if (dR == DialogResult.OK)
                {
                    Engine.SaveScriptFile(Engine.SaveLocation, Engine.FileName, false);

                    if (windowsStartup)
                        MessageBox.Show("Script successfully saved to the Windows startup directory.", "Success!", MessageBoxButtons.OK);
                    else
                        MessageBox.Show("Script successfully saved: " + Engine.SaveLocation + Engine.FileName, "Success!", MessageBoxButtons.OK);
                }
            }
            else
            {
                Engine.SaveScriptFile(Engine.SaveLocation, Engine.FileName, false);
                if (windowsStartup)
                    MessageBox.Show("Script successfully saved to the Windows startup directory.", "Success!", MessageBoxButtons.OK);
                else
                    MessageBox.Show("Script successfully saved: " + Engine.SaveLocation + Engine.FileName, "Success!", MessageBoxButtons.OK);
            }

            #endregion INTEGRITY CHECK
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAsToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);

            string[] fullLocation = Util.SaveBrowsePrompt();

            if (fullLocation[0] != "")
            {
                Engine.SaveScriptFile(fullLocation[0], fullLocation[1], false);
                MessageBox.Show("Script successfully saved to " + fullLocation[0] + fullLocation[1], "Success!", MessageBoxButtons.OK);
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.SaveScriptFile(Settings.TemporaryDataDir, Settings.DefaultFileName, true);

            Util.StartProcess(Settings.TemporaryDataDir + Settings.DefaultFileName);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Engine.Saved)
            {
                DialogResult dR = MessageBox.Show("Save changes before opening?", "Save?", MessageBoxButtons.YesNoCancel);

                switch (dR)
                {
                    case DialogResult.Yes:
                    {
                        string[] saveLocation = Util.SaveBrowsePrompt();

                        if (saveLocation[0] != "")
                            Engine.SaveScriptFile(saveLocation[0], saveLocation[1], false);
                        else
                            return;

                        string[] openLocation = Util.OpenBrowsePrompt(true);

                        if (openLocation[0] != "")
                        {
                            Engine.CurrentSteps.Clear();
                            Engine.SaveLocation = openLocation[0];
                            Engine.FileName = openLocation[1];
                            Engine.Saved = true;
                        }
                        break;
                    }
                    case DialogResult.No:
                    {
                        string[] openLocation = Util.OpenBrowsePrompt(true);

                        if (openLocation[0] != "")
                        {
                            Engine.CurrentSteps.Clear();
                            Engine.SaveLocation = openLocation[0];
                            Engine.FileName = openLocation[1];
                            Engine.Saved = true;
                        }
                        break;
                    }
                }
            }
            else
            {
                Engine.CurrentSteps.Clear();
                Engine.Saved = true;
                string[] openLocation = Util.OpenBrowsePrompt(true);

                if (openLocation[0] != "")
                {
                    Engine.SaveLocation = openLocation[0];
                    Engine.FileName = openLocation[1];

                    bool success = Engine.OpenScriptFile();

                    if (!success)
                        Engine.Error("Failed to open script file!");
                }
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Util.StartProcess(Settings.HelpLocation);
        }

        #region COLORING

        private void fileToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            fileToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);
            HelpToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void fileToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            if (!fileToolStripMenuItem.Selected && !fileToolStripMenuItem.DropDown.Visible)
                fileToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void HelpToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            HelpToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);
            fileToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void HelpToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            if (!HelpToolStripMenuItem.Selected && !HelpToolStripMenuItem.DropDown.Visible)
                HelpToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void saveAsToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            if (saveAsToolStripMenuItem.Enabled)
                saveAsToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void saveAsToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            if (saveAsToolStripMenuItem.Enabled)
                saveAsToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void aboutToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            aboutToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void aboutToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            aboutToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void saveToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            if (saveToolStripMenuItem.Enabled)
                saveToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void saveToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            if (saveToolStripMenuItem.Enabled)
                saveToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void exitToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            exitToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void exitToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            exitToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void testToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            testToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void testToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            testToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void openToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            openToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void openToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            openToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void helpToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            helpToolStripMenuItem1.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void helpToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            helpToolStripMenuItem1.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void ColoringReset()
        {
            fileToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);
            HelpToolStripMenuItem.ForeColor = Color.FromArgb(255, 255, 255);
        }

        #endregion COLORING

        #endregion MENU STRIP
    }
}