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
    public partial class ViewFile : ChildForm
    {
        public ViewFile(Form form)
            : base(form)
        {
            InitializeComponent();
        }

        private void ViewForm_Load(object sender, EventArgs e)
        {
            Engine.FinalizeScriptFile();

            int x = 0;

            foreach (string s in Engine.FileContents)
            {
                if (x > 0)
                    txtViewFile.AppendText(Environment.NewLine);

                txtViewFile.AppendText(s);
                x++;
            }

            ActiveControl = btnClose;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MoveToParentForm();
        }
    }
}