namespace Overbrugging
{
    partial class EditNamen
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ButNew = new System.Windows.Forms.Button();
            this.ButDel = new System.Windows.Forms.Button();
            this.ButSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxNaam = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBoxPersNr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBoxTeam = new System.Windows.Forms.TextBox();
            this.CheckBoxIvWv = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(600, 432);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            this.dataGridView1.BindingContextChanged += new System.EventHandler(this.dataGridView1_BindingContextChanged);
            // 
            // ButNew
            // 
            this.ButNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButNew.Location = new System.Drawing.Point(629, 13);
            this.ButNew.Name = "ButNew";
            this.ButNew.Size = new System.Drawing.Size(196, 53);
            this.ButNew.TabIndex = 1;
            this.ButNew.Text = "Nieuw";
            this.ButNew.UseVisualStyleBackColor = true;
            this.ButNew.Click += new System.EventHandler(this.ButNew_Click);
            // 
            // ButDel
            // 
            this.ButDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButDel.Location = new System.Drawing.Point(629, 72);
            this.ButDel.Name = "ButDel";
            this.ButDel.Size = new System.Drawing.Size(196, 53);
            this.ButDel.TabIndex = 1;
            this.ButDel.Text = "Delete";
            this.ButDel.UseVisualStyleBackColor = true;
            this.ButDel.Click += new System.EventHandler(this.ButDel_Click);
            // 
            // ButSave
            // 
            this.ButSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButSave.Location = new System.Drawing.Point(629, 459);
            this.ButSave.Name = "ButSave";
            this.ButSave.Size = new System.Drawing.Size(196, 109);
            this.ButSave.TabIndex = 1;
            this.ButSave.Text = "Save";
            this.ButSave.UseVisualStyleBackColor = true;
            this.ButSave.Click += new System.EventHandler(this.ButSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 462);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Naam";
            // 
            // TextBoxNaam
            // 
            this.TextBoxNaam.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxNaam.Location = new System.Drawing.Point(104, 459);
            this.TextBoxNaam.Name = "TextBoxNaam";
            this.TextBoxNaam.Size = new System.Drawing.Size(410, 24);
            this.TextBoxNaam.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 502);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Pers Nr.";
            // 
            // TextBoxPersNr
            // 
            this.TextBoxPersNr.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxPersNr.Location = new System.Drawing.Point(104, 499);
            this.TextBoxPersNr.Name = "TextBoxPersNr";
            this.TextBoxPersNr.Size = new System.Drawing.Size(509, 24);
            this.TextBoxPersNr.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 547);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Team";
            // 
            // TextBoxTeam
            // 
            this.TextBoxTeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxTeam.Location = new System.Drawing.Point(104, 544);
            this.TextBoxTeam.Name = "TextBoxTeam";
            this.TextBoxTeam.Size = new System.Drawing.Size(509, 24);
            this.TextBoxTeam.TabIndex = 3;
            // 
            // CheckBoxIvWv
            // 
            this.CheckBoxIvWv.AutoSize = true;
            this.CheckBoxIvWv.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxIvWv.Location = new System.Drawing.Point(542, 462);
            this.CheckBoxIvWv.Name = "CheckBoxIvWv";
            this.CheckBoxIvWv.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CheckBoxIvWv.Size = new System.Drawing.Size(71, 22);
            this.CheckBoxIvWv.TabIndex = 4;
            this.CheckBoxIvWv.Text = "IV /WV";
            this.CheckBoxIvWv.UseVisualStyleBackColor = true;
            // 
            // EditNamen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 587);
            this.Controls.Add(this.CheckBoxIvWv);
            this.Controls.Add(this.TextBoxTeam);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextBoxPersNr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBoxNaam);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButSave);
            this.Controls.Add(this.ButDel);
            this.Controls.Add(this.ButNew);
            this.Controls.Add(this.dataGridView1);
            this.Name = "EditNamen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditNamen";
            this.Shown += new System.EventHandler(this.EditNamen_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button ButNew;
        private System.Windows.Forms.Button ButDel;
        private System.Windows.Forms.Button ButSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxNaam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBoxPersNr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextBoxTeam;
        private System.Windows.Forms.CheckBox CheckBoxIvWv;
    }
}