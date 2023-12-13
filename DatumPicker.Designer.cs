namespace Overbrugging
{
    partial class DatumPicker
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DT = new System.Windows.Forms.DateTimePicker();
            this.TB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DT
            // 
            this.DT.CustomFormat = " ";
            this.DT.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DT.Location = new System.Drawing.Point(0, 0);
            this.DT.Name = "DT";
            this.DT.Size = new System.Drawing.Size(133, 24);
            this.DT.TabIndex = 0;
            this.DT.ValueChanged += new System.EventHandler(this.DT_ValueChanged);
            // 
            // TB
            // 
            this.TB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB.Location = new System.Drawing.Point(4, 3);
            this.TB.Margin = new System.Windows.Forms.Padding(0);
            this.TB.Name = "TB";
            this.TB.Size = new System.Drawing.Size(95, 17);
            this.TB.TabIndex = 1;
            this.TB.Text = " --/--/----";
            // 
            // DatumPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TB);
            this.Controls.Add(this.DT);
            this.Name = "DatumPicker";
            this.Size = new System.Drawing.Size(137, 42);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DateTimePicker DT;
        public System.Windows.Forms.TextBox TB;
    }
}
