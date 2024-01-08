namespace Overbrugging
{
    partial class UitvoeringPrintForm
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
            this.Uitvoering = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Uitvoering
            // 
            this.Uitvoering.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Uitvoering.Location = new System.Drawing.Point(13, 13);
            this.Uitvoering.Multiline = true;
            this.Uitvoering.Name = "Uitvoering";
            this.Uitvoering.Size = new System.Drawing.Size(775, 618);
            this.Uitvoering.TabIndex = 0;
            // 
            // UitvoeringPrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 643);
            this.Controls.Add(this.Uitvoering);
            this.Name = "UitvoeringPrintForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "UitvoeringPrintForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox Uitvoering;
    }
}