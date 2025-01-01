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
            this.SuspendLayout();
            // 
            // ButTIW
            // 
            this.ButTIW.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButTIW.Location = new System.Drawing.Point(22, 171);
            this.ButTIW.Name = "ButTIW";
            this.ButTIW.Size = new System.Drawing.Size(199, 63);
            this.ButTIW.TabIndex = 0;
            this.ButTIW.Text = "TIW";
            this.ButTIW.UseVisualStyleBackColor = true;
            // 
            // ButtonOverb
            // 
            this.ButtonOverb.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.ButtonOverb.Location = new System.Drawing.Point(22, 90);
            this.ButtonOverb.Name = "ButtonOverb";
            this.ButtonOverb.Size = new System.Drawing.Size(199, 63);
            this.ButtonOverb.TabIndex = 0;
            this.ButtonOverb.Text = "Overbruging";
            this.ButtonOverb.UseVisualStyleBackColor = true;
            // 
            // ButtonMOC
            // 
            this.ButtonMOC.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.ButtonMOC.Location = new System.Drawing.Point(22, 12);
            this.ButtonMOC.Name = "ButtonMOC";
            this.ButtonMOC.Size = new System.Drawing.Size(199, 63);
            this.ButtonMOC.TabIndex = 0;
            this.ButtonMOC.Text = "MOC";
            this.ButtonMOC.UseVisualStyleBackColor = true;
            // 
            // KeuzeType
            // 
            this.AcceptButton = this.ButTIW;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 262);
            this.Controls.Add(this.ButtonMOC);
            this.Controls.Add(this.ButtonOverb);
            this.Controls.Add(this.ButTIW);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KeuzeType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Type";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButTIW;
        private System.Windows.Forms.Button ButtonOverb;
        private System.Windows.Forms.Button ButtonMOC;
    }
}