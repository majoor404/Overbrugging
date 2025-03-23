namespace Overbrugging
{
    partial class KeuzeType
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
            this.ButTIW = new System.Windows.Forms.Button();
            this.ButtonOverb = new System.Windows.Forms.Button();
            this.ButtonMOC = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMOC = new System.Windows.Forms.TextBox();
            this.checkBoxTT = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ButTIW
            // 
            this.ButTIW.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButTIW.Location = new System.Drawing.Point(21, 337);
            this.ButTIW.Name = "ButTIW";
            this.ButTIW.Size = new System.Drawing.Size(199, 63);
            this.ButTIW.TabIndex = 0;
            this.ButTIW.Text = "TIW";
            this.ButTIW.UseVisualStyleBackColor = true;
            // 
            // ButtonOverb
            // 
            this.ButtonOverb.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.ButtonOverb.Location = new System.Drawing.Point(21, 256);
            this.ButtonOverb.Name = "ButtonOverb";
            this.ButtonOverb.Size = new System.Drawing.Size(199, 63);
            this.ButtonOverb.TabIndex = 0;
            this.ButtonOverb.Text = "Overbruging";
            this.ButtonOverb.UseVisualStyleBackColor = true;
            // 
            // ButtonMOC
            // 
            this.ButtonMOC.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.ButtonMOC.Location = new System.Drawing.Point(21, 39);
            this.ButtonMOC.Name = "ButtonMOC";
            this.ButtonMOC.Size = new System.Drawing.Size(199, 63);
            this.ButtonMOC.TabIndex = 0;
            this.ButtonMOC.Text = "MOC";
            this.ButtonMOC.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(549, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Standaard altijd een MOC, of 1 van onderstaande items moet geselecteerd worden";
            // 
            // textBoxMOC
            // 
            this.textBoxMOC.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxMOC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMOC.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBoxMOC.Location = new System.Drawing.Point(240, 39);
            this.textBoxMOC.Multiline = true;
            this.textBoxMOC.Name = "textBoxMOC";
            this.textBoxMOC.ReadOnly = true;
            this.textBoxMOC.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMOC.Size = new System.Drawing.Size(804, 361);
            this.textBoxMOC.TabIndex = 2;
            this.textBoxMOC.Click += new System.EventHandler(this.textBoxMOC_Click);
            // 
            // checkBoxTT
            // 
            this.checkBoxTT.AutoSize = true;
            this.checkBoxTT.Checked = true;
            this.checkBoxTT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTT.Location = new System.Drawing.Point(870, 13);
            this.checkBoxTT.Name = "checkBoxTT";
            this.checkBoxTT.Size = new System.Drawing.Size(117, 22);
            this.checkBoxTT.TabIndex = 3;
            this.checkBoxTT.Text = "Filter Voor TT";
            this.checkBoxTT.UseVisualStyleBackColor = true;
            this.checkBoxTT.CheckedChanged += new System.EventHandler(this.checkBoxTT_CheckedChanged);
            // 
            // KeuzeType
            // 
            this.AcceptButton = this.ButTIW;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 412);
            this.Controls.Add(this.checkBoxTT);
            this.Controls.Add(this.textBoxMOC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonMOC);
            this.Controls.Add(this.ButtonOverb);
            this.Controls.Add(this.ButTIW);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KeuzeType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Type";
            this.Shown += new System.EventHandler(this.KeuzeType_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButTIW;
        private System.Windows.Forms.Button ButtonOverb;
        private System.Windows.Forms.Button ButtonMOC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMOC;
        private System.Windows.Forms.CheckBox checkBoxTT;
    }
}