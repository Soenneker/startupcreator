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
using System.Windows.Forms;

#endregion

namespace StartupCreator
{
    public partial class NewStandardStep : ChildForm
    {
        public NewStandardStep(Form form)
            : base(form)
        {
            InitializeComponent();
        }

        private void NewStandardStep_Load(object sender, EventArgs e)
        {
            ActiveControl = txtAppStart;
        }

        private void txtAppStart_TextChanged(object sender, EventArgs e)
        {
            string input = txtAppStart.Text;
            Uri uriResult;

            bool result = Uri.TryCreate(input, UriKind.Absolute, out uriResult);

            if (result && input.Length > 3 && !input.EndsWith(@"\"))
            {
                lblSeconds.Visible = true;
                btnAddStep.Visible = true;
                txtTime.Visible = true;

                if (uriResult.Scheme == Uri.UriSchemeFile)
                {
                    lblParameters.Visible = true;
                    txtParameters.Visible = true;
                }
            }
            else
            {
                lblSeconds.Visible = false;
                btnAddStep.Visible = false;
                txtTime.Visible = false;
                lblParameters.Visible = false;
                txtParameters.Visible = false;
            }
        }

        #region BUTTONS

        private void btnAppBrowse_Click(object sender, EventArgs e)
        {
            string[] fullFile = Util.OpenBrowsePrompt(false);

            if (fullFile[0] != "")
            {
                txtAppStart.Text = fullFile[0] + fullFile[1];
                txtAppStart.Select(txtAppStart.Text.Length, 0);
            }

            if (txtAppStart.Text != "")
                txtParameters.Focus();
            else
                txtAppStart.Focus();
        }

        private void btnAddStep_Click(object sender, EventArgs e)
        {
            int time = 0;
            string input = "";
            string parameters = "";
            Uri uriResult;

            #region INTEGRITY CHECK

            try
            {
                if (txtTime.Text.Trim() != "")
                    time = Convert.ToInt32(txtTime.Text);
            }
            catch (Exception)
            {
                Engine.Error("Enter a valid integer for seconds before executing the service.");
                return;
            }

            input = txtAppStart.Text;
            parameters = txtParameters.Text;

            bool result = Uri.TryCreate(input, UriKind.Absolute, out uriResult);

            if (!result || input.Length < 4 || input.EndsWith(@"\"))
            {
                Engine.Error("Enter a valid file name, or URL.");
                return;
            }

            #endregion INTEGRITY CHECK

            Engine.AddStandardStep(input, parameters, time);
            MoveToParentForm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MoveToParentForm();
        }

        #endregion BUTTONS
    }
}