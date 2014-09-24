using System.ComponentModel;

namespace StartupCreator
{
    partial class NewStandardStep
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewStandardStep));
            this.lblParameters = new System.Windows.Forms.Label();
            this.txtParameters = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddStep = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.btnAppBrowse = new System.Windows.Forms.Button();
            this.txtAppStart = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblParameters
            // 
            this.lblParameters.AutoSize = true;
            this.lblParameters.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParameters.ForeColor = System.Drawing.Color.White;
            this.lblParameters.Location = new System.Drawing.Point(14, 64);
            this.lblParameters.Name = "lblParameters";
            this.lblParameters.Size = new System.Drawing.Size(66, 15);
            this.lblParameters.TabIndex = 7;
            this.lblParameters.Text = "Parameters:";
            this.lblParameters.Visible = false;
            // 
            // txtParameters
            // 
            this.txtParameters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtParameters.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParameters.ForeColor = System.Drawing.Color.White;
            this.txtParameters.Location = new System.Drawing.Point(85, 62);
            this.txtParameters.Name = "txtParameters";
            this.txtParameters.Size = new System.Drawing.Size(201, 22);
            this.txtParameters.TabIndex = 2;
            this.txtParameters.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(373, 90);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 27);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddStep
            // 
            this.btnAddStep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddStep.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddStep.ForeColor = System.Drawing.Color.White;
            this.btnAddStep.Location = new System.Drawing.Point(16, 90);
            this.btnAddStep.Name = "btnAddStep";
            this.btnAddStep.Size = new System.Drawing.Size(351, 27);
            this.btnAddStep.TabIndex = 4;
            this.btnAddStep.Text = "Add Step";
            this.btnAddStep.UseVisualStyleBackColor = true;
            this.btnAddStep.Visible = false;
            this.btnAddStep.Click += new System.EventHandler(this.btnAddStep_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(183, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "File or URL";
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeconds.ForeColor = System.Drawing.Color.White;
            this.lblSeconds.Location = new System.Drawing.Point(287, 65);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(82, 15);
            this.lblSeconds.TabIndex = 8;
            this.lblSeconds.Text = "Seconds delay:";
            this.lblSeconds.Visible = false;
            // 
            // txtTime
            // 
            this.txtTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTime.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTime.ForeColor = System.Drawing.Color.White;
            this.txtTime.Location = new System.Drawing.Point(373, 62);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(69, 22);
            this.txtTime.TabIndex = 3;
            this.txtTime.Text = "0";
            this.txtTime.Visible = false;
            // 
            // btnAppBrowse
            // 
            this.btnAppBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppBrowse.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppBrowse.ForeColor = System.Drawing.Color.White;
            this.btnAppBrowse.Location = new System.Drawing.Point(292, 33);
            this.btnAppBrowse.Name = "btnAppBrowse";
            this.btnAppBrowse.Size = new System.Drawing.Size(150, 25);
            this.btnAppBrowse.TabIndex = 1;
            this.btnAppBrowse.Text = "Browse";
            this.btnAppBrowse.UseVisualStyleBackColor = true;
            this.btnAppBrowse.Click += new System.EventHandler(this.btnAppBrowse_Click);
            // 
            // txtAppStart
            // 
            this.txtAppStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtAppStart.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAppStart.ForeColor = System.Drawing.Color.White;
            this.txtAppStart.Location = new System.Drawing.Point(16, 33);
            this.txtAppStart.Name = "txtAppStart";
            this.txtAppStart.Size = new System.Drawing.Size(270, 22);
            this.txtAppStart.TabIndex = 0;
            this.txtAppStart.TextChanged += new System.EventHandler(this.txtAppStart_TextChanged);
            // 
            // NewStandardStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(449, 129);
            this.Controls.Add(this.lblParameters);
            this.Controls.Add(this.txtParameters);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddStep);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblSeconds);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.btnAppBrowse);
            this.Controls.Add(this.txtAppStart);
            this.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewStandardStep";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Standard Step";
            this.Load += new System.EventHandler(this.NewStandardStep_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddStep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSeconds;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Button btnAppBrowse;
        private System.Windows.Forms.TextBox txtAppStart;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtParameters;
        private System.Windows.Forms.Label lblParameters;
    }
}