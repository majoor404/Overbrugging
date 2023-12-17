namespace Overbrugging
{
    partial class EditSecties
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
            this.dataGridViewSecties = new System.Windows.Forms.DataGridView();
            this.dataGridViewInstallaties = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSecties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInstallaties)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewSecties
            // 
            this.dataGridViewSecties.AllowUserToAddRows = false;
            this.dataGridViewSecties.AllowUserToDeleteRows = false;
            this.dataGridViewSecties.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSecties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSecties.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewSecties.MultiSelect = false;
            this.dataGridViewSecties.Name = "dataGridViewSecties";
            this.dataGridViewSecties.ReadOnly = true;
            this.dataGridViewSecties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSecties.Size = new System.Drawing.Size(318, 176);
            this.dataGridViewSecties.TabIndex = 1;
            // 
            // dataGridViewInstallaties
            // 
            this.dataGridViewInstallaties.AllowUserToAddRows = false;
            this.dataGridViewInstallaties.AllowUserToDeleteRows = false;
            this.dataGridViewInstallaties.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewInstallaties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInstallaties.Location = new System.Drawing.Point(12, 226);
            this.dataGridViewInstallaties.MultiSelect = false;
            this.dataGridViewInstallaties.Name = "dataGridViewInstallaties";
            this.dataGridViewInstallaties.ReadOnly = true;
            this.dataGridViewInstallaties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewInstallaties.Size = new System.Drawing.Size(318, 303);
            this.dataGridViewInstallaties.TabIndex = 2;
            // 
            // EditSecties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 562);
            this.Controls.Add(this.dataGridViewInstallaties);
            this.Controls.Add(this.dataGridViewSecties);
            this.Name = "EditSecties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Secties";
            this.Shown += new System.EventHandler(this.EditSecties_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSecties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInstallaties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridViewSecties;
        public System.Windows.Forms.DataGridView dataGridViewInstallaties;
    }
}