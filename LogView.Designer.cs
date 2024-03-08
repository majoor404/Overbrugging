namespace Overbrugging
{
    partial class LogView
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
            this.TB = new System.Windows.Forms.TextBox();
            this.ButClose = new System.Windows.Forms.Button();
            this.TBFilter = new System.Windows.Forms.TextBox();
            this.BTFilter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TB
            // 
            this.TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB.Location = new System.Drawing.Point(13, 13);
            this.TB.Multiline = true;
            this.TB.Name = "TB";
            this.TB.ReadOnly = true;
            this.TB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TB.Size = new System.Drawing.Size(890, 705);
            this.TB.TabIndex = 0;
            // 
            // ButClose
            // 
            this.ButClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButClose.Location = new System.Drawing.Point(593, 729);
            this.ButClose.Margin = new System.Windows.Forms.Padding(4);
            this.ButClose.Name = "ButClose";
            this.ButClose.Size = new System.Drawing.Size(310, 61);
            this.ButClose.TabIndex = 1;
            this.ButClose.Text = "Close";
            this.ButClose.UseVisualStyleBackColor = true;
            this.ButClose.Click += new System.EventHandler(this.ButClose_Click);
            // 
            // TBFilter
            // 
            this.TBFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBFilter.Location = new System.Drawing.Point(13, 747);
            this.TBFilter.Name = "TBFilter";
            this.TBFilter.Size = new System.Drawing.Size(201, 24);
            this.TBFilter.TabIndex = 2;
            // 
            // BTFilter
            // 
            this.BTFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTFilter.Location = new System.Drawing.Point(230, 738);
            this.BTFilter.Name = "BTFilter";
            this.BTFilter.Size = new System.Drawing.Size(115, 43);
            this.BTFilter.TabIndex = 3;
            this.BTFilter.Text = "Filter";
            this.BTFilter.UseVisualStyleBackColor = true;
            this.BTFilter.Click += new System.EventHandler(this.BTFilter_Click);
            // 
            // LogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 803);
            this.Controls.Add(this.BTFilter);
            this.Controls.Add(this.TBFilter);
            this.Controls.Add(this.ButClose);
            this.Controls.Add(this.TB);
            this.Name = "LogView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LogView";
            this.Shown += new System.EventHandler(this.LogView_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox TB;
        private System.Windows.Forms.Button ButClose;
        private System.Windows.Forms.TextBox TBFilter;
        private System.Windows.Forms.Button BTFilter;
    }
}