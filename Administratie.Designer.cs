namespace Overbrugging
{
    partial class Administratie
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.BTAovToPG = new System.Windows.Forms.Button();
            this.BTToXML = new System.Windows.Forms.Button();
            this.BTClose = new System.Windows.Forms.Button();
            this.DGAdmin = new System.Windows.Forms.DataGridView();
            this.BTSave = new System.Windows.Forms.Button();
            this.LBChange = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGAdmin)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LBChange);
            this.panel1.Controls.Add(this.BTSave);
            this.panel1.Controls.Add(this.BTAovToPG);
            this.panel1.Controls.Add(this.BTToXML);
            this.panel1.Controls.Add(this.BTClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1617, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 961);
            this.panel1.TabIndex = 0;
            // 
            // BTAovToPG
            // 
            this.BTAovToPG.Location = new System.Drawing.Point(29, 151);
            this.BTAovToPG.Name = "BTAovToPG";
            this.BTAovToPG.Size = new System.Drawing.Size(213, 71);
            this.BTAovToPG.TabIndex = 0;
            this.BTAovToPG.Text = "AOV to PG&A - PA";
            this.BTAovToPG.UseVisualStyleBackColor = true;
            this.BTAovToPG.Click += new System.EventHandler(this.BTAovToPG_Click);
            // 
            // BTToXML
            // 
            this.BTToXML.Location = new System.Drawing.Point(29, 61);
            this.BTToXML.Name = "BTToXML";
            this.BTToXML.Size = new System.Drawing.Size(213, 71);
            this.BTToXML.TabIndex = 0;
            this.BTToXML.Text = "To XML";
            this.BTToXML.UseVisualStyleBackColor = true;
            this.BTToXML.Click += new System.EventHandler(this.BTToXML_Click);
            // 
            // BTClose
            // 
            this.BTClose.Location = new System.Drawing.Point(29, 866);
            this.BTClose.Name = "BTClose";
            this.BTClose.Size = new System.Drawing.Size(213, 71);
            this.BTClose.TabIndex = 0;
            this.BTClose.Text = "Close";
            this.BTClose.UseVisualStyleBackColor = true;
            this.BTClose.Click += new System.EventHandler(this.BTClose_Click);
            // 
            // DGAdmin
            // 
            this.DGAdmin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGAdmin.Location = new System.Drawing.Point(0, 0);
            this.DGAdmin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DGAdmin.Name = "DGAdmin";
            this.DGAdmin.Size = new System.Drawing.Size(1617, 961);
            this.DGAdmin.TabIndex = 1;
            // 
            // BTSave
            // 
            this.BTSave.Location = new System.Drawing.Point(29, 769);
            this.BTSave.Name = "BTSave";
            this.BTSave.Size = new System.Drawing.Size(213, 71);
            this.BTSave.TabIndex = 0;
            this.BTSave.Text = "Save overbruging.bin";
            this.BTSave.UseVisualStyleBackColor = true;
            this.BTSave.Click += new System.EventHandler(this.BTSave_Click);
            // 
            // LBChange
            // 
            this.LBChange.AutoSize = true;
            this.LBChange.BackColor = System.Drawing.Color.Red;
            this.LBChange.Location = new System.Drawing.Point(95, 19);
            this.LBChange.Name = "LBChange";
            this.LBChange.Size = new System.Drawing.Size(80, 20);
            this.LBChange.TabIndex = 1;
            this.LBChange.Text = "CHANGE!!!";
            this.LBChange.Visible = false;
            // 
            // Administratie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1884, 961);
            this.Controls.Add(this.DGAdmin);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Administratie";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administratie";
            this.Shown += new System.EventHandler(this.Administratie_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGAdmin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BTClose;
        private System.Windows.Forms.DataGridView DGAdmin;
        private System.Windows.Forms.Button BTToXML;
        private System.Windows.Forms.Button BTAovToPG;
        private System.Windows.Forms.Button BTSave;
        private System.Windows.Forms.Label LBChange;
    }
}