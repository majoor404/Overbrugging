namespace Overbrugging
{
    partial class Settings
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
            this.buttonNaam = new System.Windows.Forms.Button();
            this.buttonSecties = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonNaam
            // 
            this.buttonNaam.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonNaam.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNaam.Location = new System.Drawing.Point(12, 12);
            this.buttonNaam.Name = "buttonNaam";
            this.buttonNaam.Size = new System.Drawing.Size(207, 44);
            this.buttonNaam.TabIndex = 0;
            this.buttonNaam.Text = "Edit Namen/Funties";
            this.buttonNaam.UseVisualStyleBackColor = true;
            // 
            // buttonSecties
            // 
            this.buttonSecties.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSecties.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSecties.Location = new System.Drawing.Point(12, 72);
            this.buttonSecties.Name = "buttonSecties";
            this.buttonSecties.Size = new System.Drawing.Size(207, 44);
            this.buttonSecties.TabIndex = 0;
            this.buttonSecties.Text = "Edit Secties";
            this.buttonSecties.UseVisualStyleBackColor = true;
            // 
            // buttonImport
            // 
            this.buttonImport.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.buttonImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImport.Location = new System.Drawing.Point(12, 365);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(207, 44);
            this.buttonImport.TabIndex = 0;
            this.buttonImport.Text = "Import oude overbrugprog.";
            this.buttonImport.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 422);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonSecties);
            this.Controls.Add(this.buttonNaam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonNaam;
        private System.Windows.Forms.Button buttonSecties;
        private System.Windows.Forms.Button buttonImport;
    }
}