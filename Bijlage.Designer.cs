namespace Overbrugging
{
    partial class Bijlage
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
            this.ListBox = new System.Windows.Forms.ListBox();
            this.ButVoegToe = new System.Windows.Forms.Button();
            this.ButVerwijder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListBox
            // 
            this.ListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListBox.FormattingEnabled = true;
            this.ListBox.ItemHeight = 18;
            this.ListBox.Location = new System.Drawing.Point(13, 13);
            this.ListBox.Name = "ListBox";
            this.ListBox.Size = new System.Drawing.Size(376, 310);
            this.ListBox.TabIndex = 0;
            // 
            // ButVoegToe
            // 
            this.ButVoegToe.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButVoegToe.Location = new System.Drawing.Point(407, 13);
            this.ButVoegToe.Name = "ButVoegToe";
            this.ButVoegToe.Size = new System.Drawing.Size(189, 60);
            this.ButVoegToe.TabIndex = 1;
            this.ButVoegToe.Text = "Voeg Toe";
            this.ButVoegToe.UseVisualStyleBackColor = true;
            this.ButVoegToe.Click += new System.EventHandler(this.ButVoegToe_Click);
            // 
            // ButVerwijder
            // 
            this.ButVerwijder.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButVerwijder.Location = new System.Drawing.Point(407, 93);
            this.ButVerwijder.Name = "ButVerwijder";
            this.ButVerwijder.Size = new System.Drawing.Size(189, 60);
            this.ButVerwijder.TabIndex = 1;
            this.ButVerwijder.Text = "Delete";
            this.ButVerwijder.UseVisualStyleBackColor = true;
            // 
            // Bijlage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 337);
            this.Controls.Add(this.ButVerwijder);
            this.Controls.Add(this.ButVoegToe);
            this.Controls.Add(this.ListBox);
            this.Name = "Bijlage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bijlage";
            this.Shown += new System.EventHandler(this.Bijlage_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ListBox;
        private System.Windows.Forms.Button ButVoegToe;
        private System.Windows.Forms.Button ButVerwijder;
    }
}