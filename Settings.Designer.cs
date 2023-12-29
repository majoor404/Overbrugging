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
            this.label1 = new System.Windows.Forms.Label();
            this.LabelBuildData = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.buttonNaam.Text = "Edit Namen/Funtie";
            this.buttonNaam.UseVisualStyleBackColor = true;
            // 
            // buttonSecties
            // 
            this.buttonSecties.DialogResult = System.Windows.Forms.DialogResult.Abort;
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
            this.buttonImport.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.buttonImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImport.Location = new System.Drawing.Point(12, 366);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(207, 44);
            this.buttonImport.TabIndex = 0;
            this.buttonImport.Text = "Import oude overbrugprog.";
            this.buttonImport.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(136, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Build Data :";
            // 
            // LabelBuildData
            // 
            this.LabelBuildData.AutoSize = true;
            this.LabelBuildData.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelBuildData.Location = new System.Drawing.Point(13, 240);
            this.LabelBuildData.Name = "LabelBuildData";
            this.LabelBuildData.Size = new System.Drawing.Size(56, 18);
            this.LabelBuildData.TabIndex = 1;
            this.LabelBuildData.Text = "123456";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Overbrugging.Properties.Resources.info;
            this.pictureBox1.Location = new System.Drawing.Point(16, 133);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 89);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "r.majoor@gmail.com";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 422);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LabelBuildData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonSecties);
            this.Controls.Add(this.buttonNaam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNaam;
        private System.Windows.Forms.Button buttonSecties;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LabelBuildData;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
    }
}