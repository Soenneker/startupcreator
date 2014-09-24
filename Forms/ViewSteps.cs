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
using System.Windows.Forms;

#endregion

namespace StartupCreator
{
    public partial class ViewSteps : ChildForm
    {
        public ViewSteps(Form form)
            : base(form)
        {
            InitializeComponent();
        }

        private void ViewSteps_Load(object sender, EventArgs e)
        {
            RefreshList();

            btnViewFile.Focus();

            lstViewSteps.Columns[0].Width = 200;
            lstViewSteps.Columns[1].Width = 65;
            lstViewSteps.Columns[2].Width = 67;
        }

        private void lstViewSteps_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (Engine.CurrentSteps.Count > 1)
            {
                if (lstViewSteps.SelectedIndices.Count == 1)
                {
                    foreach (int a in lstViewSteps.SelectedIndices)
                    {
                        if (a > 0)
                            btnUp.Visible = true;
                        else
                            btnUp.Visible = false;
                    }

                    foreach (int a in lstViewSteps.SelectedIndices)
                    {
                        if (a < Engine.CurrentSteps.Count - 1)
                            btnDown.Visible = true;
                        else
                            btnDown.Visible = false;
                    }
                }
                else // Multiple selections
                {
                    if (lstViewSteps.SelectedIndices.Count == Engine.CurrentSteps.Count)
                    {
                        btnUp.Visible = false;
                        btnDown.Visible = false;
                    }
                    else
                    {
                        bool upDisabled = false;
                        bool downDisabled = false;

                        foreach (int a in lstViewSteps.SelectedIndices)
                        {
                            if (a == 0)
                                upDisabled = true;

                            if (a == Engine.CurrentSteps.Count - 1)
                                downDisabled = true;
                        }

                        if (!upDisabled)
                            btnUp.Visible = true;
                        else
                            btnUp.Visible = false;

                        if (!downDisabled)
                            btnDown.Visible = true;
                        else
                            btnDown.Visible = false;
                    }
                }
            }

            btnDelete.Visible = true;
        }

        public void ChangePositions(bool UP)
        {
            List<Step> stepList = new List<Step>();
            List<int> stepIntList = new List<int>();

            int x = 0;
            int change = 1;

            if (UP)
                change = -1;

            foreach (int a in lstViewSteps.SelectedIndices)
            {
                Step s = Engine.CurrentSteps[a];
                stepIntList.Add(a);
                stepList.Add(s);
            }

            foreach (Step s in stepList)
            {
                Engine.CurrentSteps.Remove(s);
            }

            foreach (Step s in stepList)
            {
                Engine.CurrentSteps.Insert(stepIntList[x] + change, s);
                x++;
            }

            RefreshList();

            Engine.Saved = false;

            btnDown.Visible = false;
            btnUp.Visible = false;
            btnDelete.Visible = false;
        }

        private void RefreshList()
        {
            lstViewSteps.Items.Clear();

            foreach (Step s in Engine.CurrentSteps)
            {
                ListViewItem lvi = new ListViewItem(s.DisplayName);
                lvi.SubItems.Add(s.SecondsBeforeExecution.ToString());
                lvi.SubItems.Add(s.StepType.ToString());
                lstViewSteps.Items.Add(lvi);
            }
        }

        #region BUTTONS

        private void btnClose_Click(object sender, EventArgs e)
        {
            MoveToParentForm();
        }

        private void btnViewFile_Click(object sender, EventArgs e)
        {
            ViewFile vf = new ViewFile(this);
            vf.Show();
            Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<Step> stepList = new List<Step>();

            foreach (int a in lstViewSteps.SelectedIndices)
            {
                stepList.Add(Engine.CurrentSteps[a]);
            }

            foreach (Step s in stepList)
            {
                Engine.CurrentSteps.Remove(s);
            }

            Engine.Saved = false;

            RefreshList();

            btnUp.Visible = false;
            btnDown.Visible = false;
            btnDelete.Visible = false;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            ChangePositions(false);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            ChangePositions(true);
        }

        #endregion BUTTONS
    }
}