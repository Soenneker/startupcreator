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
    public partial class NewServiceStep : ChildForm
    {
        public NewServiceStep(Form form)
            : base(form)
        {
            InitializeComponent();
        }

        private void NewStep_Load(object sender, EventArgs e)
        {
            List<Service> services = Util.GetServices();

            foreach (Service s in services)
            {
                ListViewItem item = new ListViewItem(s.DisplayName);
                item.SubItems.Add(s.Status);
                item.SubItems.Add(s.ActionName);
                lstViewServices.Items.Add(item);
            }

            lstViewServices.Columns[0].Width = 213;
            lstViewServices.Columns[1].Width = 64;
            lstViewServices.Columns[2].Width = 166;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MoveToParentForm();
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            int time = 0;
            string listBoxSelectedDisplay = "";
            string listBoxSelectedService = "";

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

            try
            {
                listBoxSelectedDisplay = lstViewServices.SelectedItems[0].SubItems[0].Text;
                listBoxSelectedService = lstViewServices.SelectedItems[0].SubItems[2].Text;
            }
            catch (Exception)
            {
                Engine.Error("Please select a service to add.");
                return;
            }

            #endregion INTEGRITY CHECK

            Engine.AddServiceStep(listBoxSelectedDisplay, listBoxSelectedService, time);

            MoveToParentForm();
        }

        private void lstViewServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddService.Visible = true;
            lblSeconds.Visible = true;
            txtTime.Visible = true;
            btnAddService.Focus();
        }
    }
}