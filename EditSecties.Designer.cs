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
            this.dataGridViewInstal = new System.Windows.Forms.DataGridView();
            this.TextBoxSectie = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBoxInstall = new System.Windows.Forms.TextBox();
            this.ButNewSec = new System.Windows.Forms.Button();
            this.ButDeleteSec = new System.Windows.Forms.Button();
            this.ButSaveSec = new System.Windows.Forms.Button();
            this.ButNewInstall = new System.Windows.Forms.Button();
            this.ButSaveInstall = new System.Windows.Forms.Button();
            this.ButDelInstall = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSecties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInstal)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewSecties
            // 
            this.dataGridViewSecties.AllowUserToAddRows = false;
            this.dataGridViewSecties.AllowUserToDeleteRows = false;
            this.dataGridViewSecties.AllowUserToResizeRows = false;
            this.dataGridViewSecties.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSecties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSecties.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewSecties.MultiSelect = false;
            this.dataGridViewSecties.Name = "dataGridViewSecties";
            this.dataGridViewSecties.ReadOnly = true;
            this.dataGridViewSecties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSecties.Size = new System.Drawing.Size(287, 298);
            this.dataGridViewSecties.TabIndex = 1;
            this.dataGridViewSecties.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewSecties_CellClick);
            // 
            // dataGridViewInstal
            // 
            this.dataGridViewInstal.AllowUserToAddRows = false;
            this.dataGridViewInstal.AllowUserToDeleteRows = false;
            this.dataGridViewInstal.AllowUserToResizeRows = false;
            this.dataGridViewInstal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewInstal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInstal.Location = new System.Drawing.Point(351, 12);
            this.dataGridViewInstal.MultiSelect = false;
            this.dataGridViewInstal.Name = "dataGridViewInstal";
            this.dataGridViewInstal.ReadOnly = true;
            this.dataGridViewInstal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewInstal.Size = new System.Drawing.Size(349, 298);
            this.dataGridViewInstal.TabIndex = 2;
            this.dataGridViewInstal.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewInstal_CellClick);
            // 
            // TextBoxSectie
            // 
            this.TextBoxSectie.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxSectie.Location = new System.Drawing.Point(71, 334);
            this.TextBoxSectie.Name = "TextBoxSectie";
            this.TextBoxSectie.Size = new System.Drawing.Size(228, 24);
            this.TextBoxSectie.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 337);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sectie";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(348, 337);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Installatie";
            // 
            // TextBoxInstall
            // 
            this.TextBoxInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxInstall.Location = new System.Drawing.Point(431, 334);
            this.TextBoxInstall.Name = "TextBoxInstall";
            this.TextBoxInstall.Size = new System.Drawing.Size(269, 24);
            this.TextBoxInstall.TabIndex = 5;
            // 
            // ButNewSec
            // 
            this.ButNewSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButNewSec.Location = new System.Drawing.Point(71, 375);
            this.ButNewSec.Name = "ButNewSec";
            this.ButNewSec.Size = new System.Drawing.Size(228, 50);
            this.ButNewSec.TabIndex = 6;
            this.ButNewSec.Text = "Nieuw";
            this.ButNewSec.UseVisualStyleBackColor = true;
            this.ButNewSec.Click += new System.EventHandler(this.ButNewSec_Click);
            // 
            // ButDeleteSec
            // 
            this.ButDeleteSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButDeleteSec.Location = new System.Drawing.Point(71, 487);
            this.ButDeleteSec.Name = "ButDeleteSec";
            this.ButDeleteSec.Size = new System.Drawing.Size(228, 50);
            this.ButDeleteSec.TabIndex = 6;
            this.ButDeleteSec.Text = "Delete";
            this.ButDeleteSec.UseVisualStyleBackColor = true;
            this.ButDeleteSec.Click += new System.EventHandler(this.ButDeleteSec_Click);
            // 
            // ButSaveSec
            // 
            this.ButSaveSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButSaveSec.Location = new System.Drawing.Point(71, 431);
            this.ButSaveSec.Name = "ButSaveSec";
            this.ButSaveSec.Size = new System.Drawing.Size(228, 50);
            this.ButSaveSec.TabIndex = 6;
            this.ButSaveSec.Text = "Save";
            this.ButSaveSec.UseVisualStyleBackColor = true;
            this.ButSaveSec.Click += new System.EventHandler(this.ButSaveSec_Click);
            // 
            // ButNewInstall
            // 
            this.ButNewInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButNewInstall.Location = new System.Drawing.Point(431, 375);
            this.ButNewInstall.Name = "ButNewInstall";
            this.ButNewInstall.Size = new System.Drawing.Size(269, 50);
            this.ButNewInstall.TabIndex = 6;
            this.ButNewInstall.Text = "Nieuw";
            this.ButNewInstall.UseVisualStyleBackColor = true;
            this.ButNewInstall.Click += new System.EventHandler(this.ButNewInstall_Click);
            // 
            // ButSaveInstall
            // 
            this.ButSaveInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButSaveInstall.Location = new System.Drawing.Point(431, 431);
            this.ButSaveInstall.Name = "ButSaveInstall";
            this.ButSaveInstall.Size = new System.Drawing.Size(269, 50);
            this.ButSaveInstall.TabIndex = 6;
            this.ButSaveInstall.Text = "Save";
            this.ButSaveInstall.UseVisualStyleBackColor = true;
            this.ButSaveInstall.Click += new System.EventHandler(this.ButSaveInstall_Click);
            // 
            // ButDelInstall
            // 
            this.ButDelInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButDelInstall.Location = new System.Drawing.Point(431, 487);
            this.ButDelInstall.Name = "ButDelInstall";
            this.ButDelInstall.Size = new System.Drawing.Size(269, 50);
            this.ButDelInstall.TabIndex = 6;
            this.ButDelInstall.Text = "Delete";
            this.ButDelInstall.UseVisualStyleBackColor = true;
            this.ButDelInstall.Click += new System.EventHandler(this.ButDelInstall_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(1, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(316, 545);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(323, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(391, 545);
            this.panel2.TabIndex = 8;
            // 
            // EditSecties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 562);
            this.Controls.Add(this.ButSaveInstall);
            this.Controls.Add(this.ButSaveSec);
            this.Controls.Add(this.ButDelInstall);
            this.Controls.Add(this.ButDeleteSec);
            this.Controls.Add(this.ButNewInstall);
            this.Controls.Add(this.ButNewSec);
            this.Controls.Add(this.TextBoxInstall);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBoxSectie);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewInstal);
            this.Controls.Add(this.dataGridViewSecties);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "EditSecties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Secties";
            this.Shown += new System.EventHandler(this.EditSecties_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSecties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInstal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridViewSecties;
        public System.Windows.Forms.DataGridView dataGridViewInstal;
        private System.Windows.Forms.TextBox TextBoxSectie;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBoxInstall;
        private System.Windows.Forms.Button ButNewSec;
        private System.Windows.Forms.Button ButDeleteSec;
        private System.Windows.Forms.Button ButSaveSec;
        private System.Windows.Forms.Button ButNewInstall;
        private System.Windows.Forms.Button ButSaveInstall;
        private System.Windows.Forms.Button ButDelInstall;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}